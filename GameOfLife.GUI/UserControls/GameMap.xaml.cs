using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameOfLife.Model;
using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;

namespace GameOfLife.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for GameMap.xaml
    /// </summary>
    public partial class GameMap : UserControl, IPositionChangeObserver
    {
        public Canvas canvas { get; set; }


        private IWorldMap _map;

        const int WIDTH = 10;
        const int HEIGHT = 10;

        public Hashtable Rectangles;

        const int CELL_SIZE = 40;

        GrassField map;
        public GameMap(GrassField map)
        {
            InitializeComponent();
            this.map = map;
            this.Rectangles = new Hashtable();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer_Tick;
            timer.Start();
            this.ReadGame();
        }

        public void drawMap()
        {
            mapCanvas.Children.Clear();
            for (int i = 0; i < WIDTH; i++)
            {
                for(int j=0; j < HEIGHT; j++)
                {
                    var pos = new Vector2D(i, j);
                    var key = pos.GetHashCode();
                    var rct = (Rectangle)this.Rectangles[key];

                    Canvas.SetLeft(rct, i * 40);
                    Canvas.SetTop(rct, j * 40);

                    mapCanvas.Children.Add(rct);
                }
            }



        }

        private Rectangle GetCell(object obj)
        {
            var rct = new Rectangle
            {
                Width = CELL_SIZE,
                Height = CELL_SIZE,
                Fill = new SolidColorBrush(Color.FromRgb((byte)211, (byte)211, (byte)211)),
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            if(obj is null)
                return rct;

            if(obj.GetType() == typeof(Animal))
            {
                rct.Fill = new SolidColorBrush(Color.FromRgb((byte)30, (byte)144, (byte)255));
                return rct;
            }

            if (obj.GetType() == typeof(Grass))
            {
                rct.Fill = new SolidColorBrush(Color.FromRgb((byte)0, (byte)255, (byte)0));
                return rct;
            }

            throw new NotImplementedException();
        }
        private void ReadGame()
        {
            MoveDirection[] directions = OptionsParser.parse("f,f,l,f,f,r,f,f,r,f,f,f,b,r,f,r,f,f,b");
            
            var v2 = new Vector2D(1, 1);
            var v1 = new Vector2D(4, 3);

            Vector2D[] positions = { v1 };
            map.addObserver(this);
            IEngine engine = new SimulationEngine(directions, map, positions);

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
            this._map = map;            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var pos = new Vector2D(i, j);
                    var key = pos.GetHashCode();

                    var obj = this._map.objectAt(new Vector2D(i, j));
                    Rectangle rct = this.GetCell(obj);
                    this.Rectangles.Add(key,rct);
                }
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            this.drawMap();
        }
    }
}
