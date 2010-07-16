﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RegexParser.Matchers;
using RegexParser.Tests.Helpers;

namespace RegexParser.Tests.Matchers
{
    [TestFixture(AlgorithmType.ImplicitDFA)]
    public class GroupPatternMatcherTests : MatcherTests
    {
        public GroupPatternMatcherTests(AlgorithmType algorithmType)
            : base(algorithmType) { }

        [Test]
        public void Grouping()
        {
            string input = "A thing or another thing";

            string[] patterns = new[] {
                "th(in)g",
                "(thing)",
                "t(hi)n(g)",
                "t(h(i)n)g",
                "th(((((in)))))g"
            };

            RegexAssert.AreMatchesSameAsMsoft(input, patterns, AlgorithmType);
        }

        [Test]
        public void FalseStart()
        {
            string input = "Something or other";

            string[] patterns = new[] {
                "Som(me)",
                "So(mme)",
                "So(m)",
                "So(m)e"
            };

            RegexAssert.AreMatchesSameAsMsoft(input, patterns, AlgorithmType);
        }

        //[Test]
        //public void Grouping_ErrorHandling()
        //{
        //    string input = "A thing or another thing";

        //    Assert.Catch<ArgumentException>(() => { new Regex2("th((in)g", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2("th((in)))g", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2("(t(h(in))g", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2("thing)))", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2("(thing", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2(")thing", AlgorithmType).Match(input); });
        //    Assert.Catch<ArgumentException>(() => { new Regex2("thi)ng", AlgorithmType).Match(input); });
        //}
    }
}
