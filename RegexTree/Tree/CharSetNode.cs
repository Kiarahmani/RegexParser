using RegexParser.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegexTree.Tree
{
    class CharSetNode : BaseTreeNode
    {

        public BaseCharSet CharSet;

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
            return CharSet.ToString();
        }

        public override string ToStringWithoutDelim()
        {
            throw new NotImplementedException();
        }
    }
}
