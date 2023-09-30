using GameOfLife.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public interface IPositionChangeObserver
    {
        void positionChanged(object obj, EventArgs e);
        void positionChanged(IWorldMap map);
        void positionChanged(Vector2D oldPosition, object obj);

    }
}
