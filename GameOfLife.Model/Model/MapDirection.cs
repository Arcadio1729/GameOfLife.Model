using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Model
{
    public enum Direction
    {
        NORTH,
        SOUTH,
        WEST,
        EAST
    }
    public class MapDirection
    {
        public Direction Direction { get; set; }

        public MapDirection(Direction direction)
        {
            this.Direction = direction;
        }
        public override string ToString()
        {
            return Direction switch
            {
                Direction.NORTH => "N",
                Direction.SOUTH => "S",
                Direction.WEST => "W",
                Direction.EAST => "E",
                _ => ""
            };
        }

        public Direction next()
        {
            return Direction switch
            {
                Direction.NORTH => Direction.EAST,
                Direction.EAST => Direction.SOUTH,
                Direction.SOUTH => Direction.WEST,
                Direction.WEST => Direction.NORTH
            };
        }
        public Direction previous()
        {
            return Direction switch
            {
                Direction.SOUTH => Direction.EAST,
                Direction.EAST => Direction.NORTH,
                Direction.NORTH => Direction.WEST,
                Direction.WEST => Direction.SOUTH
            };
        }
        public void convertToMapDirection(MoveDirection moveDirection)
        {
            this.Direction = moveDirection switch
            {
                MoveDirection.RIGHT => Direction.EAST,
                MoveDirection.FORWARD => Direction.NORTH,
                MoveDirection.LEFT => Direction.WEST,
                MoveDirection.BACKWARD => Direction.SOUTH
            };
        }
        public Vector2D toUnitVector()
        {
            return Direction switch
            {
                Direction.SOUTH => new Vector2D(0, -1),
                Direction.EAST => new Vector2D(1, 0),
                Direction.NORTH => new Vector2D(0, 1),
                Direction.WEST => new Vector2D(-1, 0)
            };
        }
    }
}
