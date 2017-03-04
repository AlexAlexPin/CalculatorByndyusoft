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
                    result.AddIfNotNull(CreateCell(numberBuffer));

                    if (ch.IsMinus() && IsMinusForNumber(result))
                    {
                        numberBuffer.Append(ch);
                    }
                    else
                    {
                        result.AddLast(InputCell.Symbol(ch));
                    }
                }
            }
            result.AddIfNotNull(CreateCell(numberBuffer));
            return result.ToArray();
        }
        
        private static bool IsMinusForNumber(IEnumerable<InputCell> cells)
        {
            InputCell lastCell = cells.LastOrDefault();
            return lastCell == null || lastCell.IsOperation() || lastCell.IsOpenBracket();
        }

        public static InputCell CreateCell(NumberBuilder builder)
        {
            if (builder.IsEmpty()) return null;
            double number = builder.Build();
            builder.Clear();
            return InputCell.Number(number);
        }
    }
}
