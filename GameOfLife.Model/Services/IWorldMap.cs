using GameOfLife.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public interface IWorldMap
    {
        bool canMoveTo(Vector2D position);
        bool place(Object obj);
        bool isOccupied(Vector2D position);
        Object objectAt(Vector2D position);
    }
}
