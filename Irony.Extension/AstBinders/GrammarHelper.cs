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

namespace Irony.Extension.AstBinders
{
    public static class GrammarHelper
    {
        #region StarList and PlusList

        #region Typesafe

        public static IBnfTerm<TCollectionType> StarList<TCollectionType, TElementType>(this IBnfTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType, TElementType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakeStarRule(typeForCollection, delimiter, bnfTermElement.AsTypeless()).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static IBnfTerm<List<TElementType>> StarList<TElementType>(this IBnfTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
        {
            return StarList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        public static IBnfTerm<TCollectionType> PlusList<TCollectionType, TElementType>(this IBnfTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType, TElementType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakePlusRule(typeForCollection, delimiter, bnfTermElement.AsTypeless()).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static IBnfTerm<List<TElementType>> PlusList<TElementType>(this IBnfTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        #endregion

        #region Typeless converted to typesafe

        public static TypeForCollection<TCollectionType, TElementType> StarList<TCollectionType, TElementType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType, TElementType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakeStarRule(typeForCollection, delimiter, bnfTermElement).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static TypeForCollection<List<TElementType>, TElementType> StarList<TElementType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return StarList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        public static TypeForCollection<TCollectionType, TElementType> PlusList<TCollectionType, TElementType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType, TElementType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakePlusRule(typeForCollection, delimiter, bnfTermElement).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static TypeForCollection<List<TElementType>, TElementType> PlusList<TElementType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        #endregion

        #region Typeless

        public static TypeForCollection<TCollectionType> StarListTL<TCollectionType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<object>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakeStarRule(typeForCollection, delimiter, bnfTermElement).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static TypeForCollection<List<object>> StarListTL(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return StarListTL<List<object>>(bnfTermElement, delimiter);
        }

        public static TypeForCollection<TCollectionType> PlusListTL<TCollectionType>(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<object>, new()
        {
            var typeForCollection = TypeForCollection.Of<TCollectionType>();
            typeForCollection.Rule = Grammar.CurrentGrammar.MakePlusRule(typeForCollection, delimiter, bnfTermElement).ToType<TCollectionType>();
            return typeForCollection;
        }

        public static TypeForCollection<List<object>> PlusListTL(this BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusListTL<List<object>>(bnfTermElement, delimiter);
        }

        #endregion

        #endregion

        #region BindMember

        public static MemberBoundToBnfTerm BindMember<TBnfTermType, TMemberType>(this IBnfTerm<TBnfTermType> bnfTerm, Expression<Func<TMemberType>> exprForFieldOrPropertyAccess)
            where TBnfTermType : TMemberType
        {
            return MemberBoundToBnfTerm.Bind(exprForFieldOrPropertyAccess, bnfTerm);
        }

        public static MemberBoundToBnfTerm<TDeclaringType> BindMember<TDeclaringType, TBnfTermType, TMemberType>(this IBnfTerm<TBnfTermType> bnfTerm,
            Expression<Func<TDeclaringType, TMemberType>> exprForFieldOrPropertyAccess)
            where TBnfTermType : TMemberType
        {
            return MemberBoundToBnfTerm.Bind<TDeclaringType, TMemberType, TBnfTermType>(exprForFieldOrPropertyAccess, bnfTerm);
        }

        public static MemberBoundToBnfTerm<TDeclaringType> BindMember<TDeclaringType, TBnfTermType, TMemberType>(this IBnfTerm<TBnfTermType> bnfTerm,
            IBnfTerm<TDeclaringType> dummyBnfTerm, Expression<Func<TDeclaringType, TMemberType>> exprForFieldOrPropertyAccess)
            where TBnfTermType : TMemberType
        {
            return MemberBoundToBnfTerm.Bind<TDeclaringType, TMemberType, TBnfTermType>(dummyBnfTerm, exprForFieldOrPropertyAccess, bnfTerm);
        }

        public static MemberBoundToBnfTerm BindMember(this BnfTerm bnfTerm, PropertyInfo propertyInfo)
        {
            return MemberBoundToBnfTerm.Bind(propertyInfo, bnfTerm);
        }

        public static MemberBoundToBnfTerm BindMember(this BnfTerm bnfTerm, FieldInfo fieldInfo)
        {
            return MemberBoundToBnfTerm.Bind(fieldInfo, bnfTerm);
        }

        #endregion

        #region Create/ConvertValue

        public static ValueForBnfTerm<TOut> CreateValue<TOut>(this BnfTerm bnfTerm, TOut value)
        {
            return ValueForBnfTerm.Create(bnfTerm, value);
        }

        public static ValueForBnfTerm<TOut> CreateValue<TOut>(this BnfTerm bnfTerm, AstValueCreator<TOut> astValueCreator)
        {
            return ValueForBnfTerm.Create(bnfTerm, astValueCreator);
        }

        public static ValueForBnfTerm<TOut> ConvertValue<TIn, TOut>(this IBnfTerm<TIn> bnfTerm, ValueConverter<TIn, TOut> valueConverter)
        {
            return ValueForBnfTerm.Convert(bnfTerm, valueConverter);
        }

        #endregion

        #region Create/ConvertValue

        public static ValueForBnfTerm<TOut> Cast<TIn, TOut>(this IBnfTerm<TIn> bnfTerm)
        {
            return ValueForBnfTerm.Cast<TIn, TOut>(bnfTerm);
        }

        public static ValueForBnfTerm<TOut> Cast<TOut>(this BnfTerm bnfTerm)
        {
            return ValueForBnfTerm.Cast<TOut>(bnfTerm);
        }

        public static ValueForBnfTerm<TOut> Cast<TIn, TOut>(this IBnfTerm<TIn> bnfTerm, IBnfTerm<TOut> dummyBnfTerm)
        {
            return ValueForBnfTerm.Cast<TIn, TOut>(bnfTerm);
        }

        #endregion

        #region Typesafe Q

        public static ValueForBnfTerm<T?> QVal<T>(this IBnfTerm<T> bnfTerm)
            where T : struct
        {
            return ValueForBnfTerm.ConvertValueOptVal(bnfTerm);
        }

        public static ValueForBnfTerm<T> QRef<T>(this IBnfTerm<T> bnfTerm)
            where T : class
        {
            return ValueForBnfTerm.ConvertValueOptRef(bnfTerm);
        }

        #endregion

        #region GetMember

        public static PropertyInfo GetProperty<T>(Expression<Func<T>> exprForPropertyAccess)
        {
            var memberExpression = exprForPropertyAccess.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new InvalidOperationException("Member in expression is not a property.");

            return propertyInfo;
        }

        public static FieldInfo GetField<T>(Expression<Func<T>> exprForFieldAccess)
        {
            var memberExpression = exprForFieldAccess.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var fieldInfo = memberExpression.Member as FieldInfo;
            if (fieldInfo == null)
                throw new InvalidOperationException("Member in expression is not a field.");

            return fieldInfo;
        }

        public static MemberInfo GetMember<T>(Expression<Func<T>> exprForMemberAccess)
        {
            var memberExpression = exprForMemberAccess.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var memberInfo = memberExpression.Member as MemberInfo;
            if (memberInfo == null)
                throw new InvalidOperationException("Member in expression is not a member.");

            return memberInfo;
        }

        public static MemberInfo GetMember<TDeclaringType, TMemberType>(Expression<Func<TDeclaringType, TMemberType>> exprForMemberAccess)
        {
            var memberExpression = exprForMemberAccess.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var memberInfo = memberExpression.Member as MemberInfo;
            if (memberInfo == null)
                throw new InvalidOperationException("Member in expression is not a member.");

            return memberInfo;
        }

        #endregion

        #region Value <-> AstNode conversion

        public static object ValueToAstNode<T>(T value, AstContext context, ParseTreeNode parseTreeNode)
        {
            return ((GrammarExtension)context.Language.Grammar).BrowsableAstNodes && !(value is IBrowsableAstNode)
                ? AstNodeWrapper.Create(value, context, parseTreeNode)
                : value;
        }

        public static T AstNodeToValue<T>(object astNode)
        {
            AstNodeWrapper<T> astNodeWrapper = astNode as AstNodeWrapper<T>;

            if (astNodeWrapper == null && astNode.GetType().IsGenericType && astNode.GetType().GetGenericTypeDefinition() == typeof(AstNodeWrapper<>))
                throw new ArgumentException(
                    string.Format("AstNodeWrapper with the wrong generic type argument: {0} was found, but {1} was expected",
                        astNode.GetType().GenericTypeArguments[0].FullName, typeof(T).FullName),
                    "astNode"
                    );

            return astNodeWrapper != null ? astNodeWrapper.Value : (T)astNode;
        }

        #endregion

        #region Misc

        public static void SetRule<T>(this INonTerminalWithSingleTypesafeRule<T> nonTerminal, IBnfTerm<T> bnfExpression)
        {
            nonTerminal.Rule = bnfExpression;
        }

        public static void SetRuleOr<T>(this INonTerminalWithMultipleTypesafeRule<T> nonTerminal, params IBnfTerm<T>[] bnfExpressions)
        {
            nonTerminal.RuleTL = bnfExpressions.Cast<BnfExpression>().Aggregate(
                (BnfExpression)bnfExpressions[0],
                (bnfExpressionProcessed, bnfExpressionToBeProcess) => bnfExpressionProcessed | bnfExpressionToBeProcess
                );
        }

        public static BnfExpression<T> ToType<T>(this BnfTerm bnfTerm)
        {
            return new BnfExpression<T>(bnfTerm);
        }

        public static BnfExpression<T> ToType<T>(this BnfTerm bnfTerm, IBnfTerm<T> dummyBnfTerm)
        {
            return ToType<T>(bnfTerm);
        }

        public static void ThrowGrammarError(GrammarErrorLevel grammarErrorLevel, string message, params object[] args)
        {
            if (args.Length > 0)
                message = string.Format(message, args);

            throw new GrammarErrorException(message, new GrammarError(grammarErrorLevel, null, message));
        }

        public static string TypeNameWithDeclaringTypes(Type type)
        {
            return type.IsNested
                ? string.Format("{0}_{1}", TypeNameWithDeclaringTypes(type.DeclaringType), type.Name.ToLower())
                : type.Name.ToLower();
        }

        internal static BnfExpression<T> Op_Plus<T>(BnfTerm bnfTerm1, BnfTerm bnfTerm2)
        {
            return new BnfExpression<T>(BnfTerm.Op_Plus(bnfTerm1, bnfTerm2));
        }

        internal static BnfExpression<T> Op_Pipe<T>(BnfTerm bnfTerm1, BnfTerm bnfTerm2)
        {
            return new BnfExpression<T>(BnfTerm.Op_Pipe(bnfTerm1, bnfTerm2));
        }

        #endregion
    }
}
