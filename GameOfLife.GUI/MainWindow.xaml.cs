using GameOfLife.GUI.Model;
using GameOfLife.GUI.UserControls;
using GameOfLife.Model;
using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;
using System;
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

namespace GameOfLife.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AbstractWorldMap map;
        GrassField gf;
        GameMap gm;

        public MainWindow()
        {
            //this.DrawMap();
            this.gf = new GrassField(10, 10, 10);
            this.gm = new GameMap(this.gf);

            //this.AddChild(this.gm);

            InitializeComponent();
            
        }


        //private void AddOrRemoveItem(object sender, MouseButtonEventArgs e)
        //{
        //    if(e.OriginalSource is Rectangle)
        //    {
        //        Rectangle activeRectangle = (Rectangle)e.OriginalSource;

        //        myCanvas.Children.Remove(activeRectangle);
        //    }
        //    else
        //    {
        //        customColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1,255)));

        //        Rectangle rct = new Rectangle
        //        {
        //            Width = 50,
        //            Height = 50,
        //            Fill = customColor,
        //            StrokeThickness = 3,
        //            Stroke = Brushes.Black
        //        };

        //        Canvas.SetLeft(rct, Mouse.GetPosition(myCanvas).X);
        //        Canvas.SetTop(rct, Mouse.GetPosition(myCanvas).Y);

        //        myCanvas.Children.Add(rct);
        //    }
        //}

        //private void DrawMap()
        //{
        //    for(int i = 0; i < 10; i++)
        //    {
        //        for(int j = 0; j < 10; j++)
        //        {
        //            customColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255)));

        //            Rectangle rct = new Rectangle
        //            {
        //                Width = 40,
        //                Height = 40,
        //                Fill = customColor,
        //                StrokeThickness = 3,
        //                Stroke = Brushes.Black
        //            };

        //            Canvas.SetLeft(rct, i*40);
        //            Canvas.SetTop(rct, j*40);

        //            this.rectangles.Add(rct);
        //        }
        //    }

        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    //lblTime.Content = DateTime.Now.ToLongTimeString();
        //    this.DrawMap();
        //    myCanvas.Children.Clear();
        //    foreach (var r in this.rectangles)
        //    {
        //        myCanvas.Children.Add(r);
        //    }
        //}
    }
}
