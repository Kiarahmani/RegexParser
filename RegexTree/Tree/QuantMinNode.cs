using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{
    class QuantMinNode : BaseTreeNode
    {
        public BaseTreeNode child { get; private set; }
        public int min { get; private set; }

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
            if (min == 0)
                return "(" + child.ToStringWithDelim() + ")*";
            else if (min == 1)
                return "(" + child.ToStringWithDelim() + ")+";
            else
                return "(" + child.ToStringWithDelim() + "){" + min + ",}";
        }

        public override string ToStringWithoutDelim()
        {
            throw new NotImplementedException();
        }
    }
}
