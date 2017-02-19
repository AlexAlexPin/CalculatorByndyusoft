using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Calculator.Tools;


namespace Calculator.Domain
{
    public class RpnCounter
    {
        /// <summary>
        /// Counts a result of expression that is represented as a sequence of input cells 
        /// ordered according the order of the Reverse Polish notation (RPN). 
        /// </summary>
        public double Count(InputCell[] input)
        {
            var stack = new Stack<InputCell>();

            foreach (var cell in input)
            {
                if (cell.IsNumber())
                {
                    stack.Push(cell);
                }
                else if (cell.IsOperation())
                {
                    var operandRight = GetOperand(stack);
                    var operandLeft  = GetOperand(stack);
                    var operation    = cell.Expr(operandLeft, operandRight);

                    var result = InvokeExpession(operation);

                    stack.Push(InputCell.Number(result));
                }
            }
            return (double)stack.Peek().Value;
        }
        
        private static Expression GetOperand(Stack<InputCell> stack)
        {
            return stack.PopOrValue(InputCell.Number(0)).Expr.Invoke(null, null);
        }

        private static double InvokeExpession(Expression expression)
        {
            var lambda = Expression.Lambda<Func<double>>(expression);
            return lambda.Compile().Invoke();
        }
    }
}