namespace Computers.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        private ComputerManager computerManager;
        private Computer defaultComputer;
        private readonly string defaultManufacturer = "Toshiba";
        private readonly string defaultModel = "Satellite L50";
        private readonly decimal defaultPrice = 1000.50m;     
        [SetUp]
        public void Setup()
        {
            this.defaultComputer = new Computer(defaultManufacturer, defaultModel, defaultPrice);
            this.computerManager = new ComputerManager();
        }
        [Test]
        public void CheckTheComputerConstructorWorksCorrectly()
        {
            Assert.AreEqual(this.defaultManufacturer, this.defaultComputer.Manufacturer);
            Assert.AreEqual(this.defaultModel, this.defaultComputer.Model);
            Assert.AreEqual(this.defaultPrice, this.defaultComputer.Price);
        }
        [Test]
        public void CheckComputerManagerConstructorWorksCorrectly()
        {
            Assert.That(this.computerManager.Count, Is.EqualTo(0));
            Assert.That(this.computerManager.Computers, Is.Empty);
        }
        [Test]
        public void CheckIfCountWorksCorrectly()
        {
            this.computerManager.AddComputer(this.defaultComputer);
            Assert.That(this.computerManager.Count, Is.EqualTo(1));
            Assert.That(this.computerManager.Computers, Has.Member(this.defaultComputer));
        }
        [Test]
        public void AddComputerThrowExceptionWithExistingComputer()
        {
            this.computerManager.AddComputer(this.defaultComputer);

            Assert.Throws<ArgumentException>(() =>
                {
                    this.computerManager.AddComputer(this.defaultComputer);
                }, "This computer alredy exists."
                );
        }
        [Test]
        public void AddComputerWithDifferentModelShouldIncreaseCountWhenSuccessful()
        {
            this.computerManager.AddComputer(this.defaultComputer);
            string defaultManufacturer = "Toshiba";
            string defaultModel = "Satellite M600";
            decimal defaultPrice = 1000.50m;
            Computer testComputer = new Computer(defaultManufacturer, defaultModel, defaultPrice);
            this.computerManager.AddComputer(testComputer);
            Assert.That(this.computerManager.Count, Is.EqualTo(2));
            Assert.That(this.computerManager.Computers, Has.Member(this.defaultComputer));
            Assert.That(this.computerManager.Computers, Has.Member(testComputer));
        }
        [Test]
        public void AddShouldIncreaseCountWhenSuccessful()
        {
            this.computerManager.AddComputer(this.defaultComputer);

            Assert.That(this.computerManager.Count, Is.EqualTo(1));
            Assert.That(this.computerManager.Computers, Has.Member(this.defaultComputer));
        }
        [Test]
        public void AddShouldThrowExceptionWithNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.computerManager.AddComputer(null);
            }, "Can not be null."
                );
        }
        [Test]
        public void RemoveComputerShouldRemoveCorrectComputer()
        {
            this.computerManager.AddComputer(this.defaultComputer);
            var result = this.computerManager.RemoveComputer(this.defaultManufacturer, this.defaultModel);
            Assert.AreSame(this.defaultComputer,result);
            Assert.That(this.computerManager.Count, Is.EqualTo(0));
        }
        [Test]
        public void RemoveShouldThrowExceptionWithInvalidComputer()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.computerManager.RemoveComputer(this.defaultManufacturer, "Sony");
            }, "There is no computer with this manufacturer and model.");
        }
        [Test]
        public void GetComputer()
        {
            this.computerManager.AddComputer(this.defaultComputer);
            var result = this.computerManager.GetComputer(this.defaultManufacturer, this.defaultModel);

            Assert.AreSame(this.defaultComputer, result);
        }
        [Test]
        public void GetComputerShouldThrowExceptionWithNullManufacturer()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.computerManager.GetComputer(null, this.defaultModel);
            }
               );
        }
        [Test]
        public void GetComputerShouldThrowExceptionWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.computerManager.GetComputer(this.defaultManufacturer, null);
            }
               );
        }     
        [Test]
        public void GetComputerShouldThrowExceptionWithInvalidData()
        {
            this.computerManager.AddComputer(this.defaultComputer);

            Assert.Throws<ArgumentException>(() =>
            {
                this.computerManager.GetComputer("Acer", "N50");
            }, "There is no computer with this manufacturer and model."
                );
        }
        [Test]
        public void GetAllByManufacturer()
        {
            this.computerManager.AddComputer(this.defaultComputer);
            this.computerManager.AddComputer(new Computer(this.defaultManufacturer, "T200", 500m));
            var collection = this.computerManager.GetComputersByManufacturer(this.defaultManufacturer);
            Assert.That(collection.Count, Is.EqualTo(2));
        }
        [Test]
        public void GetByManufacturerShouldThrowExceptionWithNullManufacturer()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.computerManager.GetComputersByManufacturer(null);
            });
        }
        [Test]
        public void GetAllByManufacturerShouldReturnZeroWithNoMatches()
        {
            var collection = this.computerManager.GetComputersByManufacturer("Lenovo");
            Assert.That(collection.Count, Is.EqualTo(0));
        }
    }
}