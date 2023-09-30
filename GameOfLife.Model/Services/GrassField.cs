using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Services
{
    public class GrassField : AbstractWorldMap
    {
        private List<IPositionChangeObserver> _observers { get; set; }  
        public GrassField(int width, int height, int grassAmount, int grassesPerDay,int grassEnergy)
        {
            this.Animals = new Hashtable();
            this.Grasses = new Hashtable();

            this._observers = new List<IPositionChangeObserver>();

            this.WIDTH = width;
            this.HEIGHT = height;

            this.GRASSES_PER_DAY = grassesPerDay;
            this.GRASS_ENERGY = grassEnergy;

            this.fillWithGrass(grassAmount);
        }

 

        public override object objectAt(Vector2D position)
        {
            return base.objectAt(position);
        }
        public override bool canMoveTo(Vector2D position)
        {
            return base.canMoveTo(position);
        }
        public override bool place(object obj)
        {
            Grass grass;
            var result = base.place(obj);

            if(result && obj.GetType() == typeof(Animal))
                return true;

                if (obj.GetType() == typeof(Grass))
                {
                    grass = (Grass)obj;
                    var pos = grass.getPosition();

                    if (objectAt(pos) is Grass)
                        return false;

                    Console.WriteLine($"{grass._position.ToString()} - {grass._position.GetHashCode()}");

                    this.Grasses.Add(grass._position.GetHashCode(), grass);
                    return true;
                }

            return false;
        }

        public override string ToString()
        {
            IWorldMap map = this;
            var bottom = 0;
            var top = this.Grasses.Count;

            MapVisualizer mv = new MapVisualizer(map);

            return mv.draw(new Vector2D(bottom,bottom), new Vector2D(top,top));
        }

        private void fillWithGrass(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random();

                var x = rnd.Next(0, this.WIDTH);
                var y = rnd.Next(0, this.HEIGHT);

                var occupied = this.isOccupied(new Vector2D(x, y));
                var grassCounter = 0;
                while(occupied && grassCounter<10)
                {
                    x = rnd.Next(0, this.WIDTH);
                    y = rnd.Next(0, this.HEIGHT);

                    occupied = this.isOccupied(new Vector2D(x, y));
                    grassCounter++;
                }

                //is occupied
                Grass grass = new Grass(new Vector2D(x, y));
                this.place(grass);

                this.positionChanged(null, grass);
            }
        }

        public override void afterDay()
        {
            base.afterDay();
            this.fillWithGrass(this.GRASSES_PER_DAY);
        }
        public void addObserver(IPositionChangeObserver observer)
        {
            this._observers.Add(observer);
        }
        public void removeObserver(IPositionChangeObserver observer)
        {
            this._observers.Remove(observer);
        }
        public override void positionChanged(Vector2D oldPosition, object obj)
        {
            if(obj.GetType() == typeof(Animal))
                base.positionChanged(oldPosition, obj);

            foreach (var observer in this._observers)
            {
                //observer.positionChanged(oldPosition, newPosition);
                observer.positionChanged(this,new EventArgs());
            }
        }
    }
}
