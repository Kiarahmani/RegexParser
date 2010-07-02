﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexParser
{
    public class Match2
    {
        private Match2()
        {
            Success = false;
        }

        internal Match2(int index, int length, string value, Func<Match2> nextMatch)
        {
            Success = true;

            Index = index;
            Length = length;
            Value = value;

            this.nextMatch = nextMatch;
        }

        public bool Success { get; private set; }

        public int Index { get; private set; }
        public int Length { get; private set; }
        public string Value { get; private set; }

        public override string ToString() { return Value; }

        public Match2 NextMatch()
        {
            return nextMatch();
        }
        private Func<Match2> nextMatch = () => Match2.Empty;

        public static Match2 Empty { get { return new Match2(); } }
    }
}
