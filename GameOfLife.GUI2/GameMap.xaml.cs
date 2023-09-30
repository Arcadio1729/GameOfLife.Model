using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GameOfLife.GUI2
{
    /// <summary>
    /// Interaction logic for GameMap.xaml
    /// </summary>
    public partial class GameMap : UserControl, IPositionChangeObserver
    {
        public delegate void DrawMap(object sender, EventArgs e);

        public event DrawMap OnDrawMap;
        public GameMap()
        {
            InitializeComponent();

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(0.5);
            //timer.Tick += positionChanged;
            //timer.Start();

            

        }
        public void positionChanged(object obj, EventArgs e)
        {
            this.OnDrawMap(obj, e);

            //if (obj.GetType() != typeof(DispatcherTimer))
            //{
            //    var map = (IWorldMap)obj;

            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            object element = map.objectAt(new Vector2D(i, j));

            //            Rectangle rct = this.GetCell(element);

            //            Canvas.SetLeft(rct, i * 40);
            //            Canvas.SetTop(rct, j * 40);

            //            this.mapCanvas.Children.Add(rct);
            //        }
            //    }
            //}
        }

        private Rectangle GetCell(object obj)
        {
            var rct = new Rectangle
            {
                Width = 40,
                Height = 40,
                Fill = new SolidColorBrush(Color.FromRgb((byte)211, (byte)211, (byte)211)),
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            if (obj is null)
                return rct;

            if (obj.GetType() == typeof(Animal))
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

        public void timer_Tick(object sender, EventArgs e)
        {
            //Random rnd = new Random();

            //var a = rnd.Next(1, 255);
            //var b = rnd.Next(1, 255);
            //var c = rnd.Next(1, 255);

            //sampleRct.Fill = new SolidColorBrush(Color.FromRgb((byte)a, (byte)b, (byte)c));
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
            throw new NotImplementedException();
        }
    }
}
