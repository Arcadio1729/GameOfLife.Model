using GameOfLife.Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public abstract class AbstractWorldMap : IWorldMap, IPositionChangeObserver
    {
        public int WIDTH;
        public int HEIGHT;

        public int START_ENERGY = 10;
        public int GRASS_ENERGY = 5;
        public int LOST_ENERGY = 1;
        public int GRASSES_PER_DAY = 2;
        public double ENERGY_TO_REPRODUCE = 0.1;

        public int currentDay = 0;

        public int GrassesAmount { get; private set; }
        public int AnimalsAmount { get; private set; }
        protected Hashtable Animals { get; set; }
        protected Hashtable Grasses { get; set; }

        public virtual bool canMoveTo(Vector2D position)
        {
            if (this.isOccupied(position))
                return false;

            return !(this.edgeReached(position));
        }
        public virtual bool isOccupied(Vector2D position)
        {
            var hashCode = position.GetHashCode();
            if (this.Animals != null && this.Animals.ContainsKey(hashCode))
                return true;

            if (this.Grasses != null && this.Grasses.ContainsKey(hashCode))
                return true;

            return false;
        }
        public virtual bool edgeReached(Vector2D position)
        {
            if (this.WIDTH == position.X ||
                this.HEIGHT == position.Y ||
                position.X == -1 ||
                position.Y == -1)
                return true;

            return false;

        }
        public virtual object objectAt(Vector2D position)
        {
            var hashCode = position.GetHashCode();

            if (this.Animals.ContainsKey(hashCode))
                return this.Animals[hashCode];

            if (this.Grasses.ContainsKey(hashCode))
                return this.Grasses[hashCode];

            return null;
        }
        public virtual bool place(object obj)
        {
            if (((obj.GetType() != typeof(Grass)) && (obj.GetType() != typeof(Animal))) || obj == null)
                return false;

            Animal animal;

            if (obj.GetType() == typeof(Animal))
            {
                animal = (Animal)obj;

                if (isOccupied(animal._position) && (objectAt(animal._position) is Animal))
                    return false;

                if (this.Grasses != null && this.Grasses.ContainsKey(animal._position))
                {
                    var grass = (Grass)this.Grasses[animal._position.GetHashCode()];
                    if (grass != null)
                        this.Grasses.Remove(grass);
                }

                //this.Animals.Add(animal._position.GetHashCode(), animal);
                //List<Animal> animals = new List<Animal>() { animal };
                //this.Animals.Add(animal._position.GetHashCode(), animals);

                this.addNewAnimal(animal._position);

                return true;
            }

            return (obj.GetType() == typeof(Grass));
        }

        public virtual Hashtable getAnimals()
        {
            return this.Animals;
        }

        public virtual void positionChanged(Vector2D oldPosition, object obj)
        {
            var animal = (Animal)obj;

            if (animal != null)
            {
                var key = oldPosition.GetHashCode();
                var key2 = animal._position.GetHashCode();

                var animalsAtPosition = (List<Animal>)this.Animals[key];
                var animalsAtNewPosition = (List<Animal>)this.Animals[key2];

                if (animalsAtPosition.Count == 1)
                {
                    this.Animals.Remove(oldPosition.GetHashCode());
                }
                else
                {
                    var an = animalsAtPosition.Find(a => a == animal);
                    animalsAtPosition.Remove(an);
                }

                this.addNewAnimal(animal._position,animal);
            }
        }

        public virtual Animal addNewAnimal(Vector2D position,Animal animal = null)
        {
            if(animal is null)
                animal = new Animal(this, position);

            var key = position.GetHashCode();
            var animals = this.Animals;

            if(!animals.ContainsKey(key))
            {
                this.Animals.Add(key, new List<Animal>() { animal });
            }
            else
            {
                List<Animal> animalsAtPosition = (List<Animal>)this.Animals[key];
                animalsAtPosition.Add(animal);
            }
            return animal;
        }

        protected virtual void cleanMap()
        {
            int[] keys = new int[this.Animals.Keys.Count];
            this.Animals.Keys.CopyTo(keys, 0);

            for (int i=0;i<keys.Length;i++)
            {
                var key = keys[i];
                List<Animal> animals = (List<Animal>)this.Animals[key];

                animals.RemoveAll(a=>a.energy<=0);
                this.Animals[key] = animals;

                if (animals.Count==0)
                    this.Animals.Remove(key);
            }
        }


        public int GetAnimalsAmount()
        {
            var animalsAmount = 0;

            foreach(List<Animal> a in this.Animals.Values)
            {
                animalsAmount += a.Count;
            }
            return animalsAmount;
        }
        public virtual void afterDay()
        {
            this.cleanMap();
            this.AnimalsAmount = this.GetAnimalsAmount();
            this.GrassesAmount = this.Grasses.Values.Count;
        }
        public virtual void moveAnimals(int n)
        {
            int[] keys=new int[this.Animals.Keys.Count];
            this.Animals.Keys.CopyTo(keys, 0);

            for(int i=0;i<keys.Length;i++)
            {
                var key = keys[i];
                var animals = (List<Animal>)this.Animals[key];
                for (int j = 0; j < animals.Count; j++)
                {
                    int gen = animals[j].Genom[n];
                    animals[j].move(gen);
                }
            }
        }
        public virtual void eatGrass(Vector2D grassPosition)
        {
            var key = grassPosition.GetHashCode();
            this.Grasses.Remove(key);
        }   
        public virtual bool reproduce(Vector2D position)
        {
            if (this.canReproduce(position))
            {
                var key = position.GetHashCode();
                List<Animal> animals = (List<Animal>)this.Animals[key];

                animals = animals.OrderByDescending(a => a.energy).ToList();

                var a1 = animals.ElementAt(0);
                var a2 = animals.ElementAt(1);

                decimal minEnergy = Convert.ToDecimal(this.ENERGY_TO_REPRODUCE) * Convert.ToDecimal(this.START_ENERGY);
                decimal totalEnergy = a1.energy+ a2.energy;


                if (totalEnergy > minEnergy && a2.energy>minEnergy)
                {
                    decimal percentage = Helper.compareEnergy(a1.energy, a2.energy);
                    var newEnergyA1 = (1 - percentage) * Convert.ToDecimal(a1.energy);
                    var newEnergyA2 = percentage * Convert.ToDecimal(a2.energy);
                    decimal newEnergy = newEnergyA1 + newEnergyA2;

                    int newEnergyInt = Convert.ToInt32(Math.Floor(newEnergy));

                    a1.burnEnergy(Convert.ToInt32(Math.Floor(newEnergyA1)));
                    a2.burnEnergy(Convert.ToInt32(Math.Floor(newEnergyA2)));

                    this.addNewAnimal(position,new Animal(this,position,newEnergyInt));

                    return true;
                }
            }

            return false;
        }

        public virtual bool canReproduce(Vector2D position)
        {
            var key = position.GetHashCode();

            List<Animal> animalsAtPosition;

            if (!this.Animals.ContainsKey(key))
                return false;

            animalsAtPosition = (List<Animal>)this.Animals[key];

            return animalsAtPosition.Count > 1;
        }

        public void positionChanged(IWorldMap map)
        {
            throw new NotImplementedException();
        }

        public void positionChanged(object obj, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
