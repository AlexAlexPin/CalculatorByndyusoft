using System.Globalization;
using System.Text;


namespace Calculator.Domain
{
    /// <summary>
    /// Encapsulates a logic of building a number from symbols.
    /// </summary>
    public class NumberBuilder
    {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        private readonly StringBuilder _builder = new StringBuilder();
        
        /// <summary>
        /// Returns the builded number.
        /// </summary>
        public double Build()
        {
            return double.Parse(_builder.ToString(), NumberStyles.Number, Culture);
        }

        public NumberBuilder Append(char ch)
        {
            _builder.Append(ch);
            return this;
        }

        public bool IsEmpty()
        {
            return _builder.Length == 0;
        }

        public void Clear()
        {
            _builder.Clear();
        }
    }
}
