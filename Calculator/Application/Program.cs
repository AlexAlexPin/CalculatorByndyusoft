using System;
using Calculator.Domain;


namespace Calculator.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser     = new InputParser();
            var converter  = new RpnConverter();
            var counter    = new RpnCounter();
            var calculator = new RpnCalculator(parser, converter, counter);

            Console.WriteLine("CONSOLE CALCULATOR");
            Console.WriteLine("You can enter numbers and symbols + - * / ^ ( ).");
            Console.WriteLine("Press enter key to calculate.");
            Console.WriteLine("Enter 'q' key to exit.");

            while (true)
            {
                Console.Write("Enter expression > ");

                string input = Console.ReadLine();
                if ("q".Equals(input, StringComparison.OrdinalIgnoreCase))
                    break;

                try
                {
                    double result = calculator.Calculate(input);
                    Console.WriteLine($"Result is {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input");
                }
            }
        }
    }
}

