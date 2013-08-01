﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.GrammarAst;
using Sarcasm.Utility;
using System.Threading;

namespace Sarcasm.Unparsing
{
    public static class UnparserExtensions
    {
        public static string AsText(this IEnumerable<Utoken> utokens, Unparser unparser)
        {
            return string.Concat(utokens.Select(utoken => utoken.ToText(unparser.Formatter)));
        }

        public static async Task<string> AsTextAsync(this IEnumerable<Utoken> utokens, Unparser unparser)
        {
            return await Task.Run(() => utokens.AsText(unparser));
        }

        public static async Task<string> AsTextAsync(this IEnumerable<Utoken> utokens, Unparser unparser, CancellationToken cancellationToken)
        {
            return await Task.Run(
                () =>
                    string.Concat(
                        utokens.Select(
                            utoken =>
                                {
                                    cancellationToken.ThrowIfCancellationRequested();
                                    return utoken.ToText(unparser.Formatter);
                                }
                        )
                    ),
                cancellationToken
            );
        }

        public static void WriteToStream(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (Utoken utoken in utokens)
                    sw.Write(utoken.ToText(unparser.Formatter));
            }
        }

        public static async Task WriteToStreamAsync(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser)
        {
            await utokens.WriteToStreamAsync(stream, unparser, CancellationToken.None);
        }

        public static async Task WriteToStreamAsync(this IEnumerable<Utoken> utokens, Stream stream, Unparser unparser, CancellationToken cancellationToken)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (Utoken utoken in utokens)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await sw.WriteAsync(utoken.ToText(unparser.Formatter));
                }
            }
        }

        internal static IEnumerable<Utoken> Cook(this IEnumerable<UtokenBase> utokens, IPostProcessHelper postProcessHelper)
        {
            return Formatter.PostProcess(utokens, postProcessHelper);
        }
    }

    public delegate IEnumerable<UtokenValue> ValueUtokenizer<in T>(IFormatProvider formatProvider, UnparsableAst reference, T astValue);

    public interface IUnparsableNonTerminal : INonTerminal
    {
        bool TryGetUtokensDirectly(IUnparser unparser, UnparsableAst self, out IEnumerable<UtokenValue> utokens);
        IEnumerable<UnparsableAst> GetChildren(Unparser.ChildBnfTerms childBnfTerms, object astValue, Unparser.Direction direction);
        int? GetChildrenPriority(IUnparser unparser, object astValue, Unparser.Children children, Unparser.Direction direction);
    }

    public interface IUnparser
    {
        int? GetPriority(UnparsableAst unparsableAst);
        IFormatProvider FormatProvider { get; }
    }

    internal interface IPostProcessHelper
    {
        Formatter Formatter { get; }
        Unparser.Direction Direction { get; }
        Action<UnparsableAst> UnlinkChildFromChildPrevSiblingIfNotFullUnparse { get; }
    }
}
