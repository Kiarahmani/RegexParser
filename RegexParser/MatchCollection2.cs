﻿using System.Collections.Generic;
using System.Linq;
using Utility.BaseTypes;
using Utility.Collections;

namespace RegexParser
{
    public class MatchCollection2 : CachedList<Match2>
    {
        internal MatchCollection2(IEnumerable<Match2> matches)
            : base(matches)
        {
        }

        protected override void OnNextOriginal(Match2 match, int originalIndex)
        {
            match.Parent = this;
            match.ParentIndex = originalIndex;
        }

        public int Count { get { return this.Count(); } }

        public override string ToString()
        {
            return string.Format("MatchColl <{0}>",
                                 this.Select(m => m.ToString()).JoinStrings(", "));
        }
    }
}
