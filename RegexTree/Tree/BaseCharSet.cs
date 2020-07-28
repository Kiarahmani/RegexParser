using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{

    public enum CharSetType
    {
        Literal,
        Union,
        Range,
        Negation
    }

    public abstract class BaseCharSet
    {
        public CharSetType Type { get; private set; }

        protected abstract string ToPrettyString();

        public override string ToString()
        {
            return ToPrettyString();
        }


    }
}
