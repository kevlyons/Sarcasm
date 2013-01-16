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
    public partial class BnfiTermCollection : BnfiTermNonTerminal, IBnfiTerm, IUnparsable
    {
        #region Types

        protected enum ListKind { Star, Plus }

        #endregion

        #region State

        private ListKind? listKind = null;

        private readonly Type elementType;
        private readonly MethodInfo addMethod;

        private BnfTerm element;
        private BnfTerm delimiter;

        private readonly bool movable = true;

        public EmptyCollectionHandling EmptyCollectionHandling { get; set; }

        #endregion

        protected Type collectionType { get { return base.Type; } }

        [Obsolete("Use collectionDynamicType instead", error: true)]
        public new Type Type { get { return base.Type; } }

        private const BindingFlags bindingAttrInstanceAll = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        #region Construction

        public BnfiTermCollection(Type collectionType, string errorAlias = null)
            : this(collectionType, elementType: null, errorAlias: errorAlias, runtimeCheck: true)
        {
            this.movable = false;
        }

        public BnfiTermCollection(Type collectionType, Type elementType, string errorAlias = null)
            : this(collectionType, elementType, errorAlias, runtimeCheck: true)
        {
            this.movable = false;
        }

        protected BnfiTermCollection(Type collectionType, Type elementType, string errorAlias, bool runtimeCheck)
            : base(collectionType, errorAlias)
        {
            if (runtimeCheck && collectionType.GetConstructor(bindingAttrInstanceAll, System.Type.DefaultBinder, types: System.Type.EmptyTypes, modifiers: null) == null)
                throw new ArgumentException("Collection type has no default constructor (neither public nor nonpublic)", "type");

            if (runtimeCheck && elementType == null && collectionType.IsGenericType)
            {   // we try to guess the elementType
                Type iCollectionGenericType = collectionType.GetInterfaces().FirstOrDefault(interfaceType => interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>));
                if (iCollectionGenericType != null)
                    elementType = iCollectionGenericType.GenericTypeArguments[0];
            }
            this.elementType = elementType ?? typeof(object);

            if (runtimeCheck)
            {
                addMethod = collectionType.GetMethod("Add", bindingAttrInstanceAll, System.Type.DefaultBinder, new[] { elementType }, modifiers: null);
                if (addMethod == null)
                    throw new ArgumentException("Collection type has proper 'Add' method (neither public nor nonpublic)", "collectionType");
            }

            SetNodeCreator();

            this.EmptyCollectionHandling = Sarcasm.Ast.Grammar.CurrentGrammar.EmptyCollectionHandling;
        }

        protected virtual void SetNodeCreator()
        {
            /*
             * NOTE: We are dealing here with totally typeless collection and using reflection anyway, so we are not forcing the created object to be
             * an IList, ICollection, etc., so we are just working here with an object type and require that the collection has an 'Add' method during runtime.
             * */
            SetNodeCreator<object, object>(
                () => Activator.CreateInstance(collectionType, nonPublic: true),
                (collection, element) => addMethod.Invoke(obj: collection, parameters: new[] { element })
                );
        }

        protected void SetNodeCreator<TCollectionStaticType, TElementStaticType>(Func<TCollectionStaticType> createCollection, Action<TCollectionStaticType, TElementStaticType> addElementToCollection)
        {
            this.AstConfig.NodeCreator = (context, parseTreeNode) =>
            {
                Lazy<TCollectionStaticType> collection = new Lazy<TCollectionStaticType>(() => createCollection());

                bool collectionHasElements = false;
                foreach (var element in GetFlattenedElements<TElementStaticType>(parseTreeNode, context))
                {
                    collectionHasElements = true;
                    addElementToCollection(collection.Value, element);
                }

                TCollectionStaticType astValue = !collectionHasElements && this.EmptyCollectionHandling == EmptyCollectionHandling.ReturnNull
                    ? default(TCollectionStaticType)
                    : collection.Value;

                parseTreeNode.AstNode = GrammarHelper.ValueToAstNode(astValue, context, parseTreeNode);
            };
        }

        protected IEnumerable<TElementStaticType> GetFlattenedElements<TElementStaticType>(ParseTreeNode parseTreeNode, AstContext context)
        {
            foreach (var parseTreeChild in parseTreeNode.ChildNodes)
            {
                /*
                 * The type of childValue is 'object' because childValue can be other than an element, which causes error,
                 * but we want to give a proper error message (see below) instead of throwing a simple cast exception
                 * */
                object childValue = GrammarHelper.AstNodeToValue(parseTreeChild.AstNode);

                if (elementType.IsInstanceOfType(childValue))
                {
                    yield return (TElementStaticType)childValue;
                }
                else if (parseTreeChild.Term.Flags.IsSet(TermFlags.IsList))
                {
                    foreach (var descendantElement in GetFlattenedElements<TElementStaticType>(parseTreeChild, context))
                        yield return descendantElement;
                }
                else if (parseTreeChild.Term.Flags.IsSet(TermFlags.NoAstNode))
                {
                    // simply omit children with no ast (they are probably delimiters)
                }
                else
                {
                    GrammarHelper.GrammarError(
                        context,
                        parseTreeChild.Span.Location,
                        ErrorLevel.Error,
                        "Term '{0}' should be type of '{1}' but found '{2}' instead", parseTreeChild.Term, elementType.FullName, childValue != null ? childValue.GetType().FullName : "<<NULL>>");
                }
            }
        }

        #endregion

        #region StarList and PlusList

        #region Typesafe (TCollectionType, TElementType)

        public static BnfiTermCollection<TCollectionType, TElementType> StarList<TCollectionType, TElementType>(IBnfiTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, TElementType>();
            MakeStarRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<TElementType>, TElementType> StarList<TElementType>(IBnfiTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
        {
            return StarList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        public static BnfiTermCollection<TCollectionType, TElementType> PlusList<TCollectionType, TElementType>(IBnfiTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, TElementType>();
            MakePlusRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<TElementType>, TElementType> PlusList<TElementType>(IBnfiTerm<TElementType> bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        #endregion

        #region Typeless converted to typesafe (TCollectionType, TElementType)

        public static BnfiTermCollection<TCollectionType, TElementType> StarList<TCollectionType, TElementType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, TElementType>();
            MakeStarRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<TElementType>, TElementType> StarList<TElementType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return StarList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        public static BnfiTermCollection<TCollectionType, TElementType> PlusList<TCollectionType, TElementType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<TElementType>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, TElementType>();
            MakePlusRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<TElementType>, TElementType> PlusList<TElementType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusList<List<TElementType>, TElementType>(bnfTermElement, delimiter);
        }

        #endregion

        #region Typeless converted to semi-typesafe (TCollectionType, object)

        public static BnfiTermCollection<TCollectionType, object> StarListST<TCollectionType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<object>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, object>();
            MakeStarRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<object>, object> StarListST(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return StarListST<List<object>>(bnfTermElement, delimiter);
        }

        public static BnfiTermCollection<TCollectionType, object> PlusListST<TCollectionType>(BnfTerm bnfTermElement, BnfTerm delimiter = null)
            where TCollectionType : ICollection<object>, new()
        {
            var bnfiTermCollection = new BnfiTermCollection<TCollectionType, object>();
            MakePlusRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection<List<object>, object> PlusListST(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            return PlusListST<List<object>>(bnfTermElement, delimiter);
        }

        #endregion

        #region Typeless

        public static BnfiTermCollection StarListTL(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            var bnfiTermCollection = new BnfiTermCollection(typeof(ICollection<object>));
            MakeStarRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        public static BnfiTermCollection PlusListTL(BnfTerm bnfTermElement, BnfTerm delimiter = null)
        {
            var bnfiTermCollection = new BnfiTermCollection(typeof(ICollection<object>));
            MakePlusRule(bnfiTermCollection, delimiter, bnfTermElement);
            return bnfiTermCollection;
        }

        #endregion

        #endregion

        #region MakePlusRule and MakeStarRule

        internal static BnfiTermCollection MakePlusRule(BnfiTermCollection bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
        {
            return (BnfiTermCollection)_MakePlusRule(bnfiTermCollection, delimiter, element);
        }

        internal static BnfiTermCollection MakeStarRule(BnfiTermCollection bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
        {
            return (BnfiTermCollection)_MakeStarRule(bnfiTermCollection, delimiter, element);
        }

        internal static BnfiTermCollection<TCollectionType> MakePlusRule<TCollectionType>(BnfiTermCollection<TCollectionType, object> bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
            where TCollectionType : ICollection<object>, new()
        {
            return (BnfiTermCollection<TCollectionType>)_MakePlusRule(bnfiTermCollection, delimiter, element);
        }

        internal static BnfiTermCollection<TCollectionType> MakeStarRule<TCollectionType>(BnfiTermCollection<TCollectionType, object> bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
            where TCollectionType : ICollection<object>, new()
        {
            return (BnfiTermCollection<TCollectionType>)_MakeStarRule(bnfiTermCollection, delimiter, element);
        }

        internal static BnfiTermCollection<TCollectionType> MakePlusRule<TCollectionType, TElementType>(BnfiTermCollection<TCollectionType, TElementType> bnfiTermCollection, BnfTerm delimiter, IBnfiTerm<TElementType> element)
            where TCollectionType : ICollection<TElementType>, new()
        {
            return (BnfiTermCollection<TCollectionType>)_MakePlusRule(bnfiTermCollection, delimiter, element.AsBnfTerm());
        }

        internal static BnfiTermCollection<TCollectionType> MakeStarRule<TCollectionType, TElementType>(BnfiTermCollection<TCollectionType, TElementType> bnfiTermCollection, BnfTerm delimiter, IBnfiTerm<TElementType> element)
            where TCollectionType : ICollection<TElementType>, new()
        {
            return (BnfiTermCollection<TCollectionType>)_MakeStarRule(bnfiTermCollection, delimiter, element.AsBnfTerm());
        }

        protected static BnfiTermCollection _MakePlusRule(BnfiTermCollection bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
        {
            bnfiTermCollection.SetState(ListKind.Plus, element, delimiter);
            Irony.Parsing.Grammar.CurrentGrammar.MakePlusRule(bnfiTermCollection, delimiter, element);
            return bnfiTermCollection;
        }

        protected static BnfiTermCollection _MakeStarRule(BnfiTermCollection bnfiTermCollection, BnfTerm delimiter, BnfTerm element)
        {
            bnfiTermCollection.SetState(ListKind.Star, element, delimiter);
            Irony.Parsing.Grammar.CurrentGrammar.MakeStarRule(bnfiTermCollection, delimiter, element);
            return bnfiTermCollection;
        }

        protected void SetState(ListKind listKind, BnfTerm element, BnfTerm delimiter)
        {
            this.listKind = listKind;
            this.element = element;
            this.delimiter = delimiter;
            this.Name = GetName();
        }

        protected void ClearState()
        {
            this.listKind = null;
            this.element = null;
            this.delimiter = null;
            this.Name = "<<null list>>";
        }

        protected void MoveTo(BnfiTermCollection target)
        {
            if (!this.movable)
                GrammarHelper.ThrowGrammarErrorException(GrammarErrorLevel.Error, "This collection should not be a right-value: {0}", this.Name);

            if (!this.listKind.HasValue)
                GrammarHelper.ThrowGrammarErrorException(GrammarErrorLevel.Error, "Right-value collection has not been initialized: {0}", this.Name);

            // note: target.RuleRaw is set and target.SetState is called by _MakePlusRule/_MakeStarRule
            if (this.listKind == ListKind.Plus)
                _MakePlusRule(target, this.delimiter, this.element);
            else if (this.listKind == ListKind.Star)
                _MakeStarRule(target, this.delimiter, this.element);
            else
                throw new InvalidOperationException(string.Format("Unknown listKind: {0}", this.listKind));

            this.RuleRaw = null;
            this.ClearState();
        }

        protected string GetName()
        {
            return string.Format("{0}List<{1}>", listKind, element.Name);
        }

        #endregion

        public BnfExpression RuleRaw { get { return base.Rule; } set { base.Rule = value; } }

        public new BnfiTermCollection Rule
        {
            set
            {
                value.MoveTo(this);
            }
        }

        BnfTerm IBnfiTerm.AsBnfTerm()
        {
            return this;
        }

        public BnfiTermCollection ReturnNullInsteadOfEmptyCollection()
        {
            this.EmptyCollectionHandling = EmptyCollectionHandling.ReturnNull;
            return this;
        }

        public BnfiTermCollection ReturnEmptyCollectionInsteadOfNull()
        {
            this.EmptyCollectionHandling = EmptyCollectionHandling.ReturnEmpty;
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
            System.Collections.IEnumerable collection = (System.Collections.IEnumerable)obj;

            if (collection == null && this.EmptyCollectionHandling == EmptyCollectionHandling.ReturnNull)
                yield break;    // this null value should be handled as an empty collection

            bool firstElement = true;
            foreach (object element in collection)
            {
                if (!firstElement && this.delimiter != null)
                    yield return new Value(this.delimiter, null);

                yield return new Value(this.element, element);

                firstElement = false;
            }
        }

        int? IUnparsable.GetChildBnfTermListPriority(IUnparser unparser, object obj, IEnumerable<Value> childValues)
        {
            System.Collections.IEnumerable collection = (System.Collections.IEnumerable)obj;

            if (collection != null || this.EmptyCollectionHandling == EmptyCollectionHandling.ReturnNull)
                return 1;
            else
                return null;
        }

        #endregion
    }

    public abstract partial class BnfiTermCollection<TCollectionType> : BnfiTermCollection, IBnfiTerm<TCollectionType>
    {
        protected BnfiTermCollection(Type elementType, string errorAlias = null)
            : base(typeof(TCollectionType), elementType, errorAlias: errorAlias, runtimeCheck: false)
        {
        }
    }

    public partial class BnfiTermCollection<TCollectionType, TElementType> : BnfiTermCollection<TCollectionType>
        where TCollectionType : ICollection<TElementType>, new()
    {
        public BnfiTermCollection(string errorAlias = null)
            : base(typeof(TElementType), errorAlias)
        {
        }

        protected override void SetNodeCreator()
        {
            SetNodeCreator<TCollectionType, TElementType>(
                () => new TCollectionType(),
                (collection, element) => collection.Add(element)
                );
        }

        [Obsolete(typelessQErrorMessage, error: true)]
        public new BnfExpression Q()
        {
            return base.Q();
        }

        public BnfiTermCollection RuleTypeless { set { base.Rule = value; } }

        public new BnfiTermCollection<TCollectionType> Rule { set { base.Rule = value; } }

        public new BnfiTermCollection<TCollectionType, TElementType> ReturnNullInsteadOfEmptyCollection()
        {
            this.EmptyCollectionHandling = EmptyCollectionHandling.ReturnNull;
            return this;
        }

        public new BnfiTermCollection<TCollectionType, TElementType> ReturnEmptyCollectionInsteadOfNull()
        {
            this.EmptyCollectionHandling = EmptyCollectionHandling.ReturnEmpty;
            return this;
        }
    }
}