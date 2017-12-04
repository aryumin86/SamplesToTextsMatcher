using SamplesToTextsMatcher.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SamplesToTextsMatcher
{
    public class ConcretePatternParser : AbstractPatternParser
    {
        public override LinkedList<Entities.Expression> ParsePattern(string pattern)
        {
            LinkedList<Entities.Expression> ExpressionsList = new LinkedList<Entities.Expression>();

            char[] charArr = pattern.ToCharArray();

            for (int i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == ' ')
                    continue;
                else if (charArr[i] == '(')
                {
                    ExpressionsList.AddLast(new OpeningBracket()
                    {
                        StartIndexAtRaw = i,
                        EndIndexAtRaw = i
                    });

                    continue;
                }
                else if (charArr[i] == ')')
                {
                    ExpressionsList.AddLast(new ClosingBracket()
                    {
                        StartIndexAtRaw = i,
                        EndIndexAtRaw = i
                    });

                    continue;
                }
                else if (charArr[i] == '"')
                {
                    TerminalExpression term = getTerminalExpression(charArr, i);
                    ExpressionsList.AddLast(term);
                    i = term.EndIndexAtRaw;

                    continue;
                }
                else if (charArr[i] == '*')
                {
                    if (ExpressionsList.Last() == null || ExpressionsList.Last().GetType() != typeof(TerminalExpression))
                        throw new FormatException("Asterix should be only at the end of term");
                    else
                        ((TerminalExpression)ExpressionsList.Last()).HasAsterixSign = true;

                    continue;
                }
                else if (charArr[i] == '=')
                {
                    ExpressionsList.AddLast(new EqualsSign());
                    continue;
                }

                NonTerminalExpression exp = getNonTerminal(charArr, i);
                if (exp == null)
                {
                    TerminalExpression term = getTerminalExpression(charArr, i);
                    ExpressionsList.AddLast(term);
                    i = term.EndIndexAtRaw;
                    continue;
                }
                ExpressionsList.AddLast(exp);
                i = exp.EndIndexAtRaw;
            }

            return ExpressionsList;
        }

        /// <summary>
        /// If it is a start of NonTerminal expression this method will return the
        /// NonTerminal Expression.
        /// </summary>
        /// <returns>The non terminal.</returns>
        /// <param name="arr">Arr.</param>
        /// <param name="startPosition">Start position.</param>
        private NonTerminalExpression getNonTerminal(char[] arr, int startPosition)
        {
            NonTerminalExpression result = null;

            switch (arr[startPosition])
            {
                case '&':
                    result = new ANDExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '|':
                    result = new ORExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '~':
                    result = new NOTExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '/':
                    int distanseCharLength = 0;
                    while (char.IsDigit(arr[startPosition + distanseCharLength + 1]))
                    {
                        distanseCharLength++;
                    }

                    if (distanseCharLength == 0)
                        throw new FormatException("/n non-terminal format error - no n after /");

                    result = new MaxDistExpression(int.Parse(new string(arr.Skip(startPosition + 1).Take(distanseCharLength).ToArray())))
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition + distanseCharLength
                    };
                    break;
            }

            return result;
        }

        /// <summary>
        /// Create terminal expression starting from startIndex.
        /// </summary>
        /// <returns>The terminal expression.</returns>
        /// <param name="charArr">Char arr.</param>
        /// <param name="startIndex">Start index.</param>
        private TerminalExpression getTerminalExpression(char[] charArr, int startIndex)
        {
            TerminalExpression term = null;
            int closingQuotesIndex = 0;
            if (charArr[startIndex] == '"')
            {
                closingQuotesIndex = charArr
                    .Skip(startIndex + 1)
                    .Select((x, y) => new { ch = x, index = y })
                    .First(x => x.ch == '"')
                    .index + 1 + startIndex;

                term = new TerminalExpression(
                    "\"" + new string(charArr.Skip(startIndex + 1).Take(closingQuotesIndex - startIndex - 1).ToArray()) + "\"")
                {
                    StartIndexAtRaw = startIndex,
                    EndIndexAtRaw = closingQuotesIndex,
                    InQuotes = true
                };
            }
            else
            {
                int i = startIndex;
                while (true)
                {
                    if (charArr.Length == i || charArr[i] == ' ' || charArr[i] == ')' || charArr[i] == '(' || charArr[i] == '|' || charArr[i] == '*' || charArr[i] == '&' || charArr[i] == '~' || charArr[i] == '/')
                    {
                        term = new TerminalExpression(new string(charArr.Skip(startIndex).Take(i - startIndex).ToArray()))
                        {
                            StartIndexAtRaw = startIndex,
                            EndIndexAtRaw = i - 1,
                            InQuotes = false
                        };
                        break;
                    }
                    i++;
                }
            }

            return term;
        }
    }
}
