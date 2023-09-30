using NUnit.Framework;
using GameOfLife.Model.Model;
using GameOfLife.Model;
using GameOfLife.Model.Services;
using System.Collections;
using System.Security.Cryptography;
using GameOfLife.Services;

namespace GameOfLife.IntegrationTests
{
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {   
            CheckTwoAnimalsBehaviourEngine();
            CheckTwoAnimalsBehaviourWithColisionEngine();
        }

        [Test]
        public void CheckTwoAnimalsBehaviourEngine()
        {
            //Assign
            var map = new GrassField(10, 10,0);
            Vector2D[] positions = new[] {  new Vector2D(5, 5), new Vector2D(2, 2) };
            var directions = OptionsParser.parse("r,f,l,l,f,b,r,f,f,r,f,f,f");
            Hashtable animals;
            var engine = new SimulationEngine(directions, map, positions);

            //Act
            engine.run();

            //Assert
            animals = map.getAnimals();
            var keys = animals.Keys;

            var destinationPositions = new[] { new Vector2D(5,3), new Vector2D(5,7) };
            var counter = 0;
            foreach(var key in keys)
            {
                var animal = (Animal)animals[key];
                Assert.IsTrue(animal.isAt(destinationPositions[counter]));
                counter++;
            }
        }

        [Test]
        public void CheckTwoAnimalsBehaviourWithColisionEngine()
        {
            //Assign
            var map = new GrassField(10, 10, 0);
            Vector2D[] positions = new[] { new Vector2D(4, 4), new Vector2D(5, 5) };
            var directions = OptionsParser.parse("l,f,r,r,r,f,f,f,f,r,f,f,f,f");
            Hashtable animals;
            var engine = new SimulationEngine(directions, map, positions);

            //Act
            engine.run();

            //Assert
            animals = map.getAnimals();
            var keys = animals.Keys;

            var destinationPositions = new[] { new Vector2D(9, 5),new Vector2D(5,3) };
            var counter = 0;
            foreach (var key in keys)
            {
                var animal = (Animal)animals[key];
                Assert.IsTrue(animal.isAt(destinationPositions[counter]));
                counter++;
            }
        }
    }
}