using FoxAndChickens.Model;
using NUnit.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class ChickenAndfoxGameTest
    {
        private Game _sut;

        [SetUp]
        public void Setup()
        {

            var fox = new Item { Name = "Fox" };
            var chicken = new Item { Name = "Chick" };
            var grain = new Item { Name = "Grain" };

            fox.Food = chicken;
            chicken.Food = grain;

            _sut = new Game();

            _sut.LeftItems = new List<Item>
            {
                fox,
                chicken,
                grain
            };

        }

        [Test]
        [Repeat(25)]
        public void ShouldCrossItemSafley()
        {
            _sut.LeftItems.Shuffle();
            _sut.Cross();

            var isSafe = _sut.LeftItems.All(i => _sut.LeftItems.All(x => x.Food == null || x.Food.Name != x.Name));

            Assert.That(isSafe, Is.EqualTo(true));
        }

        [Test]
        [Repeat(25)]
        public void ShouldReturnItemSafley()
        {
            _sut.LeftItems.Shuffle();

            _sut.Cross();
            _sut.GoBack();

            var isSafe = _sut.CrossedItems.All(i => _sut.CrossedItems.All(x => x.Food == null || x.Food.Name != x.Name));

            Assert.That(isSafe, Is.EqualTo(true));
        }

        [Test]
        public void ShoudNotCrosswhenTwoCheckens()
        {
            var chicken2 = new Item { Name = "Chick2" };
            chicken2.Food = _sut.LeftItems.FirstOrDefault(x => x.Name == "Grain");
            _sut.LeftItems.Add(chicken2);

            _sut.Play();

            Assert.That(_sut.LeftItems.Any, Is.True);
        }



    }
}
