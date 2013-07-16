﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.Utility;
using Sarcasm.GrammarAst;

using Grammar = Sarcasm.GrammarAst.Grammar;

namespace Sarcasm.Unparsing
{
    public class Unparser : IUnparser, IPostProcessHelper
    {
        #region Tracing

        internal const string logDirectoryName = "unparse_logs";

        internal readonly static TraceSource tsUnparse = new TraceSource("UNPARSE", SourceLevels.Verbose);
        internal readonly static TraceSource tsPriorities = new TraceSource("PRIORITIES", SourceLevels.Verbose);

#if DEBUG
        static Unparser()
        {
            Directory.CreateDirectory(logDirectoryName);

            Trace.AutoFlush = true;

            tsUnparse.Listeners.Clear();
            tsUnparse.Listeners.Add(new TextWriterTraceListener(File.Create(Path.Combine(logDirectoryName, "00_unparse.log"))));

            tsPriorities.Listeners.Clear();
            tsPriorities.Listeners.Add(new TextWriterTraceListener(File.Create(Path.Combine(logDirectoryName, "priorities.log"))));
        }
#endif

        #endregion

        #region Constants

        private const Direction directionDefault = Direction.LeftToRight;
        private const bool enablePartialInvalidationDefault = false;
        private const bool enableParallelProcessingDefault = false;
        private static readonly bool multiCoreSystem = Environment.ProcessorCount > 1;

        #endregion

        #region State

        #region Immutable after initialization or public settings

        public Grammar Grammar { get; private set; }
        private Formatting formatting;
        private readonly UnparseControl unparseControl;
        public bool EnablePartialInvalidation { get; set; }
        public bool EnableParallelProcessing { get; set; }

        #endregion

        #region Mutable

        private Formatter formatter;
        private ExpressionUnparser expressionUnparser;
        private Direction _direction;
        private ResourceCounter parallelTaskCounter;
        private Dictionary<ConstantTerminal, Dictionary<object, string>> constantTerminalToInverseConstantTable;

        #endregion

        #endregion

        #region Settings

        public Formatting Formatting
        {
            get { return formatting; }

            set
            {
                formatting = value;
                formatter = new Formatter(value);
            }
        }

        public int DegreeOfParallelism
        {
            get { return parallelTaskCounter.TotalNumberOfResources; }

            set
            {
                if (value < 1 || value > Environment.ProcessorCount)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format("DegreeOfParallelism should be more than zero and no more than {0} (Environment.ProcessorCount)", Environment.ProcessorCount)
                        );
                }

                parallelTaskCounter = new ResourceCounter(totalNumberOfResources: value, initialAcquiredNumberOfResources: 1);
            }
        }

        private bool UseParallelProcessing { get { return multiCoreSystem && EnableParallelProcessing; } }

        #endregion

        #region Construction

        public Unparser(Grammar grammar, Formatting formatting)
        {
            this.Grammar = grammar;
            this.Formatting = formatting;   // also sets Formatter
            this.unparseControl = grammar.UnparseControl;
            this.expressionUnparser = new ExpressionUnparser(this, grammar.UnparseControl);
            this.EnablePartialInvalidation = enablePartialInvalidationDefault;
            this.EnableParallelProcessing = enableParallelProcessingDefault;
            this.DegreeOfParallelism = Environment.ProcessorCount;
            this.constantTerminalToInverseConstantTable = new Dictionary<ConstantTerminal,Dictionary<object,string>>();
        }

        public Unparser(Grammar grammar)
            : this(grammar, grammar.UnparseControl.DefaultFormatting)
        {
        }

        private Unparser(Unparser that)
        {
            this.Grammar = that.Grammar;
            this.formatting = that.formatting;  // we are not using the property because it would set the formatter which we don't want
            this.unparseControl = that.unparseControl;
            this.EnablePartialInvalidation = that.EnablePartialInvalidation;
            this.EnableParallelProcessing = that.EnableParallelProcessing;
            this.parallelTaskCounter = that.parallelTaskCounter;
            this._direction = that._direction;
            this.constantTerminalToInverseConstantTable = that.constantTerminalToInverseConstantTable;

            // the following should be set after the constructor
            this.formatter = null;
            this.expressionUnparser = null;
        }

        private Unparser Spawn(Formatter.ChildLocation childLocation = Formatter.ChildLocation.Unknown)
        {
            return Spawn(child: null, childLocation: childLocation);
        }

        private Unparser Spawn(UnparsableObject child, Formatter.ChildLocation childLocation = Formatter.ChildLocation.Unknown)
        {
            Unparser spawn = new Unparser(this);
            spawn.formatter = this.formatter.Spawn(child, childLocation);
            spawn.expressionUnparser = this.expressionUnparser.Spawn(spawn);
            return spawn;
        }

        #endregion

        #region Unparse logic

        public IEnumerable<Utoken> Unparse(object obj, Direction direction = directionDefault)
        {
            return Unparse(obj, Grammar.Root, direction);
        }

        public IEnumerable<Utoken> Unparse(object obj, BnfTerm bnfTerm, Direction direction = directionDefault)
        {
            ResetMutableState();
            this.direction = direction;
            var root = new UnparsableObject(bnfTerm, obj);
            root.SetAsRoot();
            return UnparseRaw(root)
                .Cook(this);
        }

        private Unparser.Direction direction
        {
            get { return this._direction; }
            set
            {
                this._direction = value;
                formatter.Direction = value;
            }
        }

        private void ResetMutableState()
        {
            formatter.ResetMutableState();
            // NOTE: expressionUnparser does ResetMutableState automatically
        }

        internal IEnumerable<UtokenBase> UnparseRaw(UnparsableObject self)
        {
            if (self.BnfTerm == null)
                throw new ArgumentNullException("bnfTerm must not be null", "bnfTerm");

            Formatter.Params @params;

            return expressionUnparser.OngoingOperatorGet
                ? UnparseRawMiddle(self)
                : ConcatIfAnyMiddle(
                    formatter.YieldBefore(self, out @params),
                    UnparseRawMiddle(self),
                    formatter.YieldAfter(self, @params)
                    );
        }

        private IEnumerable<UtokenBase> UnparseRawMiddle(UnparsableObject self)
        {
            if (self.BnfTerm is KeyTerm)
            {
                tsUnparse.Debug("keyterm: [{0}]", ((KeyTerm)self.BnfTerm).Text);
                tsUnparse.Debug("SetAsLeave: {0}", self);
                self.SetAsLeave();
                yield return UtokenValue.CreateText(((KeyTerm)self.BnfTerm).Text, self);
            }
            else if (self.BnfTerm is ConstantTerminal)
            {
                string lexeme = GetLexemeByValue((ConstantTerminal)self.BnfTerm, self.AstValue);

                tsUnparse.Debug("constant_terminal: [\"{0}\" ({1})]", lexeme, self.AstValue.ToString());
                tsUnparse.Debug("SetAsLeave: {0}", self);

                self.SetAsLeave();
                yield return UtokenValue.CreateText(lexeme, self);
            }
            else if (self.BnfTerm is Terminal)
            {
                tsUnparse.Debug("terminal: [\"{0}\"]", self.AstValue.ToString());
                tsUnparse.Debug("SetAsLeave: {0}", self);
                self.SetAsLeave();
                yield return UtokenValue.CreateText(self);
            }
            else if (self.BnfTerm is NonTerminal)
            {
                tsUnparse.Debug("nonterminal: {0}", self);
                tsUnparse.Indent();

                try
                {
                    IUnparsableNonTerminal unparsableSelf = self.BnfTerm as IUnparsableNonTerminal;

                    if (unparsableSelf == null)
                        throw new UnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' is not IUnparsable.", self.AstValue, self.AstValue.GetType().Name, self.BnfTerm.Name));

                    IEnumerable<UtokenValue> directUtokens;

                    if (unparsableSelf.TryGetUtokensDirectly(this, self.AstValue, out directUtokens))
                    {
                        tsUnparse.Debug("SetAsLeave: {0}", self);
                        self.SetAsLeave();

                        foreach (UtokenValue directUtoken in directUtokens)
                        {
                            if (directUtoken is UtokenToUnparse)
                            {
                                UnparsableObject child = ((UtokenToUnparse)directUtoken).UnparsableObject;

                                foreach (UtokenBase utoken in UnparseRaw(child))
                                    yield return utoken;
                            }
                            else
                                yield return directUtoken;
                        }

                        tsUnparse.Debug("utokenized: [{0}]", self.AstValue != null ? string.Format("\"{0}\"", self.AstValue) : "<<NULL>>");
                    }
                    else
                    {
                        IEnumerable<UnparsableObject> chosenChildren = ChooseChildrenByPriority(self);

                        if (expressionUnparser.NeedsExpressionUnparse(self.BnfTerm))
                        {
                            /*
                             * NOTE: LinkChildrenToEachOthersAndToSelfLazy is being called inside expressionUnparser.Unparse,
                             * because it may extend the list with automatic parentheses.
                             * */
                            foreach (var utoken in expressionUnparser.Unparse(self, chosenChildren, direction))
                                yield return utoken;
                        }
                        else
                        {
                            bool shouldUnparseParallel;
                            int numberOfParallelTaskToStartActually;
                            int subRangeCount;
                            List<UnparsableObject> chosenChildrenList;

                            shouldUnparseParallel = ShouldUnparseParallelAndAcquireTasksIfNeeded(
                                                        chosenChildren,
                                                        out numberOfParallelTaskToStartActually,
                                                        out subRangeCount,
                                                        out chosenChildrenList);

                            if (shouldUnparseParallel)
                            {
                                LinkChildrenToEachOthersAndToSelfLazy(self, chosenChildrenList, enableUnlinkOfChild: false).ConsumeAll();

                                foreach (var utoken in UnparseRawParallel(chosenChildrenList, numberOfParallelTaskToStartActually, subRangeCount))
                                    yield return utoken;
                            }
                            else
                            {
                                foreach (UnparsableObject chosenChild in LinkChildrenToEachOthersAndToSelfLazy(self, chosenChildren, enableUnlinkOfChild: true))
                                    foreach (UtokenBase utoken in UnparseRaw(chosenChild))
                                        yield return utoken;
                            }
                        }
                    }
                }
                finally
                {
                    tsUnparse.Unindent();
                }
            }
            else if (self.BnfTerm is GrammarHint)
            {
                // GrammarHint is legal, but it does not need any unparse
            }
            else
            {
                throw new ArgumentException(string.Format("bnfTerm '{0}' with unknown type: '{1}'" + self.BnfTerm.Name, self.BnfTerm.GetType().Name));
            }
        }

        private IEnumerable<UtokenBase> UnparseRawParallel(List<UnparsableObject> chosenChildrenList, int numberOfParallelTaskToStartActually, int subRangeCount)
        {
            var subUnparseTasks = new Task[numberOfParallelTaskToStartActually];
            var subUtokensList = new List<UtokenBase>[numberOfParallelTaskToStartActually];

            for (int i = 0; i < subUnparseTasks.Length; i++)
            {
                int taskIndex = i;      // NOTE: needed for closure working correctly

                subUnparseTasks[taskIndex] =
                    Task.Run(
                    () =>
                    {
                        subUtokensList[taskIndex] = new List<UtokenBase>();

                        int subRangeBeginIndex = taskIndex * subRangeCount;
                        int subRangeEndIndex = Math.Min(subRangeBeginIndex + subRangeCount, chosenChildrenList.Count);

                        Formatter.ChildLocation childLocation =
                            chosenChildrenList.Count == 1                       ?   Formatter.ChildLocation.Only :
                            subRangeBeginIndex == 0                             ?   Formatter.ChildLocation.First :
                            subRangeBeginIndex == chosenChildrenList.Count - 1  ?   Formatter.ChildLocation.Last :
                                                                                    Formatter.ChildLocation.Middle;

                        Unparser subUnparser = this.Spawn(chosenChildrenList[subRangeBeginIndex], childLocation);

                        for (int childIndex = subRangeBeginIndex; childIndex < subRangeEndIndex; childIndex++)
                            subUtokensList[taskIndex].AddRange(subUnparser.UnparseRaw(chosenChildrenList[childIndex]));

                        ReleaseTaskIfNeeded(taskIndex);
                    });
            }

            Task.WaitAll(subUnparseTasks);

            foreach (var subUtokens in subUtokensList)
                foreach (UtokenBase utoken in subUtokens)
                    yield return utoken;
        }

        private bool ShouldUnparseParallelAndAcquireTasksIfNeeded(IEnumerable<UnparsableObject> chosenChildren,
            out int numberOfParallelTaskToStartActually, out int subRangeCount, out List<UnparsableObject> chosenChildrenList)
        {
            numberOfParallelTaskToStartActually = -1;
            subRangeCount = -1;
            chosenChildrenList = null;

            if (!UseParallelProcessing)
                return false;

            chosenChildrenList = chosenChildren.ToList();

            if (chosenChildrenList.Count <= 1)
                return false;

            // heuristics
            if (chosenChildrenList.Count / 2 < parallelTaskCounter.FreeNumberOfResources)
                return false;

            int numberOfParallelTaskToStartIdeally = chosenChildrenList.Count;
            int numberOfNewParallelTaskToStartIdeally = numberOfParallelTaskToStartIdeally - 1;
            int numberOfNewParallelTaskCanBeStartedActually;

            if (!parallelTaskCounter.TryAcquireOrLess(numberOfNewParallelTaskToStartIdeally, out numberOfNewParallelTaskCanBeStartedActually))
                return false;

            int numberOfParallelTaskCanBeStartedActually = numberOfNewParallelTaskCanBeStartedActually + 1;
            subRangeCount = (int)Math.Ceiling((double)(chosenChildrenList.Count) / numberOfParallelTaskCanBeStartedActually);
            numberOfParallelTaskToStartActually = (int)Math.Ceiling((double)(chosenChildrenList.Count) / subRangeCount);

            if (numberOfParallelTaskToStartActually < numberOfParallelTaskCanBeStartedActually)
                parallelTaskCounter.Release(numberOfParallelTaskCanBeStartedActually - numberOfParallelTaskToStartActually);    // we don't need all the tasks

            return true;
        }

        private bool ReleaseTaskIfNeeded(int taskIndex)
        {
            if (taskIndex > 0)
            {
                parallelTaskCounter.Release(1);     // release all tasks except the first one
                return true;
            }
            else
                return false;
        }

        internal IEnumerable<UnparsableObject> LinkChildrenToEachOthersAndToSelfLazy(UnparsableObject self, IEnumerable<UnparsableObject> children, bool enableUnlinkOfChild = true)
        {
            UnparsableObject childPrevSibling = null;

            return children
                .ForAll(
                    executeBeforeEachIteration:
                        child =>
                        {
                            LinkChild(self, child, childPrevSibling, enableUnlinkOfChild);
                            childPrevSibling = child;
                        },

                    executeAfterFinished:
                        () =>
                            LinkChild(self, child: null, childPrevSibling: childPrevSibling, enableUnlinkOfChild: enableUnlinkOfChild)
                );
        }

        private void LinkChild(UnparsableObject self, UnparsableObject child, UnparsableObject childPrevSibling, bool enableUnlinkOfChild)
        {
            if (child != null)
            {
                tsUnparse.Debug("child is linked: {0}", child);

                child.SyntaxParent = self;

                child.AstParent =   child.AstValue != self.AstValue       ?   self :
                                    self.IsAstParentCalculated  ?   self.AstParent :
                                                                    UnparsableObject.NonCalculated;     // NOTE: if NonCalculated then it will be calculated later
            }

            if (!IsPrevMostChildCalculated(self))
            {
                tsUnparse.Debug("{2} of {0} has been set to {1}", self, child, direction == Direction.LeftToRight ? "LeftMostChild" : "RightMostChild");
                SetPrevMostChild(self, child);
            }

            if (child == null)
            {
                tsUnparse.Debug("{2} of {0} has been set to {1}", self, childPrevSibling, direction == Direction.LeftToRight ? "RightMostChild" : "LeftMostChild");
                SetNextMostChild(self, childPrevSibling);
            }

            if (child != null)
                SetPrevSibling(child, childPrevSibling);

            if (childPrevSibling != null)
            {
                // NOTE: if right-to-left then the next sibling is the left sibling, which is needed for deferred utokens even if we are not building full unparse tree
                if (buildFullUnparseTree || direction == Direction.RightToLeft)
                    SetNextSibling(childPrevSibling, child);  // we have the prev sibling of childPrevSibling set already, and now we set the next sibling too

                if (enableUnlinkOfChild)
                    UnlinkChildFromChildPrevSiblingIfNotFullUnparse(childPrevSibling);      // we unlink child prev sibling if unneeded
            }
        }

        private void UnlinkChildFromChildPrevSiblingIfNotFullUnparse(UnparsableObject child, bool enforce = false)
        {
            if (!enforce && direction == Direction.LeftToRight && child.IsLeftSiblingNeededForDeferredCalculation)
                return;

            if (!buildFullUnparseTree)
                SetPrevSibling(child, UnparsableObject.ThrownOut);  // prev sibling is not needed anymore
        }

        private void SetPrevSibling(UnparsableObject current, UnparsableObject prev)
        {
            if (direction == Direction.LeftToRight)
                current.LeftSibling = prev;
            else
                current.RightSibling = prev;
        }

        private void SetNextSibling(UnparsableObject current, UnparsableObject next)
        {
            if (direction == Direction.LeftToRight)
                current.RightSibling = next;
            else
                current.LeftSibling = next;
        }

        private void SetPrevMostChild(UnparsableObject self, UnparsableObject prevMostChild)
        {
            if (direction == Direction.LeftToRight)
                self.LeftMostChild = prevMostChild;
            else
                self.RightMostChild = prevMostChild;
        }

        private void SetNextMostChild(UnparsableObject self, UnparsableObject nextMostChild)
        {
            if (direction == Direction.LeftToRight)
                self.RightMostChild = nextMostChild;
            else
                self.LeftMostChild = nextMostChild;
        }

        private bool IsPrevMostChildCalculated(UnparsableObject self)
        {
            return direction == Direction.LeftToRight
                ? self.IsLeftMostChildCalculated
                : self.IsRightMostChildCalculated;
        }

        private bool IsNextMostChildCalculated(UnparsableObject self)
        {
            return direction == Direction.LeftToRight
                ? self.IsRightMostChildCalculated
                : self.IsLeftMostChildCalculated;
        }

        private static IEnumerable<UtokenBase> ConcatIfAnyMiddle(IEnumerable<UtokenBase> utokensBefore, IEnumerable<UtokenBase> utokensMiddle, IEnumerable<UtokenBase> utokensAfter)
        {
            bool isAnyUtokenMiddle = false;

            foreach (UtokenBase utokenMiddle in utokensMiddle)
            {
                if (!isAnyUtokenMiddle)
                {
                    foreach (UtokenBase utokenBefore in utokensBefore)
                        yield return utokenBefore;
                }

                isAnyUtokenMiddle = true;

                yield return utokenMiddle;
            }

            if (isAnyUtokenMiddle)
            {
                foreach (UtokenBase utokenAfter in utokensAfter)
                    yield return utokenAfter;
            }
        }

        private IEnumerable<UnparsableObject> ChooseChildrenByPriority(UnparsableObject self)
        {
            // TODO: we should check whether any bnfTermList has an UnparseHint

            IUnparsableNonTerminal unparsable = (IUnparsableNonTerminal)self.BnfTerm;

            tsPriorities.Debug("{0} BEGIN priorities", unparsable.AsNonTerminal());
            tsPriorities.Indent();

            try
            {
                return GetChildBnfTermLists(unparsable.AsNonTerminal())
                    .Select(childBnfTerms =>
                        {
                            var children = unparsable.GetChildren(childBnfTerms, self.AstValue, direction);
                            return new
                            {
                                Children = children,
                                Priority = unparsable.GetChildrenPriority(this, self.AstValue, children)
                                    .DebugWriteLinePriority(tsPriorities, self)
                            };
                        }
                    )
                    .Where(childrenWithPriority => childrenWithPriority.Priority.HasValue && !childrenWithPriority.Children.Contains(self))
                    .MaxItem(childrenWithPriority => childrenWithPriority.Priority.Value)
                    .Children;
            }
            catch (InvalidOperationException)
            {
                // MaxItem got an empty children list because no children remained after filtering the children list -> unparse error
                throw new UnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' has no appropriate production rule.",
                    self.AstValue, self.AstValue.GetType().Name, self.BnfTerm.Name));
            }
            finally
            {
                tsPriorities.Unindent();
                tsPriorities.Debug("{0} END priorities", unparsable.AsNonTerminal());
                tsPriorities.Debug("");
            }
        }

        internal static IEnumerable<IList<BnfTerm>> GetChildBnfTermListsLeftToRight(NonTerminal nonTerminal)
        {
            return nonTerminal.Productions.Select(production => production.RValues);
        }

        internal IEnumerable<IList<BnfTerm>> GetChildBnfTermLists(NonTerminal nonTerminal)
        {
            return GetChildBnfTermListsLeftToRight(nonTerminal)
                .Select(bnfTerms => direction == Direction.LeftToRight ? bnfTerms : bnfTerms.ReverseOptimized());
        }

        private bool buildFullUnparseTree { get { return EnablePartialInvalidation; } }

        private string GetLexemeByValue(ConstantTerminal constantTerminal, object value)
        {
            Dictionary<object, string> constantTable;

            if (constantTerminalToInverseConstantTable.TryGetValue(constantTerminal, out constantTable))
                return constantTable[value];
            else
            {
                lock (constantTerminalToInverseConstantTable)
                {
                    /*
                     * We don't want to lock on constantTerminalToInverseConstantTable at the beginning of this method,
                     * because it would slow down parallel processing (note: parallel unparsers share this data member),
                     * and it's not necessary since most of the time we only read this data member, not write.
                     * 
                     * However, after we found that we hadn't stored constantTerminal yet, and locked on constantTerminalToInverseConstantTable,
                     * we have to check again for constantTerminal, to avoid the following scenario:
                     * 
                     * Task1: check
                     * Task1: no constantTerminal found
                     * Task2: check
                     * Task2: no constantTerminal found
                     * Task1: lock
                     * Task1: store constantTerminal
                     * Task2: lock
                     * Task2: store constantTerminal (!!! STORED TWICE !!!)
                     * 
                     * */

                    if (constantTerminalToInverseConstantTable.TryGetValue(constantTerminal, out constantTable))
                        return constantTable[value];
                    else
                    {
                        /*
                         * NOTE: constantTerminal.Constants might not be bijective
                         * */

                        constantTable = new Dictionary<object, string>();

                        foreach (var pair in constantTerminal.Constants)
                        {
                            if (!constantTable.ContainsKey(pair.Value))
                                constantTable.Add(pair.Value, pair.Key);
                        }

                        constantTerminalToInverseConstantTable.Add(constantTerminal, constantTable);

                        return constantTable[value];
                    }
                }
            }
        }

        #endregion

        #region IUnparser implementation

        int? IUnparser.GetPriority(UnparsableObject unparsableObject)
        {
            IUnparsableNonTerminal unparsable = unparsableObject.BnfTerm as IUnparsableNonTerminal;

            if (unparsable != null)
            {

                tsPriorities.Indent();

                int? priority = GetChildBnfTermLists(unparsable.AsNonTerminal())
                    .Max(childBnfTerms => unparsable.GetChildrenPriority(this, unparsableObject.AstValue, unparsable.GetChildren(childBnfTerms, unparsableObject.AstValue, direction))
                        .DebugWriteLinePriority(tsPriorities, unparsableObject));

                tsPriorities.Unindent();

                priority.DebugWriteLinePriority(tsPriorities, unparsableObject, messageAfter: " (MAX)");

                return priority;
            }
            else
            {
                Misc.DebugWriteLinePriority(0, tsPriorities, unparsableObject, messageAfter: " (for terminal)");
                return 0;
            }
        }

        IFormatProvider IUnparser.FormatProvider { get { return this.Formatting.FormatProvider; } }

        #endregion

        #region IPostProcessHelper implementation

        Formatting IPostProcessHelper.Formatting { get { return this.Formatting; } }

        Unparser.Direction IPostProcessHelper.Direction
        {
            get { return this.direction; }
        }

        bool IPostProcessHelper.IndentEmptyLines
        {
            get { return formatting.IndentEmptyLines; }
        }

        Action<UnparsableObject> IPostProcessHelper.UnlinkChildFromChildPrevSiblingIfNotFullUnparse
        {
            get
            {
                return child =>
                    {
                        if (direction == Direction.LeftToRight && child.IsLeftSiblingNeededForDeferredCalculation)
                            this.UnlinkChildFromChildPrevSiblingIfNotFullUnparse(child, enforce: true);
                    };
            }
        }

        #endregion

        #region Types

        public enum Direction { LeftToRight, RightToLeft }

        #endregion
    }
}
