using GameOfLife.Model.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Model
{
    public class Animal
    {
        private MapDirection _direction { get; set; }     
        private List<IPositionChangeObserver> _observers { get; set; }
        public Vector2D _position { get;private set; }
        public AbstractWorldMap _map { get; set; }

        #region Animal Parameters
        public int[] Genom { get; set; }
        public int energy { get; set; }
        #endregion
        public Animal()
        {
            this._direction = new MapDirection(Direction.NORTH);
            this._position = new Vector2D(2,2);
            this._observers = new List<IPositionChangeObserver>();
        }
        public Animal(AbstractWorldMap map, Vector2D initialPosition, int energy=0)
        {
            this._direction = new MapDirection(Direction.NORTH);
            this._position = initialPosition;
            this._observers = new List<IPositionChangeObserver>();
            this._map = map;
            this._observers.Add(map);

            if (energy > 0)
            {
                this.energy = energy;
            }
            else
            {
                this.energy = map.START_ENERGY;
            }

            this.generateGenom();
        }


        private void generateGenom()
        {
            Random rnd = new Random();
            this.Genom = Enumerable.Range(1, 64).OrderBy(r => rnd.Next()).Select(r=>(r%8+1)).Take(8).ToArray();
        }
        public override string ToString()
        {
            return $"{this._position.ToString()} {this._direction.ToString()}";
        }

        public void move(int moveDirection)
        {
            Vector2D oldPosition = new Vector2D(this._position.X, this._position.Y);
            Vector2D newPosition;

            var potentialPosition = moveDirection switch
            {
                1 => oldPosition.up(),
                2 => oldPosition.upperRight(),
                3 => oldPosition.right(),
                4 => oldPosition.lowerRight(),
                5 => oldPosition.down(),
                6 => oldPosition.lowerLeft(),
                7 => oldPosition.left(),
                8 => oldPosition.upperLeft()
            };

            if (potentialPosition != null)
            {
                if (this._map.canMoveTo(potentialPosition))
                {
                    this._position = potentialPosition;
                }
                else
                {
                    if (!this._map.edgeReached(potentialPosition))
                    {
                        var obj = this._map.objectAt(potentialPosition);

                        if (obj != null && obj.GetType() == typeof(Grass))
                        {
                            this._map.eatGrass(potentialPosition);
                            this.energy += this._map.GRASS_ENERGY;
                            this._position = potentialPosition;
                        }

                        if(obj != null && obj.GetType() == typeof(List<Animal>))
                        {
                            this._position = potentialPosition;
                        }
                    }
                }
            }

            this._map.positionChanged(oldPosition, this);
            this.burnEnergy();
            this._map.reproduce(potentialPosition);
            this._map.positionChanged(this._position, this);
        }
        public Boolean isAt(Vector2D position)
        {
            return this._position.equals(position);
        }


        public void addObserver(IPositionChangeObserver observer)
        {
            this._observers.Add(observer);
        }
        public void removeObserver(IPositionChangeObserver observer)
        {
            this._observers.Remove(observer);
        }



        public void burnEnergy(int n=0)
        {
            if (n == 0)
            {
                this.energy -= this._map.LOST_ENERGY;
            }
            else
            {
                this.energy -= n;
            }
        }
    }
}
