using System.Collections.Generic;
using System.Globalization;
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
            var result = new List<InputCell>();
            var numberBuffer = new StringBuilder();

            foreach (char ch in input)
            {
                if (ch.IsDigit() || ch.IsDot())
                {
                    numberBuffer.Append(ch);
                }
                else if (ch.IsEmpty())
                {
                    SaveNumber(numberBuffer, result);
                }
                else
                {
                    SaveNumber(numberBuffer, result);
                    result.Add(InputCell.Symbol(ch.ToString()));
                }
            }
            SaveNumber(numberBuffer, result);
            return result.ToArray();
        }


        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        private static void SaveNumber(StringBuilder numberBuffer, List<InputCell> result)
        {
            if (numberBuffer.Length == 0)
                return;

            var number = double.Parse(numberBuffer.ToString(), NumberStyles.Number, Culture);
            result.Add(InputCell.Number(number));
            numberBuffer.Clear();
        }
    }
}
