using GameOfLife.Model;
using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GameOfLife.GUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AbstractWorldMap map;
        GrassField gf;
        GameMap gm;


        public string txtCounter { get; set; }
        int counter = 0;

        public MainWindow()
        {
            this.gf = new GrassField(10, 10, 10);
            this.map = this.gf;
            this.gm=new GameMap();
            InitializeComponent();
            //this.canvasMap.Children.Add(this.gm);
            
            this.tempGrid.Children.Add(this.gm);
            this.gf.addObserver(this.gm);
            //var v1 = new Vector2D(4, 3);
            //MoveDirection[] directions = OptionsParser.parse("f,f,l,f,f,r,f,f,r,f,f,f,b,r,f,r,f,f,b");
            //Vector2D[] positions = { v1 };
            //IEngine engine2 = new SimulationEngine(directions, gf, positions);
            //engine2.run();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Worker();
        }

        void Worker()
        {
            new Thread(() => 
            {
                Dispatcher.Invoke(() =>
                {
                    var v1 = new Vector2D(2, 4);
                    var v2 = new Vector2D(1, 4);
                    var positions = new Vector2D[] { v1, v2 };

                    MoveDirection[] directions = OptionsParser.parse("f,f,l,f,f,r,f,f,r,f,f,f,b,r,f,r,f,f,b");
                    SimulationEngine se = new SimulationEngine(directions, this.map, positions);

                    se.run();
                });
            }).Start();
                
        }
    }
}
