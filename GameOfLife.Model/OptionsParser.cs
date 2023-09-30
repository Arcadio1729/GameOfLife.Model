using GameOfLife.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public static class OptionsParser
    {
        public static MoveDirection[] parse(string directionStr)
        {
            var directions = directionStr.Split(',');
            return directions.Select(d => convertToDirection(d)).ToArray();
        }

        public static MoveDirection convertToDirection(string directionStr)
        {
            if (directionStr.ToLower() == "f" || directionStr.ToLower() == "forward")
                return MoveDirection.FORWARD;
            if (directionStr.ToLower() == "b" || directionStr.ToLower() == "backward")
                return MoveDirection.BACKWARD;
            if (directionStr.ToLower() == "r" || directionStr.ToLower() == "right")
                return MoveDirection.RIGHT;
            if (directionStr.ToLower() == "l" || directionStr.ToLower() == "left")
                return MoveDirection.LEFT;

            throw new ArgumentException($"{directionStr} is not legal move specification. Expected f,b,r,l or forward,backward,right,left.");
        }
    }
}
