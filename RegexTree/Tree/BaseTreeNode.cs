using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{

    public enum TreeNodeType
    {
        Root,
        CharacterSet,
        Alternation,
        QuantifierMin,
        Quantifier,
        Concatenation,
        Group
    }

    public abstract class BaseTreeNode
    {
        public TreeNodeType Type { get; protected set; }

        public abstract string ToStringWithDelim();
        public abstract string ToStringWithoutDelim();
        public abstract string ToStringTree();
        public abstract void FromPattern(GroupPattern gp);


        public override string ToString()
        {
            return ToStringWithoutDelim();
        }
    }

}
