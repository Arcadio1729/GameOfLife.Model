using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Model
{
    public class Grass
    {
        public Vector2D _position;
        public Grass(Vector2D position)
        {
            this._position = position;                
        }

        public Vector2D getPosition()
        {
            return this._position;
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
