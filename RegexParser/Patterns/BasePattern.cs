﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParserCombinators.ConsLists;
using ParserCombinators.Util;

namespace RegexParser.Patterns
{
    public abstract class BasePattern
    {
        public static BasePattern CreatePattern(string patternText)
        {
            var result = PatternParsers.Regex(new ArrayConsList<char>(patternText)).FirstOrDefault();

            if (result.Rest.IsEmpty)
                return result.Value;
            else
                throw new ArgumentException(
                                string.Format("Could not understand part of the regex pattern: {0}.",
                                              result.Rest.AsEnumerable().AsString().ShowVerbatim()),
                                "patternText.");
        }
    }
}
