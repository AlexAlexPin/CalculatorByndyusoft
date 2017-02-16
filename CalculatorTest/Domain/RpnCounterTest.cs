using Calculator.Domain;
using NUnit.Framework;


namespace CalculatorTest.Domain
{
    [TestFixture]
    public class RpnCounterTest
    {
        [Test]
        public void CountShouldReturnCorrectResult()
        {
            // 3 + 4 * 2 / (1 - 5)^2 = 3.5
            // 3 4 2 * 1 5 - 2 ^ / +
            var input = new[]
            {
                InputCell.Number(3),
                InputCell.Number(4),
                InputCell.Number(2),
                InputCell.Symbol("*"),
                InputCell.Number(1),
                InputCell.Number(5),
                InputCell.Symbol("-"),
                InputCell.Number(2),
                InputCell.Symbol("^"),
                InputCell.Symbol("/"),
                InputCell.Symbol("+")
            };

            var result = new RpnCounter().Count(input);

            Assert.AreEqual(3.5, result);
        }
    }
}
