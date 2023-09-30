using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Model
{
    public class Vector2D
    {
        public readonly int X;
        public readonly int Y;

        public Vector2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }

        public override int GetHashCode()
        {
            var hashedX = this.X.GetHashCode();
            var hashedY = this.Y.GetHashCode();

            //Convert.ToInt64(Math.Pow(2, this.X)) * Convert.ToInt64(Math.Pow(3, this.Y));

            //return Convert.ToInt32(Math.Pow(2, this.X)) * Convert.ToInt32(Math.Pow(3, this.Y));

            return (((hashedX + hashedY) * (hashedX + hashedY + 1)) / 2) + hashedY;

            //return DateTime.Now.Millisecond;
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
        public Vector2D upperRight(Vector2D other)
        {
            return new Vector2D(Math.Max(other.X, this.X), Math.Max(other.Y, this.Y));
        }
        public Vector2D lowerLeft(Vector2D other)
        {
            return new Vector2D(Math.Min(other.X, this.X), Math.Min(other.Y, this.Y));
        }
        public Vector2D opposite(Vector2D other)
        {
            return new Vector2D(-other.X, -other.Y);
        }   



        public Vector2D upperRight()
        {
            return new Vector2D(this.X+1, this.Y+1);
        }
        public Vector2D upperLeft()
        {
            return new Vector2D(this.X-1, this.Y+1);
        }
        public Vector2D lowerRight()
        {
            return new Vector2D(this.X + 1, this.Y - 1);
        }
        public Vector2D lowerLeft()
        {
            return new Vector2D(this.X - 1, this.Y - 1);
        }
        public Vector2D right()
        {
            return new Vector2D(this.X+1,this.Y);
        }
        public Vector2D left()
        {
            return new Vector2D(this.X - 1, this.Y);
        }
        public Vector2D up()
        {
            return new Vector2D(this.X, this.Y + 1);
        }
        public Vector2D down()
        {
            return new Vector2D(this.X, this.Y - 1);
        }



        public Vector2D narrowMap()
        {
            var narrowedVector = this;

            if (this.X > 4)
                narrowedVector = this.add(new Vector2D(-1, 0));
            if (this.Y > 4)
                narrowedVector = this.add(new Vector2D(0, -1));
            if (this.X < 0)
                narrowedVector = this.add(new Vector2D(1, 0));
            if (this.Y < 0)
                narrowedVector = this.add(new Vector2D(0, 1));

            return narrowedVector;
        }
        public bool equals(Vector2D other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
