namespace Calculator.Domain
{
    public class RpnCalculator
    {
        private readonly InputParser _parser;

        private readonly RpnConverter _converter;

        private readonly RpnCounter _counter;


        public RpnCalculator(InputParser parser, RpnConverter converter, RpnCounter counter)
        {
            _parser = parser;
            _converter = converter;
            _counter = counter;
        }


        public double Calculate(string input)
        {
            InputCell[] parsedInput = _parser.Parse(input);
            InputCell[] reversePolishNotation = _converter.Convert(parsedInput);
            double result = _counter.Count(reversePolishNotation);
            return result;
        }
    }
}
