using GameOfLife.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameOfLife.Model.Services
{
    public class MapVisualizer : IPositionChangeObserver
    {
        private const string EMPTY_CELL = " ";
        private const string FRAME_SEGMENT = "-";
        private const string CELL_SEGMENT = "|";
        private IWorldMap map;

        /**
         * Initializes the MapVisualizer with an instance of map to visualize. 
         * @param map
         */
        public MapVisualizer(IWorldMap map)
        {
            this.map = map;
        }

        /**
         * Convert selected region of the map into a string. It is assumed that the
         * indices of the map will have no more than two characters (including the
         * sign).
         *
         * @param lowerLeft  The lower left corner of the region that is drawn.
         * @param upperRight The upper right corner of the region that is drawn.
         * @return String representation of the selected region of the map.
         */
        public string draw(Vector2D lowerLeft, Vector2D upperRight)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = upperRight.Y + 1; i >= lowerLeft.Y - 1; i--)
            {
                if (i == upperRight.Y + 1)
                {
                    builder.Append(drawHeader(lowerLeft, upperRight));
                }
                builder.Append($"{i.ToString()}  ");
                for (int j = lowerLeft.X; j <= upperRight.X; j++)
                {
                    if (i < lowerLeft.Y || i > upperRight.Y)
                    {
                        builder.Append(drawFrame(j <= upperRight.X));
                    }
                    else
                    {
                        builder.Append(CELL_SEGMENT);
                        if (j <= upperRight.X)
                        {
                            builder.Append(drawObject(new Vector2D(j, i)));
                        }
                    }
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public void positionChanged(Vector2D oldPosition, object obj)
        {
            throw new NotImplementedException();
        }

        public void positionChanged(Vector2D oldPosition, Vector2D newPosition)
        {
            throw new NotImplementedException();
        }

        public void positionChanged(IWorldMap map)
        {
            this.map = map;
            var consoleMap = this.draw(new Vector2D(0,0),new Vector2D(10,10));
            Console.WriteLine(consoleMap);
        }

        public void positionChanged(object obj, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string drawFrame(bool innerSegment)
        {
            if (innerSegment)
            {
                return FRAME_SEGMENT + FRAME_SEGMENT;
            }
            else
            {
                return FRAME_SEGMENT;
            }
        }

        private string drawHeader(Vector2D lowerLeft, Vector2D upperRight)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" y\\x ");
            for (int j = lowerLeft.X; j < upperRight.X + 1; j++)
            {
                builder.Append($"{j.ToString()} ");
            }
            builder.Append(Environment.NewLine);
            return builder.ToString();
        }

        private string drawObject(Vector2D currentPosition)
        {
            string result = null;
            if (this.map.isOccupied(currentPosition))
            {
                object obj = this.map.objectAt(currentPosition);
                if (obj != null)
                {
                    result = obj.ToString();
                }
                else
                {
                    result = EMPTY_CELL;
                }
            }
            else
            {
                result = EMPTY_CELL;
            }
            return result;
        }
    }
}
