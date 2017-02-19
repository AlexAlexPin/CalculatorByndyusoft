using System;


namespace Calculator.Domain
{
    public class InputChecker
    {
        /// <summary>
        /// Checks the input cell sequence according the rules
        /// of mathematical expressions and throws an exception
        /// if it is incorrect.
        /// </summary>
        public void Check(InputCell[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (!input[i].IsOperation()) continue;

                if (OperationIsRepeated(input, i))
                    throw new ArgumentException(
                        $"Operation {input[i].Value} is repeated on {i}");

                if (OperationIsLastCell(input, i))
                    throw new ArgumentException(
                        "Operation cannot be in the last cell");
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