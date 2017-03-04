using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Calculator.Tools;


namespace Calculator.Domain
{
    /// <summary>
    /// Represents a part of an input string, that may contain a number
    /// or symbols or arithmetic operation.
    /// </summary>
    public class InputCell
    {
        /// <summary>
        /// A value of this cell.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// An expression that represents the value of this cell.
        /// </summary>
        public Func<Expression, Expression, Expression> Expr { get; }
        

        /// <summary>
        /// Creates an instance of the number cell.
        /// </summary>
        public static InputCell Number(double d)
        {
            return new InputCell(d, (e1, e2) => Expression.Constant(d));
        }

        /// <summary>
        /// Creates an instance of the symbol or arithmetic operation cell.
        /// </summary>
        public static InputCell Symbol(char ch)
        {
            return Symbol(ch.ToString());
        }

        /// <summary>
        /// Creates an instance of the symbol or arithmetic operation cell.
        /// </summary>
        public static InputCell Symbol(string str)
        {
            if (!Weights.ContainsKey(str))
                throw new ArgumentException($"Incorrect element {str}");

            return new InputCell(str, Operations.GetValueOrDefault(str));
        }


        private InputCell(object value, Func<Expression, Expression, Expression> expr)
        {
            Expr = expr;
            Value = value;
        }
        

        public bool IsOpenBracket()
        {
            return "(".Equals(Value.ToString(), StringComparison.Ordinal);
        }

        public bool IsCloseBracket()
        {
            return ")".Equals(Value.ToString(), StringComparison.Ordinal);
        }

        public bool IsOperation()
        {
            return Operations.ContainsKey(Value.ToString());
        }

        public bool IsNumber()
        {
            return Value is double;
        }

        public int Weight => Weights.GetValueOrDefault(Value.ToString());


        private static readonly Dictionary<string, int> Weights =
            new Dictionary<string, int>
            {
                [")"] = 1,
                ["("] = 1,
                ["+"] = 2,
                ["-"] = 2,
                ["*"] = 3,
                ["/"] = 3,
                ["^"] = 4
            };

        private static readonly Dictionary
            <string, Func<Expression, Expression, Expression>> Operations =
                new Dictionary<string, Func<Expression, Expression, Expression>>
                {
                    ["+"] = Expression.Add,
                    ["-"] = Expression.Subtract,
                    ["*"] = Expression.Multiply,
                    ["/"] = Expression.Divide,
                    ["^"] = Expression.Power
                };


        public override bool Equals(object obj)
        {
            return (obj as InputCell)?.Value?.Equals(Value) ?? false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}