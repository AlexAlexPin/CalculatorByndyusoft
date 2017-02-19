using System;
using Calculator.Domain;
using NUnit.Framework;


namespace CalculatorTest.Domain
{
    [TestFixture]
    public class InputCheckerTest
    {
        [Test]
        public void CheckShouldThrowExceptionIfThereAreTwoOpeartionsInRow()
        {
            var input = new[]
            {
                InputCell.Number(2),
                InputCell.Symbol("+"),
                InputCell.Symbol("+"),
                InputCell.Number(2)
            };

            Assert.Throws<ArgumentException>(() =>
                new InputChecker().Check(input));
        }

        [Test]
        public void CheckShouldThrowExceptionIfThereIsOpeartionsInLastCell()
        {
            var input = new[]
            {
                InputCell.Number(2),
                InputCell.Symbol("+"),
                InputCell.Number(2),
                InputCell.Symbol("+")
            };

            Assert.Throws<ArgumentException>(() =>
                new InputChecker().Check(input));
        }
    }
}
