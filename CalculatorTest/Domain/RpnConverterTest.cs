using System;
using Calculator.Domain;
using NUnit.Framework;


namespace CalculatorTest.Domain
{
    [TestFixture]
    public class RpnConverterTest
    {
        [Test]
        public void ConvertShouldConvertToReversePolishNotation()
        {
            var input = new[] // 3 + 4 * 2 / (1 - 5)^2
            {
                InputCell.Number(3),
                InputCell.Symbol("+"),
                InputCell.Number(4),
                InputCell.Symbol("*"),
                InputCell.Number(2),
                InputCell.Symbol("/"),
                InputCell.Symbol("("),
                InputCell.Number(1),
                InputCell.Symbol("-"),
                InputCell.Number(5),
                InputCell.Symbol(")"),
                InputCell.Symbol("^"),
                InputCell.Number(2),
            };
            
            InputCell[] result = new RpnConverter().Convert(input);

            // 3 4 2 * 1 5 - 2 ^ / +

            Assert.AreEqual(3,   result[0].Value);
            Assert.AreEqual(4,   result[1].Value);
            Assert.AreEqual(2,   result[2].Value);
            Assert.AreEqual("*", result[3].Value);
            Assert.AreEqual(1,   result[4].Value);
            Assert.AreEqual(5,   result[5].Value);
            Assert.AreEqual("-", result[6].Value);
            Assert.AreEqual(2,   result[7].Value);
            Assert.AreEqual("^", result[8].Value);
            Assert.AreEqual("/", result[9].Value);
            Assert.AreEqual("+", result[10].Value);
        }

        [Test]
        public void ConvertShouldThrowExceptionIfThereAreTwoOpeartionsInRow()
        {
            var input = new[]
            {
                InputCell.Number(2),
                InputCell.Symbol("+"),
                InputCell.Symbol("+"),
                InputCell.Number(2),
            };

            Assert.Throws<ArgumentException>(() =>
                new RpnConverter().Convert(input));
        }

        [Test]
        public void ConvertShouldThrowExceptionIfThereIsOpeartionsInLastCell()
        {
            var input = new[]
            {
                InputCell.Number(2),
                InputCell.Symbol("+"),
                InputCell.Number(2),
                InputCell.Symbol("+")
            };

            Assert.Throws<ArgumentException>(() =>
                new RpnConverter().Convert(input));
        }
    }
}
