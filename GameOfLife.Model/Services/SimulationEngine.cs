using GameOfLife.Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public class SimulationEngine : IEngine
    {
        private readonly MoveDirection[] _directions;
        private readonly AbstractWorldMap _map;
        private readonly Vector2D[] _positions;

        public SimulationEngine(
            MoveDirection[] directions,
            AbstractWorldMap map,
            Vector2D[] positions) 
        {
            this._directions = directions;
            this._map = map;
            this._positions = positions;

            foreach(var position in positions)
            {
                this._map.place(new Animal(this._map, position));
            }
        }

        public void run()
        {
            var moves = this._directions.Length / this._positions.Length;
            var map = this._map;

            var animalKeys = this.getAnimals();
            var animals = this._map.getAnimals();

            var currentAnimalCounter = 0;
            var directionsCounter = 0;

            for(int i = 0; i < this._directions.Length; i++)
            {
                if (i != 0 && i % animals.Count == 0)
                {
                    animalKeys = this.getAnimals();
                    animals = this._map.getAnimals();
                    currentAnimalCounter = 0;
                }

                var key = animalKeys[currentAnimalCounter];
                var animal = (Animal)animals[key];

                System.Threading.Thread.Sleep(500);

                //animal.move(this._directions[i]);
                currentAnimalCounter++;
            }
        }

        private List<int> getAnimals()
        {
            Hashtable animals = this._map.getAnimals();

            List<int> animalKeys = new List<int>();

            foreach (System.Collections.DictionaryEntry an in animals)
            {
                var key = an.Key;
                animalKeys.Add(Convert.ToInt32(an.Key.GetHashCode()));
            }

            return animalKeys;
        }
    }
}
