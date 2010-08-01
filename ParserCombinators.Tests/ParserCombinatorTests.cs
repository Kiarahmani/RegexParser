﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ParserCombinators;
using ParserCombinators.ConsLists;
using ParserCombinators.Tests.MiniML;

namespace ParserCombinators.Tests
{
    [TestFixture]
    public class ParserCombinatorTests
    {
        [Test]
        public void MiniML_StringConsList()
        {
            miniMLTest(s => new StringConsList(s));
        }

        [Test]
        public void MiniML_LinkedConsList()
        {
            miniMLTest(s => new LinkedConsList<char>(s));
        }

        [Test]
        public void MiniML_ArrayConsList()
        {
            miniMLTest(s => new ArrayConsList<char>(s));
        }

        private void miniMLTest(Func<string, IConsList<char>> createConsList)
        {
            string sourceCode = @"let true = \x.\y.x in 
                                  let false = \x.\y.y in 
                                  let if = \b.\l.\r.(b l) r in
                                  if true false true;";

            Result<char, Term> result = MiniMLParsers.All(createConsList(sourceCode)).FirstOrDefault();

            string expected = @"
let true = \x. \y. (x ) in
let false = \x. \y. (y ) in
let if = \b. \l. \r. ((b l) r) in
(if true false true)"
                .TrimStart();

            Assert.True(result.Rest.IsEmpty, "Rest.IsEmpty.");
            Assert.AreEqual(expected, result.Value.ToString(), "Value.");
        }
    }
}
