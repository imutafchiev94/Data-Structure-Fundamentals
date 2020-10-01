using System.Collections;

namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var stack = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                if (stack.Count < 1)
                {
                    if (parentheses[i] == '(' || parentheses[i] == '[' || parentheses[i] == '{' 
                    || parentheses[i] == ')' || parentheses[i] == ']' || parentheses[i] == '}')
                    {
                        stack.Push(parentheses[i]);
                    }
                }
                else
                {
                    switch (parentheses[i])
                    {
                        case ')':
                        {
                            if (stack.Peek() == '(')
                            {
                                stack.Pop();
                            }
                            break;
                        }
                        case ']':
                        {
                            if (stack.Peek() == '[')
                            {
                                stack.Pop();
                            }
                            break;
                        }
                        case '}':
                        {
                            if (stack.Peek() == '{')
                            {
                                stack.Pop();
                            }
                            break;
                        }
                        case '(':
                        {
                            stack.Push(parentheses[i]);
                                break;
                        }
                        case '[':
                        {
                            stack.Push(parentheses[i]);
                                break;
                        }
                        case '{':
                        {
                            stack.Push(parentheses[i]);
                                break;
                        }
                    }
                }
            }

            if (stack.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
