﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Globalization;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.Ast;

using Grammar = Sarcasm.Ast.Grammar;

namespace Sarcasm.Unparsing
{
    public class Formatting
    {
        #region Types

        private class _AnyBnfTerm : BnfTerm
        {
            private _AnyBnfTerm()
                : base("AnyBnfTerm")
            {
            }

            public static readonly _AnyBnfTerm Instance = new _AnyBnfTerm();
        }

        #endregion

        #region Default values

        private const double priorityDefault = 0;
        private const double anyPriorityDefault = double.NegativeInfinity;

        private const bool overridableDefault = true;
        private const bool anyOverridableDefault = true;

        private const string newLineDefault = "\n";
        private const string spaceDefault = " ";
        private const string tabDefault = "\t";
        private static readonly string indentUnitDefault = string.Concat(Enumerable.Repeat(spaceDefault, 4));
        private const string whiteSpaceBetweenUtokensDefault = spaceDefault;
        private static readonly IFormatProvider formatProviderDefault = CultureInfo.InvariantCulture;

        internal static BnfTerm AnyBnfTerm { get { return _AnyBnfTerm.Instance; } }

        #endregion

        #region State

        private readonly Grammar grammar;

        private IDictionary<BnfTerm, InsertedUtokens> bnfTermToUtokensBefore = new Dictionary<BnfTerm, InsertedUtokens>();
        private IDictionary<BnfTerm, InsertedUtokens> bnfTermToUtokensAfter = new Dictionary<BnfTerm, InsertedUtokens>();
        private IDictionary<Tuple<BnfTerm, BnfTerm>, InsertedUtokens> bnfTermToUtokensBetween = new Dictionary<Tuple<BnfTerm, BnfTerm>, InsertedUtokens>();
        private ISet<BnfTerm> leftBnfTerms = new HashSet<BnfTerm>();

        #endregion

        #region Construction

        public Formatting()
            : this(grammar: null)
        {
        }

        protected Formatting(Grammar grammar)
        {
            this.grammar = grammar;

            this.NewLine = newLineDefault;
            this.Space = spaceDefault;
            this.Tab = tabDefault;
            this.IndentUnit = indentUnitDefault;
            this.WhiteSpaceBetweenUtokens = whiteSpaceBetweenUtokensDefault;

            if (this.FormatProvider == null)
                this.FormatProvider = formatProviderDefault;
        }

        internal static Formatting CreateDefaultFormattingForGrammar(Grammar grammar)
        {
            return new Formatting(grammar);
        }

        #endregion

        #region Interface to grammar

        #region Settings

        public string NewLine { get; set; }
        public string Space { get; set; }
        public string Tab { get; set; }
        public string IndentUnit { get; set; }
        public string WhiteSpaceBetweenUtokens { get; set; }

        IFormatProvider formatProvider = null;

        public IFormatProvider FormatProvider
        {
            get
            {
                return grammar != null ? grammar.DefaultCulture : this.formatProvider;
            }
            set
            {
                if (grammar != null)
                    grammar.DefaultCulture = (CultureInfo)value;    // note: this will throw an InvalidCastException if the value is not a CultureInfo
                else
                    this.formatProvider = value;
            }
        }

        #endregion

        #region Insert utokens

        public void InsertUtokensBeforeAny(params Utoken[] utokensBefore)
        {
            InsertUtokensBefore(AnyBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensBefore: utokensBefore);
        }

        public void InsertUtokensBefore(BnfTerm bnfTerm, params Utoken[] utokensBefore)
        {
            InsertUtokensBefore(bnfTerm, priority: priorityDefault, overridable: overridableDefault, utokensBefore: utokensBefore);
        }

        public void InsertUtokensBefore(BnfTerm bnfTerm, double priority, bool overridable, params Utoken[] utokensBefore)
        {
            bnfTermToUtokensBefore.Add(bnfTerm, new InsertedUtokens(InsertedUtokens.Kind.Before, priority, overridable, utokensBefore, bnfTerm));
        }

        public void InsertUtokensAfterAny(params Utoken[] utokensAfter)
        {
            InsertUtokensAfter(AnyBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensAfter: utokensAfter);
        }

        public void InsertUtokensAfter(BnfTerm bnfTerm, params Utoken[] utokensAfter)
        {
            InsertUtokensAfter(bnfTerm, priority: priorityDefault, overridable: overridableDefault, utokensAfter: utokensAfter);
        }

        public void InsertUtokensAfter(BnfTerm bnfTerm, double priority, bool overridable, params Utoken[] utokensAfter)
        {
            bnfTermToUtokensAfter.Add(bnfTerm, new InsertedUtokens(InsertedUtokens.Kind.After, priority, overridable, utokensAfter, bnfTerm));
        }

        public void InsertUtokensAroundAny(params Utoken[] utokensAround)
        {
            InsertUtokensAround(AnyBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensAround: utokensAround);
        }

        public void InsertUtokensAround(BnfTerm bnfTerm, params Utoken[] utokensAround)
        {
            InsertUtokensAround(bnfTerm, priority: priorityDefault, overridable: overridableDefault, utokensAround: utokensAround);
        }

        public void InsertUtokensAround(BnfTerm bnfTerm, double priority, bool overridable, params Utoken[] utokensAround)
        {
            InsertUtokensBefore(bnfTerm, priority, overridable, utokensAround);
            InsertUtokensAfter(bnfTerm, priority, overridable, utokensAround);
        }

        public void InsertUtokensBetweenOrderedLeftAndAny(BnfTerm leftBnfTerm, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenOrdered(leftBnfTerm, AnyBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenOrderedAnyAndRight(BnfTerm rightBnfTerm, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenOrdered(AnyBnfTerm, rightBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenAny(params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenOrdered(AnyBnfTerm, AnyBnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenUnorderedAnyAndOther(BnfTerm bnfTerm, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenUnordered(AnyBnfTerm, bnfTerm, priority: anyPriorityDefault, overridable: anyOverridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenOrdered(BnfTerm leftBnfTerm, BnfTerm rightBnfTerm, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenOrdered(leftBnfTerm, rightBnfTerm, priority: priorityDefault, overridable: overridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenOrdered(BnfTerm leftBnfTerm, BnfTerm rightBnfTerm, double priority, bool overridable, params Utoken[] utokensBetween)
        {
            bnfTermToUtokensBetween.Add(
                Tuple.Create(leftBnfTerm, rightBnfTerm),
                new InsertedUtokens(InsertedUtokens.Kind.Between, priority, overridable, utokensBetween, leftBnfTerm, rightBnfTerm)
                );

            leftBnfTerms.Add(leftBnfTerm);
        }

        public void InsertUtokensBetweenUnordered(BnfTerm bnfTerm1, BnfTerm bnfTerm2, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenUnordered(bnfTerm1, bnfTerm2, priority: priorityDefault, overridable: overridableDefault, utokensBetween: utokensBetween);
        }

        public void InsertUtokensBetweenUnordered(BnfTerm bnfTerm1, BnfTerm bnfTerm2, double priority, bool overridable, params Utoken[] utokensBetween)
        {
            InsertUtokensBetweenOrdered(bnfTerm1, bnfTerm2, priority, overridable, utokensBetween);
            InsertUtokensBetweenOrdered(bnfTerm2, bnfTerm1, priority, overridable, utokensBetween);
        }

        #endregion

        #endregion

        #region Interface to unparser

        internal bool HasUtokensBefore(BnfTerm bnfTerm, out InsertedUtokens insertedUtokensBefore)
        {
            return bnfTermToUtokensBefore.TryGetValue(bnfTerm, out insertedUtokensBefore)
                || bnfTermToUtokensBefore.TryGetValue(AnyBnfTerm, out insertedUtokensBefore);
        }

        internal bool HasUtokensAfter(BnfTerm bnfTerm, out InsertedUtokens insertedUtokensAfter)
        {
            return bnfTermToUtokensAfter.TryGetValue(bnfTerm, out insertedUtokensAfter)
                || bnfTermToUtokensAfter.TryGetValue(AnyBnfTerm, out insertedUtokensAfter);
        }

        internal bool HasUtokensBetween(BnfTerm leftBnfTerm, BnfTerm rightBnfTerm, out InsertedUtokens insertedUtokensBetween)
        {
            return bnfTermToUtokensBetween.TryGetValue(Tuple.Create(leftBnfTerm, rightBnfTerm), out insertedUtokensBetween)
                || bnfTermToUtokensBetween.TryGetValue(Tuple.Create(leftBnfTerm, AnyBnfTerm), out insertedUtokensBetween)
                || bnfTermToUtokensBetween.TryGetValue(Tuple.Create(AnyBnfTerm, rightBnfTerm), out insertedUtokensBetween)
                || bnfTermToUtokensBetween.TryGetValue(Tuple.Create(AnyBnfTerm, AnyBnfTerm), out insertedUtokensBetween);
        }

        internal bool IsLeftBnfTermOfABetweenPair(BnfTerm leftBnfTerm)
        {
            return leftBnfTerms.Contains(leftBnfTerm);
        }

    	#endregion
    }
}