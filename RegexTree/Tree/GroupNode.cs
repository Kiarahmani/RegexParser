using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{
    class GroupNode : BaseTreeNode
    {

        public BaseTreeNode child { get; private set; }

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
            return "(" + child.ToStringWithDelim() + ")";
        }

        public override string ToStringWithoutDelim()
        {
            throw new NotImplementedException();
        }
    }
}
