using GameOfLife.Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public class RectangularMap : AbstractWorldMap
    {
        public RectangularMap(int width, int height) 
        {
            this.WIDTH = width;
            this.HEIGHT = height;
            this.Animals= new Hashtable();
        }

        public override bool canMoveTo(Vector2D position)
        {
            var result = base.canMoveTo(position);

            if (result)
            {
                if (this.WIDTH == position.X || this.HEIGHT == position.Y)
                    return false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            var mv = new MapVisualizer(this);
            return mv.ToString();
        }
    }
}
