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

namespace Irony.ITG
{
    public delegate T AstValueCreator<T>(AstContext context, ParseTreeNodeWithOutAst parseNode);
    public delegate TOut ValueConverter<TIn, TOut>(TIn inputObject);

    public interface IBnfTerm
    {
        BnfTerm AsBnfTerm();
    }

    public interface IBnfTerm<out T> : IBnfTerm
    {
    }

    public interface INonTerminalWithSingleTypesafeRule<T>
    {
        //BnfExpression RuleTL { get; set; }
        //BnfExpression<T> Rule { set; }
    }

    public interface INonTerminalWithMultipleTypesafeRule<T> : INonTerminalWithSingleTypesafeRule<T>
    {
    }

    public interface ITransientWithSingleTypesafeRule<T>
    {
        BnfExpression RuleTL { get; set; }
        BnfExpressionTransient<T> Rule { set; }
    }

    public interface ITransientWithMultipleTypesafeRule<T> : ITransientWithSingleTypesafeRule<T>
    {
    }

    public abstract class TypeForNonTerminal : NonTerminal
    {
        protected Type type { get; private set; }

        protected TypeForNonTerminal(Type type, string errorAlias)
            : base(GrammarHelper.TypeNameWithDeclaringTypes(type), errorAlias)
        {
            this.type = type;
        }

        protected static BnfExpression GetRuleWithOrBetweenTypesafeExpressions<T>(params IBnfTerm<T>[] bnfExpressions)
        {
            return bnfExpressions.Cast<BnfExpression>().Aggregate(
                (BnfExpression)bnfExpressions[0],
                (bnfExpressionProcessed, bnfExpressionToBeProcess) => bnfExpressionProcessed | bnfExpressionToBeProcess
                );
        }

        internal const string typelessQErrorMessage = "Use the typesafe QVal or QRef extension methods combined with CreateValue or ConvertValue extension methods instead";
        internal const string typelessMemberBoundErrorMessage = "Typeless MemberBoundToBnfTerm should not mix with typesafe MemberBoundToBnfTerm<TDeclaringType>";
        internal const string invalidUseOfNonExistingTypesafePipeOperatorErrorMessage = "There is no typesafe pipe operator for different types. Use SetRuleOr method instead.";
    }
}
