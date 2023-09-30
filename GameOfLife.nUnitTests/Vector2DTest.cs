using GameOfLife.Model.Model;
using NUnit.Framework;

namespace GameOfLife.nUnitTests
{
    public class Tests
    {
        private Vector2D _vector2D { get; set; }

        [SetUp]
        public void Setup()
        {
            NextDirection_EqualTest();
            PreviousDirection_EqualTest();
            ToString_EqualTest();
        }

        [Test]
        public void NextDirection_EqualTest()
        {
            //Assign
            var north = new MapDirection(Direction.NORTH);
            var east = new MapDirection(Direction.EAST);
            var south = new MapDirection(Direction.SOUTH);
            var west = new MapDirection(Direction.WEST);

            //Act
            var northNext = north.next();
            var eastNext = east.next();
            var southNext = south.next();
            var westNext = west.next();

            //Assert
            Assert.AreEqual(Direction.EAST, northNext);
            Assert.AreEqual(Direction.SOUTH, eastNext);
            Assert.AreEqual(Direction.WEST, southNext);
            Assert.AreEqual(Direction.NORTH, westNext);
        }

        [Test]
        public void PreviousDirection_EqualTest()
        {
            var north = new MapDirection(Direction.NORTH);
            var east = new MapDirection(Direction.EAST);
            var south = new MapDirection(Direction.SOUTH);
            var west = new MapDirection(Direction.WEST);

            //Act
            var northPrev = north.previous();
            var eastPrev = east.previous();
            var southPrev = south.previous();
            var westPrev = west.previous();

            //Assert
            Assert.AreEqual(Direction.WEST, northPrev);
            Assert.AreEqual(Direction.NORTH, eastPrev);
            Assert.AreEqual(Direction.EAST, southPrev);
            Assert.AreEqual(Direction.SOUTH, westPrev);
        }

        [Test]
        public void ToString_EqualTest()
        {
            //Assign
            var north = new MapDirection(Direction.NORTH);
            var east = new MapDirection(Direction.EAST);
            var south = new MapDirection(Direction.SOUTH);
            var west = new MapDirection(Direction.WEST);



            //Act
            var northStr = north.ToString();
            var eastStr = east.ToString();
            var southStr = south.ToString(); 
            var westStr = west.ToString();

            //Assert
            Assert.AreEqual("N", northStr);
            Assert.AreEqual("S", southStr);
            Assert.AreEqual("E", eastStr);
            Assert.AreEqual("W", westStr);
        }
    }
}