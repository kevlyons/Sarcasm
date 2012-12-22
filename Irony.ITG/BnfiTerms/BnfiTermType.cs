﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Diagnostics;
using System.IO;

using Irony;
using Irony.Ast;
using Irony.Parsing;

namespace Irony.ITG
{
    public partial class BnfiTermType : BnfiTermNonTerminal, IBnfiTerm
    {
        public BnfiTermType(Type type, string errorAlias = null)
            : base(type, errorAlias)
        {
            if (type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, binder: null, types: Type.EmptyTypes, modifiers: null) == null)
                throw new ArgumentException("Type has no default constructor (neither public nor nonpublic)", "type");
        }

        public new BnfiExpressionType Rule
        {
            get { return (BnfiExpressionType)base.Rule; }
            set
            {
                AstConfig.NodeCreator = (context, parseTreeNode) =>
                {
                    object objValue = Activator.CreateInstance(type, nonPublic: true);

                    //foreach (var parseTreeChild in parseTreeNode.ChildNodes.Where(childNode => childNode.Tag is BnfiTermType))
                    //{
                    //    BnfiTermType sourceBnfiTermType = (BnfiTermType)parseTreeChild.Tag;
                    //    object sourceObjValue = GrammarHelper.AstNodeToValue(parseTreeChild.AstNode);


                    //}

                    foreach (var parseTreeChild in parseTreeNode.ChildNodes.Where(childNode => childNode.Tag is BnfiTermMember))
                    {
                        MemberInfo memberInfo = ((BnfiTermMember)parseTreeChild.Tag).MemberInfo;
                        object memberValue = GrammarHelper.AstNodeToValue(parseTreeChild.AstNode);

                        if (memberInfo is PropertyInfo)
                            ((PropertyInfo)memberInfo).SetValue(objValue, memberValue);
                        else if (memberInfo is FieldInfo)
                            ((FieldInfo)memberInfo).SetValue(objValue, memberValue);
                        else
                            throw new ApplicationException("Object with wrong type in memberinfo: " + memberInfo.Name);
                    }

                    parseTreeNode.AstNode = GrammarHelper.ValueToAstNode(objValue, context, parseTreeNode);
                };

                foreach (var bnfTermList in ((BnfExpression)value).Data)
                {
                    foreach (var bnfTerm in bnfTermList)
                    {
                        if (bnfTerm is BnfiTermMember || bnfTerm is BnfiTermType)
                            ((NonTerminal)bnfTerm).Reduced += nonTerminal_Reduced;
                    }
                }

                base.Rule = value;
            }
        }

        public BnfExpression RuleTL { get { return base.Rule; } set { base.Rule = value; } }

        void nonTerminal_Reduced(object sender, ReducedEventArgs e)
        {
            e.ResultNode.Tag = sender;
        }

        public BnfTerm AsBnfTerm()
        {
            return this;
        }
    }

    public partial class BnfiTermType<TType> : BnfiTermType, IBnfiTerm<TType>, INonTerminalWithMultipleTypesafeRule<TType>
        where TType : new()
    {
        public static TType __ { get; private set; }

        static BnfiTermType()
        {
            __ = new TType();
        }

        public TType _ { get { return BnfiTermType<TType>.__; } }

        public BnfiTermType(string errorAlias = null)
            : base(typeof(TType), errorAlias)
        {
        }

        [Obsolete(typelessQErrorMessage, error: true)]
        public new BnfExpression Q()
        {
            return base.Q();
        }

        public new BnfiExpressionType<TType> Rule { set { base.Rule = value; } }

    }
}
