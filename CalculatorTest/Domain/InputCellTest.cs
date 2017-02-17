using System;
using Calculator.Domain;
using NUnit.Framework;


namespace CalculatorTest.Domain
{
    [TestFixture]
    public class InputCellTest
    {
        [Test]
        public void IsOperationShouldReturnTrue()
        {
            Assert.IsTrue(InputCell.Symbol("+").IsOperation());
            Assert.IsTrue(InputCell.Symbol("-").IsOperation());
            Assert.IsTrue(InputCell.Symbol("*").IsOperation());
            Assert.IsTrue(InputCell.Symbol("/").IsOperation());
            Assert.IsTrue(InputCell.Symbol("^").IsOperation());
        }

        [Test]
        public void IsOperationShouldReturnFalse()
        {
            Assert.IsFalse(InputCell.Symbol("(").IsOperation());
            Assert.IsFalse(InputCell.Symbol(")").IsOperation());
        }

        [Test]
        public void IsNumberShouldReturnTrue()
        {
            Assert.IsTrue(InputCell.Number(1).IsNumber());
        }

        [Test]
        public void IsNumberShouldReturnFalse()
        {
            Assert.IsFalse(InputCell.Symbol(")").IsNumber());
        }

        [Test]
        public void WeightShouldReturnCorrectWeigth()
        {
            Assert.AreEqual(0, InputCell.Number(1).Weight);
            Assert.AreEqual(1, InputCell.Symbol(")").Weight);
            Assert.AreEqual(1, InputCell.Symbol("(").Weight);
            Assert.AreEqual(2, InputCell.Symbol("+").Weight);
            Assert.AreEqual(2, InputCell.Symbol("-").Weight);
            Assert.AreEqual(3, InputCell.Symbol("*").Weight);
            Assert.AreEqual(3, InputCell.Symbol("/").Weight);
            Assert.AreEqual(4, InputCell.Symbol("^").Weight);
        }

        [Test]
        public void SymbolShouldTrowExceptionInputContainsInvalidSymbols()
        {
            Assert.Throws<ArgumentException>(() => InputCell.Symbol("$"));
            Assert.Throws<ArgumentException>(() => InputCell.Symbol("1"));
            Assert.Throws<ArgumentException>(() => InputCell.Symbol("a"));
        }
    }
}
