using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public class World
    {

        static void Main(string[] args)
        {
            try
            {
                MoveDirection[] directions = OptionsParser.parse("f,f,l,f,f,r,f,f,r,f,f,f,b,r,f,r,f,f,b");
                AbstractWorldMap map = new RectangularMap(10, 10);
                GrassField gf = new GrassField(10,10,10,2,2);
                var mv=new MapVisualizer(map);

                gf.addObserver(mv);
                
                var v2 = new Vector2D(1, 1);
                var v1 = new Vector2D(4, 3);

                Vector2D[] positions = { v1 };
                IEngine engine2 = new SimulationEngine(directions, gf, positions);


                engine2.run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine('\n');
                Console.WriteLine("Press any button to close console.");
                Console.ReadKey();
            }
        }
    }
}
