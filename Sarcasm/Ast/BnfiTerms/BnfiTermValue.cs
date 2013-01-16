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
using Sarcasm.Unparsing;

namespace Sarcasm.Ast
{
    public partial class BnfiTermValue : BnfiTermNonTerminal, IBnfiTerm, IUnparsable
    {
        private BnfTerm bnfTerm;
        private object value;

        public ValueConverter<object, object> InverseValueConverterForUnparse { private get; set; }
        public ValueUtokenizer<object> UtokenizerForUnparse { private get; set; }

        private readonly bool movable = true;

        public BnfiTermValue(string errorAlias = null)
            : this(typeof(object), errorAlias)
        {
            this.movable = false;
        }

        public BnfiTermValue(Type type, string errorAlias = null)
            : base(type, errorAlias)
        {
            this.movable = false;
            GrammarHelper.MarkTransientForced(this);    // default "transient" behavior (the Rule of this BnfiTermValue will contain the BnfiTermValue which actually does something)
            this.InverseValueConverterForUnparse = IdentityFunction;
        }

        protected BnfiTermValue(Type type, BnfTerm bnfTerm, object value, bool isOptionalValue, string errorAlias, bool astForChild)
            : this(type, bnfTerm, (context, parseNode) => value, isOptionalValue, errorAlias, astForChild)
        {
            this.value = value;
        }

        protected BnfiTermValue(Type type, BnfTerm bnfTerm, ValueParser<object> valueParser, bool isOptionalValue, string errorAlias, bool astForChild)
            : base(type, errorAlias)
        {
            this.bnfTerm = bnfTerm;

            if (!astForChild)
                bnfTerm.Flags |= TermFlags.NoAstNode;

            this.RuleRaw = isOptionalValue
                ? bnfTerm | Irony.Parsing.Grammar.CurrentGrammar.Empty
                : new BnfExpression(bnfTerm);

            this.AstConfig.NodeCreator = (context, parseTreeNode) =>
                parseTreeNode.AstNode = GrammarHelper.ValueToAstNode(valueParser(context, new ParseTreeNodeWithOutAst(parseTreeNode)), context, parseTreeNode);
        }

        public static BnfiTermValue Parse(Terminal terminal, object value, bool astForChild = true)
        {
            return Parse(typeof(object), terminal, value, astForChild);
        }

        public static BnfiTermValue Parse(Type type, Terminal terminal, object value, bool astForChild = true)
        {
            BnfiTermValue bnfiTermValue = new BnfiTermValue(type, terminal, value, isOptionalValue: false, errorAlias: null, astForChild: astForChild);
            bnfiTermValue.InverseValueConverterForUnparse = IdentityFunction;
            return bnfiTermValue;
        }

        public static BnfiTermValue Parse(Terminal terminal, ValueParser<object> valueParser, bool astForChild = true)
        {
            return Parse(typeof(object), terminal, valueParser, astForChild);
        }

        public static BnfiTermValue Parse(Type type, Terminal terminal, ValueParser<object> valueParser, bool astForChild = true)
        {
            return new BnfiTermValue(type, terminal, (context, parseNode) => valueParser(context, parseNode), isOptionalValue: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue<T> Parse<T>(Terminal terminal, T value, bool astForChild = true)
        {
            BnfiTermValue<T> bnfiTermValue = new BnfiTermValue<T>(terminal, value, isOptionalValue: false, errorAlias: null, astForChild: astForChild);
            bnfiTermValue.InverseValueConverterForUnparse = IdentityFunctionForceCast<T, object>;
            return bnfiTermValue;
        }

        public static BnfiTermValue<T> Parse<T>(Terminal terminal, ValueParser<T> valueParser, bool astForChild = true)
        {
            return new BnfiTermValue<T>(terminal, (context, parseNode) => valueParser(context, parseNode), isOptionalValue: false, errorAlias: null, astForChild: astForChild);
        }

        public static BnfiTermValue<string> ParseIdentifier(IdentifierTerminal identifierTerminal)
        {
            BnfiTermValue<string> bnfiTermValue = Parse<string>(identifierTerminal, (context, parseNode) => parseNode.FindTokenAndGetText(), astForChild: false);
            bnfiTermValue.InverseValueConverterForUnparse = IdentityFunctionForceCast<string, object>;
            return bnfiTermValue;
        }

        public static BnfiTermValue Convert(IBnfiTerm bnfiTerm, ValueConverter<object, object> valueConverter)
        {
            return Convert(typeof(object), bnfiTerm, valueConverter);
        }

        public static BnfiTermValue Convert(Type type, IBnfiTerm bnfiTerm, ValueConverter<object, object> valueConverter)
        {
            return new BnfiTermValue(
                type,
                bnfiTerm.AsBnfTerm(),
                ConvertValueConverterToValueParser(valueConverter),
                isOptionalValue: false,
                errorAlias: null,
                astForChild: true
                );
        }

        public static BnfiTermValue<TOut> Convert<TIn, TOut>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter)
        {
            return new BnfiTermValue<TOut>(
                bnfTerm.AsBnfTerm(),
                ConvertValueConverterToValueParser(valueConverter),
                isOptionalValue: false,
                errorAlias: null,
                astForChild: true
                );
        }

        public static BnfiTermValue<TOut> Convert<TOut>(IBnfiTerm bnfiTerm, ValueConverter<object, TOut> valueConverter)
        {
            return new BnfiTermValue<TOut>(
                bnfiTerm.AsBnfTerm(),
                ConvertValueConverterToValueParser(valueConverter),
                isOptionalValue: false,
                errorAlias: null,
                astForChild: true
                );
        }

        public static BnfiTermValue<TOut> Cast<TIn, TOut>(IBnfiTerm<TIn> bnfTerm)
        {
            BnfiTermValue<TOut> bnfiTermValue = Convert(bnfTerm, value => IdentityFunctionForceCast<TIn, TOut>(value));
            bnfiTermValue.InverseValueConverterForUnparse = value => IdentityFunctionForceCast<TOut, TIn>(value);
            return bnfiTermValue;
        }

        public static BnfiTermValue<TOut> Cast<TOut>(Terminal terminal)
        {
            BnfiTermValue<TOut> bnfiTermValue = Parse<TOut>(terminal, (context, parseNode) => (TOut)GrammarHelper.AstNodeToValue(parseNode.Token.Value));
            bnfiTermValue.InverseValueConverterForUnparse = IdentityFunctionForceCast<TOut, object>;
            return bnfiTermValue;
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
            return ConvertValueOpt<TIn, TOut>(bnfTerm, valueConverter);
        }

        public static BnfiTermValue<T> ConvertValueOptVal<T>(IBnfiTerm<T> bnfiTerm, T defaultValue)
            where T : struct
        {
            return ConvertValueOptVal(bnfiTerm, value => value, defaultValue);
        }

        public static BnfiTermValue<TOut> ConvertValueOptVal<TIn, TOut>(IBnfiTerm<TIn> bnfiTerm, ValueConverter<TIn, TOut> valueConverter, TOut defaultValue)
            where TIn : struct
            where TOut : struct
        {
            return ConvertValueOpt<TIn, TOut>(bnfiTerm, valueConverter, defaultValue);
        }

        public static BnfiTermValue<T> ConvertValueOptRef<T>(IBnfiTerm<T> bnfiTerm, T defaultValue)
            where T : class
        {
            return ConvertValueOptRef(bnfiTerm, value => value, defaultValue);
        }

        public static BnfiTermValue<TOut> ConvertValueOptRef<TIn, TOut>(IBnfiTerm<TIn> bnfiTerm, ValueConverter<TIn, TOut> valueConverter, TOut defaultValue)
            where TIn : class
            where TOut : class
        {
            return ConvertValueOptRef(bnfiTerm, valueConverter, defaultValue);
        }

        protected static BnfiTermValue<TOut> ConvertValueOpt<TIn, TOut>(IBnfiTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter, TOut defaultValue = default(TOut))
        {
            return new BnfiTermValue<TOut>(
                bnfTerm.AsBnfTerm(),
                ConvertValueConverterToValueParserOpt(valueConverter, defaultValue),
                isOptionalValue: true,
                errorAlias: null,
                astForChild: true
                );
        }

        protected static ValueParser<TOut> ConvertValueConverterToValueParser<TIn, TOut>(ValueConverter<TIn, TOut> valueConverter)
        {
            return ConvertValueConverterToValueParser(valueConverter, isOptionalValue: false, defaultValue: default(TOut));   // defaultValue won't be used
        }

        protected static ValueParser<TOut> ConvertValueConverterToValueParserOpt<TIn, TOut>(ValueConverter<TIn, TOut> valueConverter, TOut defaultValue)
        {
            return ConvertValueConverterToValueParser(valueConverter, isOptionalValue: true, defaultValue: defaultValue);
        }

        private static ValueParser<TOut> ConvertValueConverterToValueParser<TIn, TOut>(ValueConverter<TIn, TOut> valueConverter, bool isOptionalValue, TOut defaultValue)
        {
            return (context, parseTreeNode) =>
            {
                if (!isOptionalValue && parseTreeNode.ChildNodes.Count != 1)
                    throw new ArgumentException("Only one child is allowed for a non-optional BnfiTermValue term: {0}", parseTreeNode.Term.Name);
                else if (isOptionalValue && parseTreeNode.ChildNodes.Count > 1)
                    throw new ArgumentException("Only zero or one child is allowed for an optional BnfiTermValue term: {0}", parseTreeNode.Term.Name);

                ParseTreeNode parseTreeChild = parseTreeNode.ChildNodes.SingleOrDefault();

                return parseTreeChild != null
                    ? valueConverter(GrammarHelper.AstNodeToValue<TIn>(parseTreeChild.AstNode))
                    : defaultValue;
            };
        }

        protected static object IdentityFunction(object obj)
        {
            return obj;
        }

        protected static TOut IdentityFunctionForceCast<TIn, TOut>(TIn obj)
        {
            return (TOut)(object)obj;
        }

        protected void SetState(BnfiTermValue source)
        {
            this.bnfTerm = source.bnfTerm;
            this.value = source.value;
            this.Flags = source.Flags;
            this.AstConfig = source.AstConfig;

            this.InverseValueConverterForUnparse = source.InverseValueConverterForUnparse;

            if (this.UtokenizerForUnparse != null)
                this.UtokenizerForUnparse = source.UtokenizerForUnparse;
        }

        protected void ClearState()
        {
            this.bnfTerm = null;
            this.value = null;
            this.Flags = TermFlags.None;
            this.AstConfig = null;
            this.InverseValueConverterForUnparse = null;
            this.UtokenizerForUnparse = null;
        }

        protected void MoveTo(BnfiTermValue target)
        {
            if (!this.movable)
                GrammarHelper.ThrowGrammarErrorException(GrammarErrorLevel.Error, "This value should not be a right-value: {0}", this.Name);

            target.RuleRaw = this.RuleRaw;
            target.SetState(this);

            this.RuleRaw = null;
            this.ClearState();
        }

        protected BnfExpression RuleRaw { get { return base.Rule; } set { base.Rule = value; } }

        public new BnfiExpressionValue Rule
        {
            set
            {
                try
                {
                    /*
                     * Examine whether there is only one single BnfiTermValue inside 'value', and if it is true, then move the state of that BnfiTermValue
                     * so we can "destroy" that BnfiTermValue in order to eliminate unneccessary, extra "rule-embedding"
                     * 
                     * (note that if there is a single bnfTerm inside 'value' then it must be a BnfiTermValue (enforced by BnfiExpressionValue's typesafety),
                     * that's why we can cast here)
                     * */
                    BnfiTermValue onlyBnfTermInValue = (BnfiTermValue)value.GetBnfTermsList().Single().Single();
                    onlyBnfTermInValue.MoveTo(this);
                }
                catch (InvalidOperationException)
                {
                    // there are more than one bnfTerms inside 'value' -> "rule-embedding" is necessary
                    base.Rule = value;
                }
            }
        }

        BnfTerm IBnfiTerm.AsBnfTerm()
        {
            return this;
        }

        #region Unparse

        bool IUnparsable.TryGetUtokensDirectly(IUnparser unparser, object obj, out IEnumerable<Utoken> utokens)
        {
            if (this.UtokenizerForUnparse != null)
            {
                utokens = this.UtokenizerForUnparse(unparser.FormatProvider, obj);
                return true;
            }
            else if (this.InverseValueConverterForUnparse != null)
            {
                utokens = null;
                return false;
            }
            else
                throw new CannotUnparseException(string.Format("'{0}' has neither UtokenizerForUnparse nor ValueConverterForUnparse", this.Name));
        }

        IEnumerable<Value> IUnparsable.GetChildValues(BnfTermList childBnfTerms, object obj)
        {
            return childBnfTerms.Select(childBnfTerm => new Value(childBnfTerm, ConvertObjectForChild(obj, childBnfTerm)));
        }

        int? IUnparsable.GetChildBnfTermListPriority(IUnparser unparser, object obj, IEnumerable<Value> childValues)
        {
            if (this.value != null)
                return this.value.Equals(obj) ? (int?)1 : null;
            else
            {
                Value innerValue = GetMainChildValue(obj, childValues);
                return unparser.GetBnfTermPriority(innerValue.bnfTerm, innerValue.obj);
            }
        }

        private static bool IsMainChild(BnfTerm bnfTerm)
        {
            return !bnfTerm.Flags.IsSet(TermFlags.IsPunctuation) && !(bnfTerm is GrammarHint);
        }

        private Value GetMainChildValue(object obj, IEnumerable<Value> childValues)
        {
            return this.bnfTerm != null
                ? new Value(this.bnfTerm, ConvertObjectForChild(obj, this.bnfTerm))
                : childValues.Single(childValue => IsMainChild(childValue.bnfTerm));    // "transient" unparse with the actual BnfiTermValue(s) under the current one (set by Rule)
        }

        private object ConvertObjectForChild(object obj, BnfTerm childBnfTerm)
        {
            if (IsMainChild(childBnfTerm))
            {
                return this.InverseValueConverterForUnparse != null
                        ? this.InverseValueConverterForUnparse(obj)
                        : obj;
            }
            else
                return null;
        }

        #endregion

        public override string GetExtraStrForToString()
        {
            return string.Format("child bnfterm: {0}, value: {1}, utokenizer: {2}, inverse value converter: {3}",
                this.bnfTerm.Name, this.value != null ? value.ToString() : "<<NULL>>", this.UtokenizerForUnparse != null, this.InverseValueConverterForUnparse != null);
        }

        protected static ValueConverter<TInTo, object> CastValueConverter<TInFrom, TOutFrom, TInTo>(ValueConverter<TInFrom, TOutFrom> valueConverter)
        {
            return obj => valueConverter((TInFrom)(object)obj);
        }

        protected static ValueConverter<object, object> CastValueConverterToObject<TInFrom, TOutFrom>(ValueConverter<TInFrom, TOutFrom> valueConverter)
        {
            return obj => valueConverter((TInFrom)obj);
        }

        protected static ValueUtokenizer<object> CastUtokenizerToObject<T>(ValueUtokenizer<T> utokenizer)
        {
            return (formatProvider, obj) => utokenizer(formatProvider, (T)obj);
        }
    }

    public partial class BnfiTermValue<T> : BnfiTermValue, IBnfiTerm<T>
    {
        public BnfiTermValue(string errorAlias = null)
            : base(typeof(T), errorAlias)
        {
            this.InverseValueConverterForUnparse = IdentityFunctionForceCast<T, object>;
        }

        internal BnfiTermValue(BnfTerm bnfTerm, T value, bool isOptionalValue, string errorAlias, bool astForChild)
            : base(typeof(T), bnfTerm, value, isOptionalValue, errorAlias, astForChild)
        {
        }

        internal BnfiTermValue(BnfTerm bnfTerm, ValueParser<T> valueParser, bool isOptionalValue, string errorAlias, bool astForChild)
            : base(typeof(T), bnfTerm, (context, parseNode) => valueParser(context, parseNode), isOptionalValue, errorAlias, astForChild)
        {
        }

        public new ValueConverter<T, object> InverseValueConverterForUnparse
        {
            set
            {
                base.InverseValueConverterForUnparse = CastValueConverterToObject(value);
            }
        }

        public new ValueUtokenizer<T> UtokenizerForUnparse
        {
            set
            {
                base.UtokenizerForUnparse = CastUtokenizerToObject(value);
            }
        }

        public BnfiExpressionValue RuleTypeless { set { base.Rule = value; } }

        public new BnfiExpressionValue<T> Rule { set { base.Rule = value; } }

        [Obsolete(BnfiTermNonTerminal.typelessQErrorMessage, error: true)]
        public new BnfExpression Q()
        {
            return base.Q();
        }
    }
}