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

            foreach (var note in input)
            {
                if (note.IsNumber())
                {
                    stack.Push(note);
                }
                else if (note.IsOperation())
                {
                    var operandRight = GetOperand(stack);
                    var operandLeft  = GetOperand(stack);
                    var operation    = note.Expr(operandLeft, operandRight);

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