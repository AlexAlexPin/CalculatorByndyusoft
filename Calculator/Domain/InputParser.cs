using System.Collections.Generic;
using System.Linq;
using Calculator.Tools;


namespace Calculator.Domain
{
    public class InputParser
    {
        /// <summary>
        /// Parses the specified string to a sequence of cells 
        /// that may contain a number or symbols or arithmetic operation.
        /// </summary>
        public InputCell[] Parse(string input)
        {
            var result = new LinkedList<InputCell>();
            var numberBuffer = new NumberBuilder();

            foreach (char ch in input)
            {
                if (ch.IsEmpty()) continue;
                if (ch.IsDigit() || ch.IsDot())
                {
                    numberBuffer.Append(ch);
                }
                else
                {
                    SaveNumber(numberBuffer, result);

                    if (ch.IsMinus() && IsMinusForNumber(result))
                        numberBuffer.Append(ch);
                    else
                        result.AddLast(InputCell.Symbol(ch));
                }
            }
            SaveNumber(numberBuffer, result);
            return result.ToArray();
        }

        private static void SaveNumber(NumberBuilder buffer, LinkedList<InputCell> result)
        {
            if (buffer.IsEmpty()) return;
            var number = buffer.Build();
            result.AddLast(InputCell.Number(number));
            buffer.Clear();
        }

        private static bool IsMinusForNumber(IEnumerable<InputCell> input)
        {
            var lastCell = input.LastOrDefault();
            return lastCell == null || lastCell.IsOperation() || lastCell.IsOpenBracket();
        }
    }
}
