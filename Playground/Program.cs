﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Globalization;

using Irony;
using Irony.Ast;
using Irony.Parsing;
using Sarcasm;
using Sarcasm.GrammarAst;
using Sarcasm.UniversalGrammars;
using Sarcasm.Parsing;
using Sarcasm.Unparsing;
using Sarcasm.Utility;

using MiniPLG = MiniPL.Grammars;
using ParseTree = Sarcasm.Parsing.ParseTree;

namespace Playground
{
    class Program
    {
        static string path = @"MiniPL_long.mplp";
//        static string path2 = @"..\..\..\Sarcasm.UnitTest\Test files\Binary1.expr";

        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();

            stopwatch.Start();
            var grammar = new MiniPLG.GrammarP();
            ShowTimeAndRestart(stopwatch, "Creation of grammar");

            var parser = MultiParser.Create(grammar);
            ShowTimeAndRestart(stopwatch, "Creation of parser");

            var parseTree = parser.Parse(File.ReadAllText(path), path);
            ShowTimeAndRestart(stopwatch, "Parsing");

            var astRootValue = parseTree.RootAstValue;

#if false
            var universalGrammar = new JsonGrammar();

            Unparser universalUnparser = new Unparser(universalGrammar);
            string text = universalUnparser.Unparse(astRootValue).AsText(universalUnparser);

            var universalParser = MultiParser.Create(universalGrammar);
            var parseTree2 = universalParser.Parse(text);
            var astRootValue2 = parseTree2.RootAstValue;

            string text2 = universalUnparser.Unparse(astRootValue2).AsText(universalUnparser);
#endif

            Unparser unparser = new Unparser(grammar);
            ShowTimeAndRestart(stopwatch, "Creation of unparser");

            //var utokens = unparser.Unparse(astRootValue).ToList();
            //ShowTimeAndRestart(stopwatch, "Unparsing to utokens");

            //string unparsedText = utokens.AsString(unparser);
            //ShowTimeAndRestart(stopwatch, "Converting utokens to string");

            unparser.EnableParallelProcessing = false;
            unparser.Unparse(astRootValue).ConsumeAll();
            ShowTimeAndRestart(stopwatch, "Sequential unparsing to string");

            unparser.EnableParallelProcessing = true;
            unparser.Unparse(astRootValue).ConsumeAll();
            ShowTimeAndRestart(stopwatch, "Parallel unparsing to string");

            //var utokensReverse = unparser.Unparse(astRootValue, Unparser.Direction.RightToLeft).ToList();
            //ShowTimeAndRestart(stopwatch, "Reverse unparsing to utokens");

            unparser.EnableParallelProcessing = false;
            unparser.Unparse(astRootValue, Unparser.Direction.RightToLeft).ConsumeAll();
            ShowTimeAndRestart(stopwatch, "Reverse sequential unparsing to string");

            unparser.EnableParallelProcessing = true;
            unparser.Unparse(astRootValue, Unparser.Direction.RightToLeft).ConsumeAll();
            ShowTimeAndRestart(stopwatch, "Reverse parallel unparsing to string");

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //stopwatch.Start();
            //var parser2 = MultiParser.Create(grammar);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //var parseTree2 = parser.Parse(File.ReadAllText(path2), path2, grammar.B.Expression);
            //var parseTree3 = parser.Parse(File.ReadAllText(path2), path2, (NonTerminal)grammar.B.Expression);
            //MiniPL.DomainModel.Expression astValue2 = parseTree2.RootAstValue;

            //Unparser unparser = new Unparser(grammar);

            //Directory.CreateDirectory("unparse_logs");
            //var stream = File.Create(@"unparse_logs\unparsed_text");
            //unparser.Unparse(parseTree.Root.AstNode).WriteToStream(stream, unparser);

            ////string str = unparser.Unparse(parseTree.Root.AstNode).AsString(unparser);
            ////Console.WriteLine(str);
        }

        private static TimeSpan ShowTimeAndRestart(Stopwatch stopwatch, string message)
        {
            stopwatch.Stop();
            var timeSpan = stopwatch.Elapsed;
            Console.WriteLine("{0}: time elapsed = {1}", message, stopwatch.Elapsed);
            stopwatch.Restart();
            return timeSpan;
        }
    }
}
