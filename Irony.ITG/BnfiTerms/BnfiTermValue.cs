﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using System.IO;

namespace Irony.ITG
{
    public partial class BnfiTermValue : BnfiTermNonTerminal, IBnfiTerm
    {
        public BnfiTermValue(Type type, string errorAlias = null)
            : this(type, errorAlias, (context, parseTreeNode) => parseTreeNode.ChildNodes[0].AstNode)   // default "transient" astValueCreator
        {
        }

        protected BnfiTermValue(Type type, BnfTerm bnfTerm, AstValueCreator<object> astValueCreator, bool isOptionalData, string errorAlias, bool astForChild)
            : this(type, errorAlias, astValueCreator)
        {
            if (!astForChild)
                bnfTerm.Flags |= TermFlags.NoAstNode;

            this.RuleTL = isOptionalData
                ? bnfTerm | Irony.Parsing.Grammar.CurrentGrammar.Empty
                : new BnfExpression(bnfTerm);
        }

        private BnfiTermValue(Type type, string errorAlias, AstValueCreator<object> astValueCreator)
            : base(type, errorAlias)
        {
            this.AstConfig.NodeCreator = (context, parseTreeNode) =>
                parseTreeNode.AstNode = GrammarHelper.ValueToAstNode(astValueCreator(context, new ParseTreeNodeWithOutAst(parseTreeNode)), context, parseTreeNode);
        }

        public static BnfiTermValue Create(Type type, Terminal terminal, object value, bool astForChild = true)
        {
            return new BnfiTermValue(type, terminal, (context, parseNode) => value, isOptionalData: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue Create(Type type, Terminal terminal, AstValueCreator<object> astValueCreator, bool astForChild = true)
        {
            return new BnfiTermValue(type, terminal, (context, parseNode) => astValueCreator(context, parseNode), isOptionalData: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue<T> Create<T>(Terminal terminal, T value, bool astForChild = true)
        {
            return new BnfiTermValue<T>(terminal, (context, parseNode) => value, isOptionalData: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue<T> Create<T>(Terminal terminal, AstValueCreator<T> astValueCreator, bool astForChild = true)
        {
            return new BnfiTermValue<T>(terminal, (context, parseNode) => astValueCreator(context, parseNode), isOptionalData: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue<string> CreateIdentifier(IdentifierTerminal identifierTerminal)
        {
            return Create<string>(identifierTerminal, (context, parseNode) => parseNode.FindTokenAndGetText(), astForChild: false);
        }

        public static BnfiTermValue<T> CreateNumber<T>(NumberLiteral numberLiteral)
        {
            return Create<T>(numberLiteral, (context, parseNode) => (T)parseNode.FindToken().Value, astForChild: false);
        }

        public static BnfiTermValue Convert(Type type, BnfTerm bnfTerm, ValueConverter<object, object> valueConverter)
        {
            return new BnfiTermValue(
                type,
                bnfTerm,
                (context, parseTreeNode) =>
                    {
                        if (parseTreeNode.ChildNodes.Count != 1)
                            throw new ArgumentException("Only one child is allowed for a TypeForValue term: {0}", parseTreeNode.Term.Name);

                        return valueConverter(GrammarHelper.AstNodeToValue<object>(parseTreeNode.ChildNodes[0].AstNode));
                    },
                isOptionalData: false,
                errorAlias: null,
                astForChild: true
                );
        }

        public static BnfiTermValue<TOut> Convert<TIn, TOut>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter)
        {
            return new BnfiTermValue<TOut>(
                bnfTerm.AsBnfTerm(),
                (context, parseTreeNode) =>
                    {
                        if (parseTreeNode.ChildNodes.Count != 1)
                            throw new ArgumentException("Only one child is allowed for a TypeForValue term: {0}", parseTreeNode.Term.Name);

                        return valueConverter(GrammarHelper.AstNodeToValue<TIn>(parseTreeNode.ChildNodes[0].AstNode));
                    },
                isOptionalData: false,
                errorAlias: null,
                astForChild: true
                );
        }

        public static BnfiTermValue<TOut> Cast<TIn, TOut>(IBnfiTerm<TIn> bnfTerm)
        {
            return Convert(bnfTerm, inValue => (TOut)(object)inValue);
        }

        public static BnfiTermValue<TOut> Cast<TOut>(Terminal terminal)
        {
            return Create<TOut>(terminal, (context, parseNode) => (TOut)GrammarHelper.AstNodeToValue<object>(parseNode.Token.Value));
        }

        public static BnfiTermValue<T?> ConvertValueOptVal<T>(IBnfiTerm<T> bnfTerm)
            where T : struct
        {
            return ConvertValueOptVal(bnfTerm, value => value);
        }

        public static BnfiTermValue<TOut?> ConvertValueOptVal<TIn, TOut>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter)
            where TIn : struct
            where TOut : struct
        {
            return ConvertValueOpt<TIn, TOut?>(bnfTerm, value => valueConverter(value));
        }

        public static BnfiTermValue<T> ConvertValueOptRef<T>(IBnfiTerm<T> bnfTerm)
            where T : class
        {
            return ConvertValueOptRef(bnfTerm, value => value);
        }

        public static BnfiTermValue<TOut> ConvertValueOptRef<TIn, TOut>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter)
            where TIn : class
            where TOut : class
        {
            return ConvertValueOpt<TIn, TOut>(bnfTerm, value => valueConverter(value));
        }

        private static BnfiTermValue<TOutData> ConvertValueOpt<TIn, TOutData>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOutData> valueConverter)
        {
            return new BnfiTermValue<TOutData>(
                bnfTerm.AsBnfTerm(),
                (context, parseNode) =>
                {
                    TIn value = GrammarHelper.AstNodeToValue<TIn>(parseNode.ChildNodes.FirstOrDefault(parseTreeChild => parseTreeChild.Term == bnfTerm).AstNode);
                    return valueConverter(value);
                },
                isOptionalData: true,
                errorAlias: null,
                astForChild: true
                );
        }

        protected BnfExpression RuleTL { get { return base.Rule; } set { base.Rule = value; } }

        public new BnfiExpressionValue Rule { set { base.Rule = value; } }

        public BnfTerm AsBnfTerm()
        {
            return this;
        }
    }

    public partial class BnfiTermValue<T> : BnfiTermValue, IBnfiTerm<T>
    {
        public BnfiTermValue(string errorAlias = null)
            : base(typeof(T), errorAlias)
        {
        }

        internal BnfiTermValue(BnfTerm bnfTerm, AstValueCreator<T> astValueCreator, bool isOptionalData, string errorAlias, bool astForChild)
            : base(typeof(T), bnfTerm, (context, parseNode) => astValueCreator(context, parseNode), isOptionalData, errorAlias, astForChild)
        {
        }

        public new BnfiExpressionValue<T> Rule { set { base.Rule = value; } }

        [Obsolete(BnfiTermNonTerminal.typelessQErrorMessage, error: true)]
        public new BnfExpression Q()
        {
            return base.Q();
        }
    }
}