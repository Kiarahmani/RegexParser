using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{
    class QuantNode : BaseTreeNode
    {
        public BaseTreeNode child { get; private set; }
        public int min { get; private set; }
        public int max { get; private set; }

        public override void FromPattern(GroupPattern gp)
        {
            throw new NotImplementedException();
        }

        public override string ToStringTree()
        {
            throw new NotImplementedException();
        }

        public override string ToStringWithDelim()
        {
            return "(" + child.ToStringWithDelim() + "){" + min + "," + max + "}";
        }

        public override string ToStringWithoutDelim()
        {
            throw new NotImplementedException();
        }
    }
}
