﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParserCombinators;
using ParserCombinators.Tests.Performance;
using RegexParser.Patterns;

namespace RegexParser.Tests.Performance
{
    public static class PatternPerformanceTests
    {
        public static void CharClassPatternTest()
        {
            const int times = 200, maxItemCount = 100000;


            string digitChars = new string(EnumerablePerformanceTests.RepeatChars("0123456789", maxItemCount).ToArray());

            charClassPatternTest(new CharClassPattern(true, "0123456789"),
                                 times, maxItemCount, digitChars);
            // 2.39 sec.

            charClassPatternTest(new CharClassPattern(true, new[] { new CharRangePattern('0', '9') }),
                                 times, maxItemCount, digitChars);
            // 1.59 sec.


            string lowercaseChars = new string(EnumerablePerformanceTests.RepeatChars("abcdefghijklmnopqrstuvwxyz", maxItemCount).ToArray());

            charClassPatternTest(new CharClassPattern(true, "abcdefghijklmnopqrstuvwxyz"),
                                 times, maxItemCount, lowercaseChars);
            // 2.55 sec.

            charClassPatternTest(new CharClassPattern(true, new[] { new CharRangePattern('a', 'z') }),
                                 times, maxItemCount, lowercaseChars);
            // 1.65 sec.

            charTest("char.IsLetter", c => char.IsLetter(c),
                     times, maxItemCount, lowercaseChars);
            // 1.42 sec.


            charClassPatternTest(new CharClassPattern(true, "0123456789abcdefghijklmnopqrstuvwxyz"),
                                 times, maxItemCount, lowercaseChars);
            // 2.80 sec.

            charClassPatternTest(new CharClassPattern(true, new[] { new CharRangePattern('0', '9'),
                                                                    new CharRangePattern('a', 'z') }),
                                 times, maxItemCount, lowercaseChars);
            // 1.69 sec.

            charTest("char.IsLetterOrDigit", c => char.IsLetterOrDigit(c),
                     times, maxItemCount, lowercaseChars);
            // 1.64 sec.


            string repeatedChar = new string(EnumerablePerformanceTests.RepeatChars("7", maxItemCount).ToArray());

            charClassPatternTest(new CharClassPattern(true, "7"),
                                 times, maxItemCount, repeatedChar);
            // 1.97 sec.

            charClassPatternTest(new CharClassPattern(true, new[] { new CharRangePattern('7', '7') }),
                                 times, maxItemCount, repeatedChar);
            // 1.61 sec.


            // Negative character classes
            charClassPatternTest(new CharClassPattern(false, "abcdefghijklmnopqrstuvwxyz"),
                                 times, maxItemCount, digitChars);
            // 2.84 sec.

            charClassPatternTest(new CharClassPattern(false, new[] { new CharRangePattern('a', 'z') }),
                                 times, maxItemCount, digitChars);
            // 1.64 sec.
        }

        private static void charClassPatternTest(CharClassPattern charClassPattern, int times, int maxItemCount, string inputText)
        {
            charTest(charClassPattern.ToString(), c => charClassPattern.IsMatch(c), times, maxItemCount, inputText);
        }

        private static void charTest(string testName, Func<char, bool> predicate, int times, int maxItemCount, string inputText)
        {
            Console.WriteLine(testName);
            CharParsersPerformanceTests.CharParserTest(CharParsers.Satisfy(predicate),
                                                       times, maxItemCount, inputText);

            Console.WriteLine();
        }
    }
}
