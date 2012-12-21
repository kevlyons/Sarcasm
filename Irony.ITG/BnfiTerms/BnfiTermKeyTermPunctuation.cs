﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Ast;
using Irony.Parsing;

namespace Irony.ITG
{
    public partial class BnfiTermKeyTermPunctuation : KeyTerm, IBnfiTerm
    {
        public BnfiTermKeyTermPunctuation(string text)
            : base(text, text)
        {
            this.SetFlag(TermFlags.IsPunctuation | TermFlags.NoAstNode);
        }

        public BnfTerm AsBnfTerm()
        {
            return this;
        }
    }
}