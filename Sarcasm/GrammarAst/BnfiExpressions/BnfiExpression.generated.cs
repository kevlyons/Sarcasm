﻿ 
 
 
 
 
// GENERATED FILE

using System;
using System.Collections.Generic;
using System.Linq;

using Irony;
using Irony.Ast;
using Irony.Parsing;

namespace Sarcasm.GrammarAst
{
	#region BnfiExpressionTerminals definition and operators

	#region BnfiExpressionTerminals definition

	public partial class BnfiExpressionTerminals : BnfiExpression, IBnfiExpressionTL
	{
		#region Construction

		public BnfiExpressionTerminals()
		{
		}

		public BnfiExpressionTerminals(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionTerminals(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionTerminals(BnfExpression bnfExpression)
		{
			return new BnfiExpressionTerminals(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermNoAst, BnfiTermKeyTerm]: implicit conversions from BnfiExpressionTerminals

	public partial class BnfiTermNoAst
	{
		public static implicit operator BnfiExpressionTerminals(BnfiTermNoAst term)
		{
			return new BnfiExpressionTerminals((BnfTerm)term);
		}
	}

	public partial class BnfiTermKeyTerm
	{
		public static implicit operator BnfiExpressionTerminals(BnfiTermKeyTerm term)
		{
			return new BnfiExpressionTerminals((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiExpressionTerminals: implicit conversions from [Terminal, GrammarHint]

	public partial class BnfiExpressionTerminals
	{
		public static implicit operator BnfiExpressionTerminals(Terminal term)
		{
			return new BnfiExpressionTerminals((BnfTerm)term);
		}
		public static implicit operator BnfiExpressionTerminals(GrammarHint term)
		{
			return new BnfiExpressionTerminals((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermNoAst '+' operators for BnfExpression

	public partial class BnfiTermNoAst
	{
        public static BnfiExpressionTerminals operator +(BnfiTermNoAst term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermNoAst term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermKeyTerm term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermNoAst term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(Terminal term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermNoAst term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(GrammarHint term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermKeyTerm '+' operators for BnfExpression

	public partial class BnfiTermKeyTerm
	{
        public static BnfiExpressionTerminals operator +(BnfiTermKeyTerm term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermKeyTerm term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(Terminal term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermKeyTerm term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(GrammarHint term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermNoAst '|' operators for BnfExpression

	public partial class BnfiTermNoAst
	{
        public static BnfiExpressionTerminals operator |(BnfiTermNoAst term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(Terminal term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermNoAst term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(GrammarHint term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermNoAst term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermNoAst term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermKeyTerm term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermNoAst term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiExpressionTerminals term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermKeyTerm '|' operators for BnfExpression

	public partial class BnfiTermKeyTerm
	{
        public static BnfiExpressionTerminals operator |(BnfiTermKeyTerm term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(Terminal term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermKeyTerm term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(GrammarHint term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermKeyTerm term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiTermKeyTerm term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiExpressionTerminals term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionTerminals '+' operators for BnfExpression

	public partial class BnfiExpressionTerminals
	{
        public static BnfiExpressionTerminals operator +(BnfiExpressionTerminals term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermNoAst term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiExpressionTerminals term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiTermKeyTerm term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiExpressionTerminals term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(Terminal term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiExpressionTerminals term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(GrammarHint term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator +(BnfiExpressionTerminals term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionTerminals '|' operators for BnfExpression

	public partial class BnfiExpressionTerminals
	{
        public static BnfiExpressionTerminals operator |(BnfiExpressionTerminals term1, Terminal term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(Terminal term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiExpressionTerminals term1, GrammarHint term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(GrammarHint term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionTerminals operator |(BnfiExpressionTerminals term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionTerminals)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionKeyTermPunctuations definition and operators

	#region BnfiExpressionKeyTermPunctuations definition

	public partial class BnfiExpressionKeyTermPunctuations : BnfiExpression, IBnfiExpressionTL
	{
		#region Construction

		public BnfiExpressionKeyTermPunctuations()
		{
		}

		public BnfiExpressionKeyTermPunctuations(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionKeyTermPunctuations(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionKeyTermPunctuations(BnfExpression bnfExpression)
		{
			return new BnfiExpressionKeyTermPunctuations(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermKeyTermPunctuation]: implicit conversions from BnfiExpressionKeyTermPunctuations

	public partial class BnfiTermKeyTermPunctuation
	{
		public static implicit operator BnfiExpressionKeyTermPunctuations(BnfiTermKeyTermPunctuation term)
		{
			return new BnfiExpressionKeyTermPunctuations((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiExpressionKeyTermPunctuations: implicit conversions from [GrammarHint]

	public partial class BnfiExpressionKeyTermPunctuations
	{
		public static implicit operator BnfiExpressionKeyTermPunctuations(GrammarHint term)
		{
			return new BnfiExpressionKeyTermPunctuations((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermKeyTermPunctuation '+' operators for BnfExpression

	public partial class BnfiTermKeyTermPunctuation
	{
        public static BnfiExpressionKeyTermPunctuations operator +(BnfiTermKeyTermPunctuation term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(BnfiTermKeyTermPunctuation term1, GrammarHint term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(GrammarHint term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermKeyTermPunctuation '|' operators for BnfExpression

	public partial class BnfiTermKeyTermPunctuation
	{
        public static BnfiExpressionKeyTermPunctuations operator |(BnfiTermKeyTermPunctuation term1, GrammarHint term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(GrammarHint term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(BnfiTermKeyTermPunctuation term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(BnfiTermKeyTermPunctuation term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(BnfiExpressionKeyTermPunctuations term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionKeyTermPunctuations '+' operators for BnfExpression

	public partial class BnfiExpressionKeyTermPunctuations
	{
        public static BnfiExpressionKeyTermPunctuations operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(BnfiTermKeyTermPunctuation term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(BnfiExpressionKeyTermPunctuations term1, GrammarHint term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(GrammarHint term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator +(BnfiExpressionKeyTermPunctuations term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionKeyTermPunctuations '|' operators for BnfExpression

	public partial class BnfiExpressionKeyTermPunctuations
	{
        public static BnfiExpressionKeyTermPunctuations operator |(BnfiExpressionKeyTermPunctuations term1, GrammarHint term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(GrammarHint term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionKeyTermPunctuations operator |(BnfiExpressionKeyTermPunctuations term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionKeyTermPunctuations)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionRecordTL definition and operators

	#region BnfiExpressionRecordTL definition

	public partial class BnfiExpressionRecordTL : BnfiExpression, IBnfiExpressionTL
	{
		#region Construction

		public BnfiExpressionRecordTL()
		{
		}

		public BnfiExpressionRecordTL(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionRecordTL(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionRecordTL(BnfExpression bnfExpression)
		{
			return new BnfiExpressionRecordTL(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [Member, BnfiTermCopyTL]: implicit conversions from BnfiExpressionRecordTL

	public partial class Member
	{
		public static implicit operator BnfiExpressionRecordTL(Member term)
		{
			return new BnfiExpressionRecordTL((BnfTerm)term);
		}
	}

	public partial class BnfiTermCopyTL
	{
		public static implicit operator BnfiExpressionRecordTL(BnfiTermCopyTL term)
		{
			return new BnfiExpressionRecordTL((BnfTerm)term);
		}
	}

	#endregion

	#region Member '+' operators for BnfExpression

	public partial class Member
	{
        public static BnfiExpressionRecordTL operator +(Member term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermNoAst term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermKeyTerm term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, Terminal term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Terminal term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, GrammarHint term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(GrammarHint term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionTerminals term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCopyTL '+' operators for BnfExpression

	public partial class BnfiTermCopyTL
	{
        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermNoAst term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermKeyTerm term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, Terminal term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Terminal term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, GrammarHint term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(GrammarHint term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionTerminals term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region Member '|' operators for BnfExpression

	public partial class Member
	{
        public static BnfiExpressionRecordTL operator |(Member term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(Member term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(BnfiTermCopyTL term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(Member term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(BnfiExpressionRecordTL term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCopyTL '|' operators for BnfExpression

	public partial class BnfiTermCopyTL
	{
        public static BnfiExpressionRecordTL operator |(BnfiTermCopyTL term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(BnfiTermCopyTL term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator |(BnfiExpressionRecordTL term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionRecordTL '+' operators for BnfExpression

	public partial class BnfiExpressionRecordTL
	{
        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermNoAst term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermKeyTerm term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, Terminal term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Terminal term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, GrammarHint term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(GrammarHint term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionTerminals term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, Member term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(Member term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, BnfiTermCopyTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiTermCopyTL term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecordTL operator +(BnfiExpressionRecordTL term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionRecordTL '|' operators for BnfExpression

	public partial class BnfiExpressionRecordTL
	{
        public static BnfiExpressionRecordTL operator |(BnfiExpressionRecordTL term1, BnfiExpressionRecordTL term2)
        {
            return (BnfiExpressionRecordTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionRecord<T> definition and operators

	#region BnfiExpressionRecord<T> definition

	public partial class BnfiExpressionRecord<T> : BnfiExpression, IBnfiExpression<T>, IBnfiTermPlusAbleForType<T>
	{
		#region Construction

		public BnfiExpressionRecord()
		{
		}

		public BnfiExpressionRecord(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionRecord(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionRecord<T>(BnfExpression bnfExpression)
		{
			return new BnfiExpressionRecord<T>(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [Member<TDeclaringType>, BnfiTermCopy<T>]: implicit conversions from BnfiExpressionRecord<T>

	public partial class Member<TDeclaringType>
	{
		public static implicit operator BnfiExpressionRecord<TDeclaringType>(Member<TDeclaringType> term)
		{
			return new BnfiExpressionRecord<TDeclaringType>((BnfTerm)term);
		}
	}

	public partial class BnfiTermCopy<T>
	{
		public static implicit operator BnfiExpressionRecord<T>(BnfiTermCopy<T> term)
		{
			return new BnfiExpressionRecord<T>((BnfTerm)term);
		}
	}

	#endregion

	#region Member<TDeclaringType> '+' operators for BnfExpression

	public partial class Member<TDeclaringType>
	{
        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(BnfiTermNoAst term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(BnfiTermKeyTerm term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, Terminal term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Terminal term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, GrammarHint term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(GrammarHint term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(BnfiExpressionTerminals term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(Member<TDeclaringType> term1, BnfiTermCopy<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator +(BnfiTermCopy<TDeclaringType> term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCopy<T> '+' operators for BnfExpression

	public partial class BnfiTermCopy<T>
	{
        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermNoAst term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermKeyTerm term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, Terminal term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(Terminal term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(GrammarHint term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionTerminals term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region Member<TDeclaringType> '|' operators for BnfExpression

	public partial class Member<TDeclaringType>
	{
        public static BnfiExpressionRecord<TDeclaringType> operator |(Member<TDeclaringType> term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator |(Member<TDeclaringType> term1, BnfiTermCopy<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator |(BnfiTermCopy<TDeclaringType> term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator |(Member<TDeclaringType> term1, BnfiExpressionRecord<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<TDeclaringType> operator |(BnfiExpressionRecord<TDeclaringType> term1, Member<TDeclaringType> term2)
        {
            return (BnfiExpressionRecord<TDeclaringType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCopy<T> '|' operators for BnfExpression

	public partial class BnfiTermCopy<T>
	{
        public static BnfiExpressionRecord<T> operator |(BnfiTermCopy<T> term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator |(BnfiTermCopy<T> term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator |(BnfiExpressionRecord<T> term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionRecord<T> '+' operators for BnfExpression

	public partial class BnfiExpressionRecord<T>
	{
        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermNoAst term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermKeyTerm term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, Terminal term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(Terminal term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(GrammarHint term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionTerminals term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, Member<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(Member<T> term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, BnfiTermCopy<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiTermCopy<T> term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionRecord<T> operator +(BnfiExpressionRecord<T> term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionRecord<T> '|' operators for BnfExpression

	public partial class BnfiExpressionRecord<T>
	{
        public static BnfiExpressionRecord<T> operator |(BnfiExpressionRecord<T> term1, BnfiExpressionRecord<T> term2)
        {
            return (BnfiExpressionRecord<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionConversionTL definition and operators

	#region BnfiExpressionConversionTL definition

	public partial class BnfiExpressionConversionTL : BnfiExpression, IBnfiExpressionTL
	{
		#region Construction

		public BnfiExpressionConversionTL()
		{
		}

		public BnfiExpressionConversionTL(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionConversionTL(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionConversionTL(BnfExpression bnfExpression)
		{
			return new BnfiExpressionConversionTL(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermConversion]: implicit conversions from BnfiExpressionConversionTL

	public partial class BnfiTermConversion
	{
		public static implicit operator BnfiExpressionConversionTL(BnfiTermConversion term)
		{
			return new BnfiExpressionConversionTL((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermConversion '+' operators for BnfExpression

	public partial class BnfiTermConversion
	{
        public static BnfiExpressionConversionTL operator +(BnfiTermConversion term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermNoAst term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermConversion term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermKeyTerm term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermConversion term1, Terminal term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(Terminal term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermConversion term1, GrammarHint term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(GrammarHint term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermConversion term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionTerminals term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermConversion '|' operators for BnfExpression

	public partial class BnfiTermConversion
	{
        public static BnfiExpressionConversionTL operator |(BnfiTermConversion term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator |(BnfiTermConversion term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator |(BnfiExpressionConversionTL term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionConversionTL '+' operators for BnfExpression

	public partial class BnfiExpressionConversionTL
	{
        public static BnfiExpressionConversionTL operator +(BnfiExpressionConversionTL term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermNoAst term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionConversionTL term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiTermKeyTerm term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionConversionTL term1, Terminal term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(Terminal term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionConversionTL term1, GrammarHint term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(GrammarHint term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionConversionTL term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversionTL operator +(BnfiExpressionTerminals term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionConversionTL '|' operators for BnfExpression

	public partial class BnfiExpressionConversionTL
	{
        public static BnfiExpressionConversionTL operator |(BnfiExpressionConversionTL term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionConversionTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionConversion<T> definition and operators

	#region BnfiExpressionConversion<T> definition

	public partial class BnfiExpressionConversion<T> : BnfiExpression, IBnfiExpression<T>, IBnfiTermOrAbleForChoice<T>
	{
		#region Construction

		public BnfiExpressionConversion()
		{
		}

		public BnfiExpressionConversion(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionConversion(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionConversion<T>(BnfExpression bnfExpression)
		{
			return new BnfiExpressionConversion<T>(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermConversion<T>]: implicit conversions from BnfiExpressionConversion<T>

	public partial class BnfiTermConversion<T>
	{
		public static implicit operator BnfiExpressionConversion<T>(BnfiTermConversion<T> term)
		{
			return new BnfiExpressionConversion<T>((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermConversion<T> '+' operators for BnfExpression

	public partial class BnfiTermConversion<T>
	{
        public static BnfiExpressionConversion<T> operator +(BnfiTermConversion<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermNoAst term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermConversion<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermKeyTerm term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermConversion<T> term1, Terminal term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(Terminal term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermConversion<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(GrammarHint term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermConversion<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionTerminals term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermConversion<T> '|' operators for BnfExpression

	public partial class BnfiTermConversion<T>
	{
        public static BnfiExpressionConversion<T> operator |(BnfiTermConversion<T> term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator |(BnfiTermConversion<T> term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator |(BnfiExpressionConversion<T> term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionConversion<T> '+' operators for BnfExpression

	public partial class BnfiExpressionConversion<T>
	{
        public static BnfiExpressionConversion<T> operator +(BnfiExpressionConversion<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermNoAst term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionConversion<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiTermKeyTerm term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionConversion<T> term1, Terminal term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(Terminal term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionConversion<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(GrammarHint term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionConversion<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionConversion<T> operator +(BnfiExpressionTerminals term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionConversion<T> '|' operators for BnfExpression

	public partial class BnfiExpressionConversion<T>
	{
        public static BnfiExpressionConversion<T> operator |(BnfiExpressionConversion<T> term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionConversion<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionChoiceTL definition and operators

	#region BnfiExpressionChoiceTL definition

	public partial class BnfiExpressionChoiceTL : BnfiExpression, IBnfiExpressionTL
	{
		#region Construction

		public BnfiExpressionChoiceTL()
		{
		}

		public BnfiExpressionChoiceTL(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionChoiceTL(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionChoiceTL(BnfExpression bnfExpression)
		{
			return new BnfiExpressionChoiceTL(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermRecord, BnfiTermChoice, BnfiTermCollectionTL]: implicit conversions from BnfiExpressionChoiceTL

	public partial class BnfiTermRecord
	{
		public static implicit operator BnfiExpressionChoiceTL(BnfiTermRecord term)
		{
			return new BnfiExpressionChoiceTL((BnfTerm)term);
		}
	}

	public partial class BnfiTermChoice
	{
		public static implicit operator BnfiExpressionChoiceTL(BnfiTermChoice term)
		{
			return new BnfiExpressionChoiceTL((BnfTerm)term);
		}
	}

	public partial class BnfiTermCollectionTL
	{
		public static implicit operator BnfiExpressionChoiceTL(BnfiTermCollectionTL term)
		{
			return new BnfiExpressionChoiceTL((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiExpressionChoiceTL: implicit conversions from [BnfiTermConversion, BnfiExpressionConversionTL]

	public partial class BnfiExpressionChoiceTL
	{
		public static implicit operator BnfiExpressionChoiceTL(BnfiTermConversion term)
		{
			return new BnfiExpressionChoiceTL((BnfTerm)term);
		}
		public static implicit operator BnfiExpressionChoiceTL(BnfiExpressionConversionTL term)
		{
			return new BnfiExpressionChoiceTL((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermRecord '+' operators for BnfExpression

	public partial class BnfiTermRecord
	{
        public static BnfiExpressionChoiceTL operator +(BnfiTermRecord term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermKeyTermPunctuation term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermRecord term1, GrammarHint term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(GrammarHint term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermRecord term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermChoice '+' operators for BnfExpression

	public partial class BnfiTermChoice
	{
        public static BnfiExpressionChoiceTL operator +(BnfiTermChoice term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermKeyTermPunctuation term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermChoice term1, GrammarHint term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(GrammarHint term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermChoice term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCollectionTL '+' operators for BnfExpression

	public partial class BnfiTermCollectionTL
	{
        public static BnfiExpressionChoiceTL operator +(BnfiTermCollectionTL term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermKeyTermPunctuation term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermCollectionTL term1, GrammarHint term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(GrammarHint term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermCollectionTL term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermRecord '|' operators for BnfExpression

	public partial class BnfiTermRecord
	{
        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermConversion term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionConversionTL term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermRecord term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiTermRecord term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermChoice '|' operators for BnfExpression

	public partial class BnfiTermChoice
	{
        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermConversion term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionConversionTL term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermChoice term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiTermChoice term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCollectionTL '|' operators for BnfExpression

	public partial class BnfiTermCollectionTL
	{
        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermConversion term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionConversionTL term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermCollectionTL term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiTermCollectionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionChoiceTL '+' operators for BnfExpression

	public partial class BnfiExpressionChoiceTL
	{
        public static BnfiExpressionChoiceTL operator +(BnfiExpressionChoiceTL term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiTermKeyTermPunctuation term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionChoiceTL term1, GrammarHint term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(GrammarHint term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionChoiceTL term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator +(BnfiExpressionKeyTermPunctuations term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionChoiceTL '|' operators for BnfExpression

	public partial class BnfiExpressionChoiceTL
	{
        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiTermConversion term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiTermConversion term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiExpressionConversionTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionConversionTL term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoiceTL operator |(BnfiExpressionChoiceTL term1, BnfiExpressionChoiceTL term2)
        {
            return (BnfiExpressionChoiceTL)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionChoice<T> definition and operators

	#region BnfiExpressionChoice<T> definition

	public partial class BnfiExpressionChoice<T> : BnfiExpression, IBnfiExpression<T>, IBnfiTermOrAbleForChoice<T>
	{
		#region Construction

		public BnfiExpressionChoice()
		{
		}

		public BnfiExpressionChoice(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionChoice(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionChoice<T>(BnfExpression bnfExpression)
		{
			return new BnfiExpressionChoice<T>(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermRecord<TType>, BnfiTermConstant<T>, BnfiTermChoice<TType>, BnfiTermCollectionWithCollectionType<TCollectionType>]: implicit conversions from BnfiExpressionChoice<T>

	public partial class BnfiTermRecord<TType>
	{
		public static implicit operator BnfiExpressionChoice<TType>(BnfiTermRecord<TType> term)
		{
			return new BnfiExpressionChoice<TType>((BnfTerm)term);
		}
	}

	public partial class BnfiTermConstant<T>
	{
		public static implicit operator BnfiExpressionChoice<T>(BnfiTermConstant<T> term)
		{
			return new BnfiExpressionChoice<T>((BnfTerm)term);
		}
	}

	public partial class BnfiTermChoice<TType>
	{
		public static implicit operator BnfiExpressionChoice<TType>(BnfiTermChoice<TType> term)
		{
			return new BnfiExpressionChoice<TType>((BnfTerm)term);
		}
	}

	public partial class BnfiTermCollectionWithCollectionType<TCollectionType>
	{
		public static implicit operator BnfiExpressionChoice<TCollectionType>(BnfiTermCollectionWithCollectionType<TCollectionType> term)
		{
			return new BnfiExpressionChoice<TCollectionType>((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiExpressionChoice<T>: implicit conversions from [BnfiTermConversion<T>, BnfiExpressionConversion<T>]

	public partial class BnfiExpressionChoice<T>
	{
		public static implicit operator BnfiExpressionChoice<T>(BnfiTermConversion<T> term)
		{
			return new BnfiExpressionChoice<T>((BnfTerm)term);
		}
		public static implicit operator BnfiExpressionChoice<T>(BnfiExpressionConversion<T> term)
		{
			return new BnfiExpressionChoice<T>((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermRecord<TType> '+' operators for BnfExpression

	public partial class BnfiTermRecord<TType>
	{
        public static BnfiExpressionChoice<TType> operator +(BnfiTermRecord<TType> term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermKeyTermPunctuation term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermRecord<TType> term1, GrammarHint term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(GrammarHint term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermRecord<TType> term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermConstant<T> '+' operators for BnfExpression

	public partial class BnfiTermConstant<T>
	{
        public static BnfiExpressionChoice<T> operator +(BnfiTermConstant<T> term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiTermKeyTermPunctuation term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiTermConstant<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(GrammarHint term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiTermConstant<T> term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermChoice<TType> '+' operators for BnfExpression

	public partial class BnfiTermChoice<TType>
	{
        public static BnfiExpressionChoice<TType> operator +(BnfiTermChoice<TType> term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermKeyTermPunctuation term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermChoice<TType> term1, GrammarHint term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(GrammarHint term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiTermChoice<TType> term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCollectionWithCollectionType<TCollectionType> '+' operators for BnfExpression

	public partial class BnfiTermCollectionWithCollectionType<TCollectionType>
	{
        public static BnfiExpressionChoice<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator +(BnfiTermKeyTermPunctuation term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, GrammarHint term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator +(GrammarHint term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator +(BnfiExpressionKeyTermPunctuations term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermRecord<TType> '|' operators for BnfExpression

	public partial class BnfiTermRecord<TType>
	{
        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiTermConversion<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermConversion<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiExpressionConversion<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiExpressionConversion<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiTermConstant<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermConstant<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiTermCollectionWithCollectionType<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermCollectionWithCollectionType<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermRecord<TType> term1, BnfiExpressionChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiExpressionChoice<TType> term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermConstant<T> '|' operators for BnfExpression

	public partial class BnfiTermConstant<T>
	{
        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConversion<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiExpressionConversion<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiTermChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermChoice<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiTermCollectionWithCollectionType<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermCollectionWithCollectionType<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConstant<T> term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiExpressionChoice<T> term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermChoice<TType> '|' operators for BnfExpression

	public partial class BnfiTermChoice<TType>
	{
        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiTermConversion<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermConversion<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiExpressionConversion<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiExpressionConversion<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiTermCollectionWithCollectionType<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermCollectionWithCollectionType<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiTermChoice<TType> term1, BnfiExpressionChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TType> operator |(BnfiExpressionChoice<TType> term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionChoice<TType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCollectionWithCollectionType<TCollectionType> '|' operators for BnfExpression

	public partial class BnfiTermCollectionWithCollectionType<TCollectionType>
	{
        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiTermConversion<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiTermConversion<TCollectionType> term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiExpressionConversion<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiExpressionConversion<TCollectionType> term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiExpressionChoice<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<TCollectionType> operator |(BnfiExpressionChoice<TCollectionType> term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionChoice<TCollectionType>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionChoice<T> '+' operators for BnfExpression

	public partial class BnfiExpressionChoice<T>
	{
        public static BnfiExpressionChoice<T> operator +(BnfiExpressionChoice<T> term1, BnfiTermKeyTermPunctuation term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiTermKeyTermPunctuation term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiExpressionChoice<T> term1, GrammarHint term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(GrammarHint term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiExpressionChoice<T> term1, BnfiExpressionKeyTermPunctuations term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator +(BnfiExpressionKeyTermPunctuations term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionChoice<T> '|' operators for BnfExpression

	public partial class BnfiExpressionChoice<T>
	{
        public static BnfiExpressionChoice<T> operator |(BnfiExpressionChoice<T> term1, BnfiTermConversion<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiTermConversion<T> term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiExpressionChoice<T> term1, BnfiExpressionConversion<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiExpressionConversion<T> term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionChoice<T> operator |(BnfiExpressionChoice<T> term1, BnfiExpressionChoice<T> term2)
        {
            return (BnfiExpressionChoice<T>)BnfiExpression.Op_Pipe((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

	#region BnfiExpressionGeneral<T> definition and operators

	#region BnfiExpressionGeneral<T> definition

	public partial class BnfiExpressionGeneral<T> : BnfiExpression, IBnfiExpression<T>
	{
		#region Construction

		public BnfiExpressionGeneral()
		{
		}

		public BnfiExpressionGeneral(BnfExpression bnfExpression)
			: base(bnfExpression)
		{
		}

        public BnfiExpressionGeneral(BnfTerm bnfTerm)
			: base(bnfTerm)
        {
        }

		#endregion

		#region Cast operators

		public static explicit operator BnfiExpressionGeneral<T>(BnfExpression bnfExpression)
		{
			return new BnfiExpressionGeneral<T>(bnfExpression);
		}

		#endregion
	}

	#endregion

	#region [BnfiTermRecord<TType>, BnfiTermConstant<T>, BnfiTermChoice<TType>, BnfiTermCollectionWithCollectionType<TCollectionType>]: implicit conversions from BnfiExpressionGeneral<T>

	public partial class BnfiTermRecord<TType>
	{
		public static implicit operator BnfiExpressionGeneral<TType>(BnfiTermRecord<TType> term)
		{
			return new BnfiExpressionGeneral<TType>((BnfTerm)term);
		}
	}

	public partial class BnfiTermConstant<T>
	{
		public static implicit operator BnfiExpressionGeneral<T>(BnfiTermConstant<T> term)
		{
			return new BnfiExpressionGeneral<T>((BnfTerm)term);
		}
	}

	public partial class BnfiTermChoice<TType>
	{
		public static implicit operator BnfiExpressionGeneral<TType>(BnfiTermChoice<TType> term)
		{
			return new BnfiExpressionGeneral<TType>((BnfTerm)term);
		}
	}

	public partial class BnfiTermCollectionWithCollectionType<TCollectionType>
	{
		public static implicit operator BnfiExpressionGeneral<TCollectionType>(BnfiTermCollectionWithCollectionType<TCollectionType> term)
		{
			return new BnfiExpressionGeneral<TCollectionType>((BnfTerm)term);
		}
	}

	#endregion

	#region BnfiTermRecord<TType> '+' operators for BnfExpression

	public partial class BnfiTermRecord<TType>
	{
        public static BnfiExpressionGeneral<TType> operator +(BnfiTermRecord<TType> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermNoAst term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermRecord<TType> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermKeyTerm term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermRecord<TType> term1, Terminal term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(Terminal term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermRecord<TType> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiExpressionTerminals term1, BnfiTermRecord<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermConstant<T> '+' operators for BnfExpression

	public partial class BnfiTermConstant<T>
	{
        public static BnfiExpressionGeneral<T> operator +(BnfiTermConstant<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermNoAst term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermConstant<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermKeyTerm term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermConstant<T> term1, Terminal term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(Terminal term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermConstant<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionTerminals term1, BnfiTermConstant<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermChoice<TType> '+' operators for BnfExpression

	public partial class BnfiTermChoice<TType>
	{
        public static BnfiExpressionGeneral<TType> operator +(BnfiTermChoice<TType> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermNoAst term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermChoice<TType> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermKeyTerm term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermChoice<TType> term1, Terminal term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(Terminal term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiTermChoice<TType> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TType> operator +(BnfiExpressionTerminals term1, BnfiTermChoice<TType> term2)
        {
            return (BnfiExpressionGeneral<TType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiTermCollectionWithCollectionType<TCollectionType> '+' operators for BnfExpression

	public partial class BnfiTermCollectionWithCollectionType<TCollectionType>
	{
        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermNoAst term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermKeyTerm term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, Terminal term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(Terminal term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiTermCollectionWithCollectionType<TCollectionType> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<TCollectionType> operator +(BnfiExpressionTerminals term1, BnfiTermCollectionWithCollectionType<TCollectionType> term2)
        {
            return (BnfiExpressionGeneral<TCollectionType>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#region BnfiExpressionGeneral<T> '+' operators for BnfExpression

	public partial class BnfiExpressionGeneral<T>
	{
        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionGeneral<T> term1, BnfiTermNoAst term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermNoAst term1, BnfiExpressionGeneral<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionGeneral<T> term1, BnfiTermKeyTerm term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiTermKeyTerm term1, BnfiExpressionGeneral<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionGeneral<T> term1, Terminal term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(Terminal term1, BnfiExpressionGeneral<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionGeneral<T> term1, BnfiExpressionTerminals term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

        public static BnfiExpressionGeneral<T> operator +(BnfiExpressionTerminals term1, BnfiExpressionGeneral<T> term2)
        {
            return (BnfiExpressionGeneral<T>)BnfiExpression.Op_Plus((BnfTerm)term1, (BnfTerm)term2);
        }

	}

	#endregion

	#endregion

}
