using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{
    class AlterNode : BaseTreeNode
    {
        public BaseTreeNode left { get; private set; }
        public BaseTreeNode right { get; private set; }

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
            return "(" + left.ToStringWithDelim() + ")|(" + right.ToStringWithDelim() + ")";
        }

        public override string ToStringWithoutDelim()
        {
            throw new NotImplementedException();
        }
    }
}
