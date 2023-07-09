namespace Computers.Tests
{
    using System;
    using NUnit.Framework;  
    class ComputersTest
    {
        private Computer defaultComputer;
        private readonly string defaultManufacturer = "Toshiba";
        private readonly string defaultModel = "Satellite L50";
        private readonly decimal defaultPrice = 1000.50m;
        [SetUp]
        public void Setup()
        {
            this.defaultComputer = new Computer(defaultManufacturer, defaultModel, defaultPrice);
        }
        [Test]
        public void CheckTheComputerConstructorWorksCorrectly()
        {
            Assert.AreEqual(this.defaultManufacturer, this.defaultComputer.Manufacturer);
            Assert.AreEqual(this.defaultModel, this.defaultComputer.Model);
            Assert.AreEqual(this.defaultPrice, this.defaultComputer.Price);
        }

    }
}
