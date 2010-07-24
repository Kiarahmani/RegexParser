﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegexParser.Util;

namespace RegexParser.Patterns
{
    public class QuantifierPattern : BasePattern, IEquatable<QuantifierPattern>
    {
        public QuantifierPattern(BasePattern childPattern, int minOccurrences, int? maxOccurrences, bool isGreedy)
        {
            if (childPattern == null)
                throw new ArgumentNullException("childPattern.", "Child pattern is null in quantifier pattern.");

            ChildPattern = childPattern;
            MinOccurrences = minOccurrences;
            MaxOccurrences = maxOccurrences;
            IsGreedy = isGreedy;
        }

        public BasePattern ChildPattern { get; private set; }

        public int MinOccurrences { get; private set; }
        public int? MaxOccurrences { get; private set; }

        public bool IsGreedy { get; private set; }

        public override string ToString()
        {
            return string.Format("Quant {{{0}, Min={1}{2}{3}}}",
                                 ChildPattern.ToString(),
                                 MinOccurrences,
                                 MaxOccurrences != null ? string.Format(", Max={0}", MaxOccurrences) : "",
                                 IsGreedy ? "" : string.Format(", IsGreedy={0}", IsGreedy));
        }

        bool IEquatable<QuantifierPattern>.Equals(QuantifierPattern other)
        {
            return other != null &&
                   this.ChildPattern.Equals(other.ChildPattern) &&
                   this.MinOccurrences == other.MinOccurrences &&
                   this.MaxOccurrences == other.MaxOccurrences &&
                   this.IsGreedy == other.IsGreedy;
        }

        public override int GetHashCode()
        {
            return HashCodeCombiner.Combine(ChildPattern.GetHashCode(), MinOccurrences.GetHashCode(), MaxOccurrences.GetHashCode(),
                                            IsGreedy.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return ((IEquatable<QuantifierPattern>)this).Equals(obj as QuantifierPattern);
        }
    }
}