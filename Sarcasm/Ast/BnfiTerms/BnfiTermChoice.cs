﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.IO;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.Unparsing;

namespace Sarcasm.Ast
{
    public partial class BnfiTermChoice : BnfiTermNonTerminal, IBnfiTerm, IUnparsable
    {
        public BnfiTermChoice(Type type, string errorAlias = null)
            : base(type, errorAlias)
        {
            GrammarHelper.MarkTransient(this);      // the child node already contains the created ast node
        }

        public new BnfiExpressionChoice Rule { set { base.Rule = value; } }

        public BnfExpression RuleRaw { get { return base.Rule; } set { base.Rule = value; } }

        BnfTerm IBnfiTerm.AsBnfTerm()
        {
            return this;
        }

        #region Unparse

        bool IUnparsable.TryGetUtokensDirectly(IUnparser unparser, object obj, out IEnumerable<Utoken> utokens)
        {
            utokens = null;
            return false;
        }

        IEnumerable<Value> IUnparsable.GetChildValues(BnfTermList childBnfTerms, object obj)
        {
            return childBnfTerms
                .Select(childBnfTerm =>
                    new Value(
                        childBnfTerm,
                        IsMainChild(childBnfTerm) ? obj : null
                        )
                    );
        }

        int? IUnparsable.GetChildBnfTermListPriority(IUnparser unparser, object obj, IEnumerable<Value> childValues)
        {
            BnfTerm mainChildBnfTerm = childValues.Single(childValue => IsMainChild(childValue.bnfTerm)).bnfTerm;

            if (obj.GetType() == this.Type)
                return unparser.GetBnfTermPriority(mainChildBnfTerm, obj);
            else
            {
                IHasType mainChildBnfTermWithType = mainChildBnfTerm as IHasType;

                if (mainChildBnfTermWithType == null)
                {
                    throw new CannotUnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' is not a BnfiTermNonTerminal.",
                        obj, obj.GetType().Name, mainChildBnfTerm.Name));
                }

                int? priority = 0 - mainChildBnfTermWithType.Type.GetInheritanceDistance(obj);

                Unparser.tsPriorities.Indent();
                priority.DebugWriteLinePriority(Unparser.tsPriorities, mainChildBnfTerm, obj);
                Unparser.tsPriorities.Unindent();

                return priority;
            }
        }

        private static bool IsMainChild(BnfTerm bnfTerm)
        {
            return !bnfTerm.Flags.IsSet(TermFlags.IsPunctuation) && !(bnfTerm is GrammarHint);
        }

        #endregion
    }

    public partial class BnfiTermChoice<TType> : BnfiTermChoice, IBnfiTerm<TType>
    {
        public BnfiTermChoice(string errorAlias = null)
            : base(typeof(TType), errorAlias)
        {
        }

        [Obsolete(typelessQErrorMessage, error: true)]
        public new BnfExpression Q()
        {
            return base.Q();
        }

        public BnfiExpressionChoice RuleTypeless { set { base.Rule = value; } }

        public new BnfiExpressionChoice<TType> Rule { set { base.Rule = value; } }

        public void SetRuleOr(IBnfiTerm<TType> bnfiTermFirst, params IBnfiTerm<TType>[] bnfiTerms)
        {
            this.Rule = Or(bnfiTermFirst, bnfiTerms);
        }

        public BnfiExpressionChoice<TType> Or(IBnfiTerm<TType> bnfiTermFirst, params IBnfiTerm<TType>[] bnfiTerms)
        {
            return (BnfiExpressionChoice<TType>)bnfiTerms
                .Select(bnfiTerm => bnfiTerm.AsBnfTerm())
                .Aggregate(
                new BnfExpression(bnfiTermFirst.AsBnfTerm()),
                (bnfExpressionProcessed, bnfTermToBeProcess) => bnfExpressionProcessed | bnfTermToBeProcess
                );
        }
    }
}
