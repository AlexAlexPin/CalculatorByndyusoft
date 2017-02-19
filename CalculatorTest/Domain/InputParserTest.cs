using Calculator.Domain;
using NUnit.Framework;


namespace CalculatorTest.Domain
{
    [TestFixture]
    public class InputParserTest
    {
        [Test]
        public void ParseShouldReturnCorrectResult()
        {
            InputCell[] result = new InputParser()
                .Parse("3 + 4 * 2 / (1 - 5)^2");

            Assert.AreEqual(3,   result[0].Value);
            Assert.AreEqual("+", result[1].Value);
            Assert.AreEqual(4,   result[2].Value);
            Assert.AreEqual("*", result[3].Value);
            Assert.AreEqual(2,   result[4].Value);
            Assert.AreEqual("/", result[5].Value);
            Assert.AreEqual("(", result[6].Value);
            Assert.AreEqual(1,   result[7].Value);
            Assert.AreEqual("-", result[8].Value);
            Assert.AreEqual(5,   result[9].Value);
            Assert.AreEqual(")", result[10].Value);
            Assert.AreEqual("^", result[11].Value);
            Assert.AreEqual(2,   result[12].Value);
        }

        [Test]
        public void ParseShouldWorkWithFloatingPoint()
        {
            InputCell[] result = new InputParser().Parse("3.4 + 4");

            Assert.AreEqual(3.4, result[0].Value);
            Assert.AreEqual("+", result[1].Value);
            Assert.AreEqual(4,   result[2].Value);
        }

        [Test]
        public void ParseShouldWorkWithNegativeNumbers()
        {
            InputCell[] result = new InputParser().Parse("1 - (-2)");

            Assert.AreEqual(1,   result[0].Value);
            Assert.AreEqual("-", result[1].Value);
            Assert.AreEqual("(", result[2].Value);
            Assert.AreEqual(-2,  result[3].Value);
            Assert.AreEqual(")", result[4].Value);
        }

        [Test]
        public void ParseShouldDetectNegativeNumberInFirstCell()
        {
            InputCell[] result = new InputParser().Parse(" - 1 + 2 ");

            Assert.AreEqual(-1,  result[0].Value);
            Assert.AreEqual("+", result[1].Value);
            Assert.AreEqual(2,   result[2].Value);
        }
    }
}
