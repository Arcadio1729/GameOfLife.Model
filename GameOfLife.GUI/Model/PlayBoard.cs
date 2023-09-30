using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Ink;

namespace GameOfLife.GUI.Model
{
    public class PlayBoard
    {
        public Canvas canvas;

        public PlayBoard(int width, int height)
        {
            canvas = new Canvas();

            canvas.Width = width;
            canvas.Height = height;

            Rectangle r1 = new Rectangle();
            r1.Fill = new SolidColorBrush(Color.FromRgb(3, 4, 111));

            Rectangle r2 = new Rectangle();
            r2.Fill = new SolidColorBrush(Color.FromRgb(113, 4, 11));

            canvas.Children.Add(r1);
            canvas.Children.Add(r2);    

            //playGrid = new Grid();
            //playGrid.Width = width - 20;
            //playGrid.Height = height - 20;

            //for (int i = 0; i < (playGrid.Height / 10); i++)
            //{
            //    var myRowDefinition = new RowDefinition();
            //    myRowDefinition.Height = new GridLength(40);
            //    playGrid.RowDefinitions.Add(myRowDefinition);
            //}

            //for (int j = 0; j < (playGrid.Width / 10); j++)
            //{
            //    var myColumnDefinition = new ColumnDefinition();
            //    myColumnDefinition.Width = new GridLength(40);
            //    playGrid.ColumnDefinitions.Add(myColumnDefinition);
            //}
            //var myRect = new System.Windows.Shapes.Rectangle();
            //myRect.Stroke = System.Windows.Media.Brushes.Black;
            //myRect.Fill = System.Windows.Media.Brushes.SkyBlue;
            //myRect.HorizontalAlignment = HorizontalAlignment.Left;
            //myRect.VerticalAlignment = VerticalAlignment.Center;
            //myRect.Height = 10;
            //myRect.Width = 10;
            //playGrid.Children.Add(myRect);
        }
    }
}
