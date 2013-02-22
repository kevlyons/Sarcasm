﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.Ast;
using System.Runtime.CompilerServices;

namespace Sarcasm.Unparsing
{
    public static class UnparserExtensions
    {
        public static string AsString(this IEnumerable<Utoken> utokens, Unparser unparser)
        {
            return string.Concat(utokens.Select(utoken => utoken.ToString(unparser.Formatting)));
        }

        public static async Task WriteToStreamAsync(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (Utoken utoken in utokens)
                    await sw.WriteAsync(utoken.ToString(unparser.Formatting));
            }
        }

        public static void WriteToStream(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser)
        {
            //            utokens.WriteToStreamAsync(stream, unparser).Wait();
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (Utoken utoken in utokens)
                    sw.Write(utoken.ToString(unparser.Formatting));
            }
        }

        internal static IEnumerable<Utoken> Cook(this IEnumerable<UtokenBase> utokens)
        {
            return Formatter.PostProcess(utokens);
        }
    }

    public delegate IEnumerable<UtokenValue> ValueUtokenizer<T>(IFormatProvider formatProvider, T obj);

    public interface IUnparsableNonTerminal : INonTerminal
    {
        bool TryGetUtokensDirectly(IUnparser unparser, object obj, out IEnumerable<UtokenValue> utokens);
        IEnumerable<UnparsableObject> GetChildren(BnfTermList childBnfTerms, object obj);
        int? GetChildrenPriority(IUnparser unparser, object obj, IEnumerable<UnparsableObject> children);
    }

    public interface IUnparser
    {
        int? GetPriority(UnparsableObject unparsableObject);
        IFormatProvider FormatProvider { get; }
    }

    public class UnparsableObject
    {
        private static readonly UnparsableObject nonCalculated = new UnparsableObject(null, null);

        public BnfTerm BnfTerm { get; private set; }
        public object Obj { get; private set; }

        private UnparsableObject parent = nonCalculated;
        private UnparsableObject leftMostChild = nonCalculated;
        private UnparsableObject rightMostChild = nonCalculated;
        private UnparsableObject leftSibling = nonCalculated;
        private UnparsableObject rightSibling = nonCalculated;

        public UnparsableObject Parent { get { return CheckIfCalculated(parent); } set { parent = value; } }
        public UnparsableObject LeftMostChild { get { return CheckIfCalculated(leftMostChild); } set { leftMostChild = value; } }
        public UnparsableObject RightMostChild { get { return CheckIfCalculated(rightMostChild); } set { rightMostChild = value; } }
        public UnparsableObject LeftSibling { get { return CheckIfCalculated(leftSibling); } set { leftSibling = value; } }
        public UnparsableObject RightSibling { get { return CheckIfCalculated(rightSibling); } set { rightSibling = value; } }

        public bool IsParentCalculated { get { return IsCalculated(parent); } }
        public bool IsLeftMostChildCalculated { get { return IsCalculated(leftMostChild); } }
        public bool IsRightMostChildCalculated { get { return IsCalculated(rightMostChild); } }
        public bool IsLeftSiblingCalculated { get { return IsCalculated(leftSibling); } }
        public bool IsRightSiblingCalculated { get { return IsCalculated(rightSibling); } }

        public UnparsableObject(BnfTerm bnfTerm, object obj)
        {
            this.BnfTerm = bnfTerm;
            this.Obj = obj;
        }

        public bool Equals(UnparsableObject that)
        {
            return object.ReferenceEquals(this, that)
                ||
                that != null &&
                this.BnfTerm == that.BnfTerm &&
                this.Obj == that.Obj;
        }

        public override bool Equals(object obj)
        {
            return obj is UnparsableObject && Equals((UnparsableObject)obj);
        }

        public override int GetHashCode()
        {
            return Util.GetHashCodeMulti(BnfTerm, Obj);
        }

        public static bool operator ==(UnparsableObject unparsableObject1, UnparsableObject unparsableObject2)
        {
            return object.ReferenceEquals(unparsableObject1, unparsableObject2) || !object.ReferenceEquals(unparsableObject1, null) && unparsableObject1.Equals(unparsableObject2);
        }

        public static bool operator !=(UnparsableObject unparsableObject1, UnparsableObject unparsableObject2)
        {
            return !(unparsableObject1 == unparsableObject2);
        }

        public override string ToString()
        {
            return IsCalculated(this)
                ? string.Format("[bnfTerm: {0}, obj: {1}]", BnfTerm, Obj)
                : "<<NonCalculated>>";
        }

        public void SetAsRoot()
        {
            Parent = null;
            LeftSibling = null;
            RightSibling = null;
        }

        public void SetAsLeave()
        {
            LeftMostChild = null;
            RightMostChild = null;
        }

        private UnparsableObject CheckIfCalculated(UnparsableObject relative, [CallerMemberName] string nameOfRelative = "")
        {
            if (!IsCalculated(relative))
            {
                return null;
                throw new InvalidOperationException(string.Format("Tried to use a non-calculated relative '{0}' for {1}", nameOfRelative, this));
            }

            return relative;
        }

        private static bool IsCalculated(UnparsableObject relative)
        {
            return !object.ReferenceEquals(relative, nonCalculated);
        }
    }
}
