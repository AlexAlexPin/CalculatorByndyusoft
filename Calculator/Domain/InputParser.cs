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
            var cellBuffer = new LinkedList<InputCell>();
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
                    SaveNumber(numberBuffer, cellBuffer);

                    if (ch.IsMinus() && IsMinusForNumber(cellBuffer))
                        numberBuffer.Append(ch);
                    else
                        cellBuffer.AddLast(InputCell.Symbol(ch));
                }
            }
            SaveNumber(numberBuffer, cellBuffer);
            return cellBuffer.ToArray();
        }

        private static void SaveNumber(NumberBuilder numberBuffer, LinkedList<InputCell> cellBuffer)
        {
            if (numberBuffer.IsEmpty()) return;
            var number = numberBuffer.Build();
            cellBuffer.AddLast(InputCell.Number(number));
            numberBuffer.Clear();
        }

        private static bool IsMinusForNumber(IEnumerable<InputCell> input)
        {
            var lastCell = input.LastOrDefault();
            return lastCell == null || lastCell.IsOperation() || lastCell.IsOpenBracket();
        }
    }
}
