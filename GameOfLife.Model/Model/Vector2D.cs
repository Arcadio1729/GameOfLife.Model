using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Model
{
    public class Vector2D
    {
        private readonly int X;
        private readonly int Y;

        public Vector2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }

        public bool precedes(Vector2D other)
        {
            return (this.X <= other.X && this.Y <= other.Y) ? true : false;
        }

        public bool follows(Vector2D other)
        {
            return (this.X >= other.X && this.Y >=other.Y) ? true : false;
        }

        public Vector2D add(Vector2D other)
        {
            return new Vector2D(this.X + other.X, this.Y + other.Y);
        }

        public Vector2D subtract(Vector2D other)
        {
            return new Vector2D(this.X - other.X, this.Y - other.Y);
        }


    }
}
