using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;
using NUnit.Framework;

namespace GameOfLife.nUnitTests
{
    public class RectangularMapTests
    {
        private Vector2D _vector2D { get; set; }

        [SetUp]
        public void Setup()
        {
            Place_ObjectAt_Test();
            Place_IsOccupied_Test();
        }

        [Test]
        public void Place_ObjectAt_Test()
        {
            //Assign
            var map = new GrassField(10, 10,0);

            Animal a1 = new Animal(map, new Vector2D(2, 2));
            Animal a2 = new Animal(map, new Vector2D(2, 3));
            Animal a3 = new Animal(map, new Vector2D(3, 2));
            Animal a4 = new Animal(map, new Vector2D(3, 3));
            Animal a5 = new Animal(map, new Vector2D(3, 3));

            //Act
            map.place(a1);
            map.place(a2);

            var obj1 = map.objectAt(new Vector2D(2, 2));
            var obj2 = map.objectAt(new Vector2D(2, 3));

            //Assert
            Assert.AreEqual(obj1, a1);
            Assert.AreEqual(obj2, a2);
        }

        [Test]
        public void Place_IsOccupied_Test()
        {
            //Assign
            var map = new RectangularMap(5, 5);

            var a1 = new Animal(map, new Vector2D(2, 2));
            var a2 = new Animal(map, new Vector2D(2, 3));
            var a3 = new Animal(map, new Vector2D(3, 2));
            var a4 = new Animal(map, new Vector2D(3, 3));
            var a5 = new Animal(map, new Vector2D(3, 3));

            //Act
            map.place(a1);
            map.place(a2);
            map.place(a3);
            map.place(a4);

            var occupied1 = map.isOccupied(new Vector2D(2, 2));
            var occupied4 = map.isOccupied(new Vector2D(3, 3));
            var unoccupied = map.isOccupied(new Vector2D(1, 1));

            //Assert
            Assert.IsTrue(occupied1);
            Assert.IsTrue(occupied4);
            Assert.IsFalse(unoccupied);
        }

    }
}