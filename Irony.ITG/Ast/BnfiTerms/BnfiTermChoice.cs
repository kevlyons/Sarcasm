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
using Irony.ITG.Unparsing;

namespace Irony.ITG.Ast
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

        public IEnumerable<Utoken> Unparse(IUnparser unparser, object obj)
        {
            foreach (BnfTermList childBnfTerms in Unparser.GetChildBnfTermLists(this))
            {
                BnfTerm childBnfTermCandidate = childBnfTerms.Single(bnfTerm => !bnfTerm.Flags.IsSet(TermFlags.IsPunctuation) && !(bnfTerm is GrammarHint));

                IEnumerable<Utoken> utokens;

                if (obj.GetType() == this.Type)
                {
                    // we need to do the full unparse non-lazy in order to catch ValueMismatch error if any (that's why we use ToList here)
                    utokens = unparser.Unparse(obj, childBnfTermCandidate).ToList();

                    if (unparser.GetError() == Error.ValueMismatch)
                    {
                        // it is okay, keep trying with the others...
                        unparser.ClearError();
                        continue;
                    }
                    unparser.AssumeNoError();
                }
                else
                {
                    IHasType childCandidateWithType = childBnfTermCandidate as IHasType;

                    if (childCandidateWithType == null)
                        throw new CannotUnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' is not a BnfiTermNonTerminal.", obj, obj.GetType().Name, childBnfTermCandidate.Name));

                    if (!childCandidateWithType.Type.IsInstanceOfType(obj))
                    {
                        // keep trying with the others...
                        continue;
                    }

                    utokens = unparser.Unparse(obj, childBnfTermCandidate);
                }

                foreach (Utoken utoken in utokens)
                    yield return utoken;

                yield break;
            }

            throw new CannotUnparseException(string.Format("'{0}' cannot be unparsed.", this.Name));
        }
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