﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegexParser.ParserCombinators.ConsLists;

namespace RegexParser.ParserCombinators
{
    public class Result<TToken, TValue>
    {
        public Result(TValue value, IConsList<TToken> rest)
        {
            Value = value;
            Rest = rest;
        }

        public TValue Value { get; private set; }

        public IConsList<TToken> Rest { get; private set; }
    }
}
