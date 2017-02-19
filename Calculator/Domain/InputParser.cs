using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
            var numberBuffer = new StringBuilder();
            var numberIsNegative = false;

            foreach (char ch in input)
            {
                if (ch.IsDigit() || ch.IsDot() || ch.IsMinus() && numberIsNegative)
                {
                    numberBuffer.Append(ch);
                }
                else if (!ch.IsEmpty())
                {
                    SaveNumber(numberBuffer, result);
                    result.AddLast(InputCell.Symbol(ch.ToString()));
                }
                if (ch.IsMinus()) // register expression like this 1-(-1)
                {
                    numberIsNegative = !numberIsNegative;
                }
            }
            SaveNumber(numberBuffer, result);
            return result.ToArray();
        }


        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        private static void SaveNumber(StringBuilder numberBuffer, LinkedList<InputCell> result)
        {
            if (numberBuffer.Length == 0)
                return;

            var number = double.Parse(numberBuffer.ToString(), NumberStyles.Number, Culture);
            result.AddLast(InputCell.Number(number));
            numberBuffer.Clear();
        }
    }
}
