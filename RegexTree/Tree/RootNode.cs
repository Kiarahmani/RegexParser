using System;
using System.Collections.Generic;
using System.Text;
using RegexParser.Patterns;
using Utility.ConsLists;
using ParserCombinators;

namespace RegexTree.Tree
{
    public class RootNode : BaseTreeNode
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public BaseTreeNode RootGroup { get; private set; }
        public bool IsModified { get; }
        public string OriginalRegex { get; }
        public GroupPattern InputPattern { get; }

        public RootNode(string regex)
        {
            var result = PatternParsers.Regex(new ArrayConsList<char>(regex));
            if (result.Rest.IsEmpty)
                InputPattern = result.Tree;
            else
                throw new ArgumentException(string.Format("RegexParse could not understand part of the regex pattern: {0}.", result.Rest), "regex.");
            Type = TreeNodeType.Root;
            IsModified = false;
            OriginalRegex = regex;
            RootGroup = new GroupNode();
            RootGroup.FromPattern(InputPattern);
        }



        public override void FromPattern(GroupPattern gp)
        {
            throw new InvalidOperationException("Root nodes can only be initialized from a string pattern");
        }

        public override string ToStringTree()
        {
            return RootGroup.ToStringTree();
        }
        public override string ToStringWithDelim()
        {
            return RootGroup.ToStringWithDelim();
        }

        public override string ToStringWithoutDelim()
        {
            return RootGroup.ToStringWithoutDelim();
        }
    }
}
