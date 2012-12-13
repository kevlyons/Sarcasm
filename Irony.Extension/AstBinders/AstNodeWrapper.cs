﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony;
using Irony.Ast;
using Irony.Parsing;

namespace Irony.Extension.AstBinders
{
    public static class AstNodeWrapper
    {
        public static object Create<T>(T value, AstContext context, ParseTreeNode parseTreeNode)
        {
            return new AstNodeWrapper<T>(value, context, parseTreeNode);
        }
    }

    public class AstNodeWrapper<T> : IBrowsableAstNode
    {
        public T Value { get; private set; }

        private readonly AstContext context;
        private readonly ParseTreeNode parseTreeNode;

        internal AstNodeWrapper(T value, AstContext context, ParseTreeNode parseTreeNode)
        {
            this.Value = value;
            this.context = context;
            this.parseTreeNode = parseTreeNode;
        }

        public static implicit operator T(AstNodeWrapper<T> astNode)
        {
            return astNode.Value;
        }

        System.Collections.IEnumerable IBrowsableAstNode.GetChildNodes()
        {
            return parseTreeNode.ChildNodes.Select(parseTreeChild => parseTreeChild.AstNode);
        }

        int IBrowsableAstNode.Position
        {
            get { return parseTreeNode.Span.Location.Position; }
        }
    }
}