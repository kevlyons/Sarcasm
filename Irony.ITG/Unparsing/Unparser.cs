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
using Irony.ITG;
using Irony.ITG.Ast;

using Grammar = Irony.ITG.Ast.Grammar;

namespace Irony.ITG.Unparsing
{
    public class Unparser : IUnparser
    {
        internal const string logDirectoryName = "unparse_logs";

        internal readonly static TraceSource tsUnparse = new TraceSource("UNPARSE", SourceLevels.Verbose);

#if DEBUG
        static Unparser()
        {
            Directory.CreateDirectory(logDirectoryName);

            Trace.AutoFlush = true;

            tsUnparse.Listeners.Clear();
            tsUnparse.Listeners.Add(new TextWriterTraceListener(File.Create(Path.Combine(logDirectoryName, "00_unparse.log"))));
        }
#endif

        public Grammar Grammar { get; private set; }
        public Formatting Formatting { get; private set; }

        private readonly Formatter formatter;

        public Unparser(Grammar grammar)
            : this(grammar, grammar.DefaultFormatting)
        {
        }

        public Unparser(Grammar grammar, Formatting formatting)
        {
            this.Grammar = grammar;
            this.Formatting = formatting;

            this.formatter = new Formatter(formatting);
        }

        public IEnumerable<Utoken> Unparse(object obj, Context context = null)
        {
            BnfTerm bnfTerm = GetBnfTerm(obj, context);
            return Unparse(obj, bnfTerm);
        }

        public IEnumerable<Utoken> Unparse(object obj, BnfTerm bnfTerm)
        {
            return UnparseRaw(obj, bnfTerm)
                .Cook();
        }

        bool steppedIntoUnparseRaw = false;

        private IEnumerable<Utoken> UnparseRaw(object obj, BnfTerm bnfTerm)
        {
            if (bnfTerm == null)
                throw new ArgumentNullException("bnfTerm must not be null", "bnfTerm");

            Formatter.BeginParams beginParams;

            formatter.Begin(bnfTerm, out beginParams);

            try
            {
                foreach (var utoken in formatter.YieldBefore(bnfTerm, beginParams))
                    yield return utoken;

                steppedIntoUnparseRaw = true;

                if (bnfTerm is KeyTerm)
                {
                    Unparser.tsUnparse.Debug("keyterm: [{0}]", ((KeyTerm)bnfTerm).Text);
                    yield return Utoken.CreateText(((KeyTerm)bnfTerm).Text, obj);
                }
                else if (bnfTerm is Terminal)
                {
                    Unparser.tsUnparse.Debug("terminal: [\"{0}\"]", obj.ToString());
                    yield return Utoken.CreateText(obj);
                }
                else if (bnfTerm is NonTerminal)
                {
                    Unparser.tsUnparse.Debug("nonterminal: {0}", bnfTerm.Name);

                    Unparser.tsUnparse.Indent();

                    try
                    {
                        NonTerminal nonTerminal = (NonTerminal)bnfTerm;
                        IUnparsable unparsable = nonTerminal as IUnparsable;

                        if (unparsable == null)
                            throw new CannotUnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' is not IUnparsable.", obj, obj.GetType().Name, nonTerminal.Name));

                        steppedIntoUnparseRaw = false;

                        IEnumerable<Utoken> utokens;

                        if (unparsable.TryGetUtokensDirectly(this, obj, out utokens))
                        {
                            foreach (Utoken utoken in utokens)
                                yield return utoken;
                        }
                        else
                        {
                            // TODO: we should check whether any bnfTermList has an UnparseHint

                            var chosenChildBnfTermsWithPriority = GetChildBnfTermLists(nonTerminal)
                                .Select(childBnfTerms =>
                                    {
                                        var childValues = unparsable.GetChildValues(childBnfTerms, obj);
                                        return new
                                            {
                                                ChildValues = childValues,
                                                Priority = unparsable.GetChildBnfTermListPriority(this, obj, childValues),
                                            };
                                    }
                                )
                                .Where(childBnfTermsWithPriority => childBnfTermsWithPriority.Priority.HasValue)
                                .OrderByDescending(childBnfTermsWithPriority => childBnfTermsWithPriority.Priority.Value)
                                .FirstOrDefault();

                            if (chosenChildBnfTermsWithPriority == null)
                                throw new CannotUnparseException(string.Format("Cannot unparse '{0}' (type: '{1}'). BnfTerm '{2}' has no approriate production rule.", obj, obj.GetType().Name, bnfTerm.Name));

                            foreach (Value childValue in chosenChildBnfTermsWithPriority.ChildValues)
                                foreach (Utoken utoken in UnparseRaw(childValue.obj, childValue.bnfTerm))
                                    yield return utoken;
                        }

                        if (!steppedIntoUnparseRaw)
                            Unparser.tsUnparse.Debug("utokenized: [{0}]", obj != null ? string.Format("\"{0}\"", obj) : "<<NULL>>");

                        steppedIntoUnparseRaw = true;
                    }
                    finally
                    {
                        Unparser.tsUnparse.Unindent();
                    }

                    foreach (var utoken in formatter.YieldAfter(bnfTerm))
                        yield return utoken;
                }
                else if (bnfTerm is GrammarHint)
                {
                    // GrammarHint is legal, but it does not need any unparse
                }
                else
                {
                    throw new ArgumentException(string.Format("bnfTerm '{0}' with unknown type: '{1}'" + bnfTerm.Name, bnfTerm.GetType().Name));
                }
            }
            finally
            {
                formatter.End(bnfTerm);
            }
        }

        internal static IEnumerable<BnfTermList> GetChildBnfTermLists(NonTerminal nonTerminal)
        {
            return nonTerminal.Productions.Select(production => production.RValues);
        }

        private BnfTerm GetBnfTerm(object obj, Context context)
        {
            return Grammar.Root;
            // TODO: do this by choosing by context
        }

        IEnumerable<Utoken> IUnparser.Unparse(BnfTerm bnfTerm, object obj)
        {
            return UnparseRaw(obj, bnfTerm);
        }

        int? IUnparser.GetBnfTermPriority(BnfTerm bnfTerm, object obj)
        {
            if (bnfTerm is NonTerminal && bnfTerm is IUnparsable)
            {
                NonTerminal nonTerminal = (NonTerminal)bnfTerm;
                IUnparsable unparsable = (IUnparsable)bnfTerm;

                return GetChildBnfTermLists(nonTerminal)
                    .Max(childBnfTerms => unparsable.GetChildBnfTermListPriority(this, obj, unparsable.GetChildValues(childBnfTerms, obj)));
            }
            else
            {
                return 0;
            }
        }

        IFormatProvider IUnparser.FormatProvider { get { return this.FormatProvider; } }

        protected IFormatProvider FormatProvider { get { return this.Formatting.FormatProvider; } }

        public static string ToString(IFormatProvider formatProvider, object obj)
        {
            return string.Format(formatProvider, "{0}", obj);
        }
    }

    public static class UnparserExtensions
    {
        public static string AsString(this IEnumerable<Utoken> utokens, Unparser unparser)
        {
            return string.Concat(utokens.Select(utoken => utoken.ToString(unparser.Formatting)));
        }

        public static async void WriteToStreamAsync(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (Utoken utoken in utokens)
                    await sw.WriteAsync(utoken.ToString(unparser.Formatting));
            }
        }

        internal static IEnumerable<Utoken> Cook(this IEnumerable<Utoken> utokens)
        {
            return Formatter.PostProcess(utokens);
        }
    }

    public class Context
    {
        public MemberInfo MemberInfo { get; private set; }

        public Context(MemberInfo memberInfo)
        {
            this.MemberInfo = memberInfo;
        }
    }

    public interface IUnparsable
    {
        bool TryGetUtokensDirectly(IUnparser unparser, object obj, out IEnumerable<Utoken> utokens);
        IEnumerable<Value> GetChildValues(BnfTermList childBnfTerms, object obj);
        int? GetChildBnfTermListPriority(IUnparser unparser, object obj, IEnumerable<Value> childValues);
    }

    public interface IUnparser
    {
        IEnumerable<Utoken> Unparse(BnfTerm bnfTerm, object obj);
        int? GetBnfTermPriority(BnfTerm bnfTerm, object obj);
        IFormatProvider FormatProvider { get; }
    }

    public class Value
    {
        public readonly BnfTerm bnfTerm;
        public readonly object obj;

        public Value(BnfTerm bnfTerm, object obj)
        {
            this.bnfTerm = bnfTerm;
            this.obj = obj;
        }
    }
}
