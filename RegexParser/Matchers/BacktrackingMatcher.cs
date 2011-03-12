﻿using System;
using System.Linq;
using ParserCombinators;
using RegexParser.Patterns;
using RegexParser.Transforms;
using Utility.BaseTypes;
using Utility.ConsLists;

namespace RegexParser.Matchers
{
    public class BacktrackingMatcher : BaseMatcher
    {
        public BacktrackingMatcher(string patternText, RegexOptionsEx options)
            : base(patternText, options)
        {
        }

        protected override BasePattern TransformAST(BasePattern pattern)
        {
            pattern = base.TransformAST(pattern);

            return new QuantifierASTTransform().Transform(pattern);
        }

        protected override Result<char, string> Parse(ArrayConsList<char> consList)
        {
            BacktrackPoint lastBacktrackPoint = null;

            var callStack = new StackFrame(null, Pattern);
            var partialResult = new Result<char, int>(0, consList);

            while (callStack != null)
            {
                BasePattern currentPattern = callStack.RemainingChildren.Head;

                if (partialResult.Value + currentPattern.MinCharLength > consList.Length)
                    partialResult = null;
                else
                {
                    callStack = new StackFrame(callStack.Parent, callStack.RemainingChildren.Tail);

                    callStack = unwindEmptyFrames(callStack);

                    switch (currentPattern.Type)
                    {
                        case PatternType.Group:
                            callStack = new StackFrame(callStack, ((GroupPattern)currentPattern).Patterns);
                            break;


                        case PatternType.Quantifier:
                            var quant = (QuantifierPattern)currentPattern;

                            quant.AssertCanonicalForm();

                            if (quant.MinOccurrences == quant.MaxOccurrences)
                                callStack = new StackFrame(callStack,
                                                           new RepeaterConsList<BasePattern>(quant.ChildPattern,
                                                                                             quant.MinOccurrences));

                            else
                            {
                                IConsList<BasePattern> split = splitQuantifier(quant);

                                lastBacktrackPoint = new BacktrackPoint(lastBacktrackPoint,
                                                                        quant.IsGreedy ? callStack : new StackFrame(callStack, split),
                                                                        partialResult);

                                callStack = quant.IsGreedy ? new StackFrame(callStack, split) : callStack;
                            }
                            break;


                        case PatternType.Alternation:
                            var alternatives = ((AlternationPattern)currentPattern).Alternatives;

                            foreach (var alt in alternatives.Skip(1).Reverse())
                                lastBacktrackPoint = new BacktrackPoint(lastBacktrackPoint,
                                                                        new StackFrame(callStack, alt),
                                                                        partialResult);

                            callStack = new StackFrame(callStack, alternatives.First());
                            break;


                        case PatternType.Anchor:
                            if (!doesAnchorMatch((ArrayConsList<char>)partialResult.Rest,
                                                 ((AnchorPattern)currentPattern).AnchorType))
                                partialResult = null;
                            break;


                        case PatternType.Char:
                            partialResult = parseChar(partialResult, ((CharPattern)currentPattern).IsMatch);
                            break;


                        default:
                            throw new ApplicationException(
                                string.Format("BacktrackingMatcher: unrecognized pattern type ({0}).",
                                              currentPattern.GetType().Name));
                    }
                }

                if (partialResult != null)
                    callStack = unwindEmptyFrames(callStack);
                else
                {
                    if (lastBacktrackPoint != null)
                    {
                        callStack = lastBacktrackPoint.CallStack;
                        partialResult = lastBacktrackPoint.PartialResult;

                        lastBacktrackPoint = lastBacktrackPoint.Previous;
                    }
                    else
                        return null;
                }
            }

            return new Result<char, string>(consList.AsEnumerable().Take(partialResult.Value).AsString(),
                                            partialResult.Rest);
        }

        private StackFrame unwindEmptyFrames(StackFrame callStack)
        {
            while (callStack != null && callStack.RemainingChildren.IsEmpty)
                callStack = callStack.Parent;

            return callStack;
        }

        private IConsList<BasePattern> splitQuantifier(QuantifierPattern quant)
        {
            var tail = SimpleConsList<BasePattern>.Empty;

            if (quant.MaxOccurrences != 1)
                tail = new SimpleConsList<BasePattern>(
                                quant.MaxOccurrences == null ?
                                    quant :
                                    new QuantifierPattern(quant.ChildPattern,
                                                          0,
                                                          quant.MaxOccurrences - 1,
                                                          quant.IsGreedy));

            return new SimpleConsList<BasePattern>(quant.ChildPattern, tail);
        }

        private bool doesAnchorMatch(ArrayConsList<char> consList, AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.StartOfString:
                    return consList.IsStartOfArray;

                case AnchorType.StartOfLine:
                    return consList.IsStartOfArray || consList.Prev == '\n';


                case AnchorType.EndOfString:
                    return consList.IsEmpty;

                case AnchorType.EndOfLine:
                    return consList.IsEmpty || consList.Head == '\n';

                case AnchorType.EndOfStringOrBeforeEndingNewline:
                    return consList.DropWhile(c => c == '\n').IsEmpty;


                //case AnchorType.ContiguousMatch:
                //    break;
                //case AnchorType.WordBoundary:
                //    break;
                //case AnchorType.NonWordBoundary:
                //    break;


                default:
                    throw new ApplicationException(
                        string.Format("BacktrackingMatcher: illegal anchor type ({0}).",
                                      anchorType.ToString()));
            }
        }

        private Result<char, int> parseChar(Result<char, int> partialResult, Func<char, bool> isMatch)
        {
            if (!partialResult.Rest.IsEmpty && isMatch(partialResult.Rest.Head))
                return new Result<char, int>(partialResult.Value + 1,
                                             partialResult.Rest.Tail);
            else
                return null;
        }
    }
}
