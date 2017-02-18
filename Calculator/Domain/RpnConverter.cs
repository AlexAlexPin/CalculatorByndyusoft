using System;
using System.Collections.Generic;
using System.Linq;


namespace Calculator.Domain
{
    public class RpnConverter
    {
        /// <summary>
        /// Allocates items of the specified sequence according the order
        /// of the Reverse Polish notation (RPN).
        /// </summary>
        public InputCell[] Convert(InputCell[] input)
        {
            // Shunting-yard algorithm
            // https://en.wikipedia.org/wiki/Shunting-yard_algorithm

            var stack  = new Stack<InputCell>();
            var output = new LinkedList<InputCell>();

            for (var i = 0; i < input.Length; i++)
            {
                var item = input[i];
                if (item.IsNumber())
                {
                    output.AddLast(item);
                }
                else if (item.IsOpenBracket())
                {
                    stack.Push(item);
                }
                else if (item.IsCloseBracket())
                {
                    PurgeStackUpToCloseBracket(stack, output);
                }
                else if (item.IsOperation())
                {
                    if (OperationIsRepeated(input, i))
                        throw new ArgumentException(
                            $"Operation {item.Value} is repeated on {i}");

                    if (OperationIsLastCell(input, i))
                        throw new ArgumentException(
                            "Operation cannot be in the last cell");

                    PurgeTopStack(stack, output, item);
                }
            }
            PurgeStack(stack, output);

            return output.ToArray();
        }
        

        private static void PurgeStackUpToCloseBracket(
            Stack<InputCell> stack, LinkedList<InputCell> output)
        {
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                if (top.IsOpenBracket()) break;
                output.AddLast(top);
            }
        }

        private static void PurgeTopStack(Stack<InputCell> stack, 
            LinkedList<InputCell> output, InputCell element)
        {
            while (stack.Count > 0 && element.Weight <= stack.Peek().Weight)
            {
                output.AddLast(stack.Pop());
            }
            stack.Push(element);
        }
        
        private static void PurgeStack(
            Stack<InputCell> stack, LinkedList<InputCell> output)
        {
            while (stack.Count != 0)
            {
                output.AddLast(stack.Pop());
            }
        }


        private static bool OperationIsRepeated(InputCell[] input, int i)
        {
            if (i <= 0 || i >= input.Length)
                return false;

            return input[i].Equals(input[i - 1]);
        }

        private static bool OperationIsLastCell(InputCell[] input, int i)
        {
            return i == input.Length - 1;
        }
    }
}