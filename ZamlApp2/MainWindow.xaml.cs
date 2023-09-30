using GameOfLife.Model;
using GameOfLife.Model.Model;
using GameOfLife.Model.Services;
using GameOfLife.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace ZamlApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPositionChangeObserver, INotifyPropertyChanged
    {
        Thread t,t1,t2;
        decimal _animalsPercent = Convert.ToDecimal(0.4);

        int _width;
        int _height;
        int _animalsAmount;
        int _maxAnimalsAmount;
        int _grassesAmount;
        int _grassesPerDay;
        int _grassEnergy;

        int _rectangleWidth;
        int _rectangleHeight;


        
        public bool END_GAME = false;

        public int MAX_ANIMALS_AMOUNT 
        {
            get
            {
                return this._maxAnimalsAmount;
            }
            set
            {
                this._maxAnimalsAmount = value;
                this.OnPropertyChanged();
            }
        }
        public int ANIMALS_AMOUNT
        {
            get
            {
                return this._animalsAmount;
            }
            set
            {
                this._animalsAmount = value;
                this.OnPropertyChanged();

            }
        }
        public int GRASSES_AMOUNT
        {
            get
            {
                return this._grassesAmount;
            }
            set
            {
                this._grassesAmount = value;
                this.OnPropertyChanged();

            }
        }
        public int GRASSES_PER_DAY
        {
            get
            {
                return this._grassesPerDay;
            }
            set
            {
                this._grassesPerDay = value;
                this.OnPropertyChanged();
            }
        }
        public int GRASS_ENERGY
        {
            get
            {
                return this._grassEnergy;
            }
            set
            {
                this._grassEnergy = value;
                this.OnPropertyChanged();
            }
        }
        public int MAP_WIDTH 
        {
            get
            {
                return this._width;
            }   
            set
            {
                this._width=value;
                this._rectangleWidth = Convert.ToInt32(Math.Floor(Convert.ToDecimal(400) / Convert.ToDecimal(this._width)));

                decimal bars = Convert.ToDecimal(this._height * this._width);

                this.MAX_ANIMALS_AMOUNT = Convert.ToInt32(Math.Floor(bars * this._animalsPercent));

                this.OnPropertyChanged();
            } 
        }
        public int MAP_HEIGHT
        {
            get
            {
                return this._height;
            }
            set
           {
                this._height = value;
                decimal bars = Convert.ToDecimal(this._height * this._width);
                this.MAX_ANIMALS_AMOUNT = Convert.ToInt32(Math.Floor(bars * this._animalsPercent));
                this._rectangleHeight = Convert.ToInt32(Math.Floor(Convert.ToDecimal(400) / Convert.ToDecimal(this._height)));
                this.OnPropertyChanged();
            }
        }

        int counter = 0;

        AbstractWorldMap map;
        GrassField gf; 
        MoveDirection[] _directions;
        Vector2D[] _positions;

        public bool continueGame = true;

        DispatcherTimer dispatcherTimer;

        int CurrentAnimalCounter = 0;
        public MainWindow() 
        {
            InitializeComponent();
            DataContext = this;

            MenuWindow menuWindow = new MenuWindow(this);
            menuWindow.Show();
        }


        void increment()
        {
            counter++;
            //Thread.Sleep(1000);
        }


        Rectangle GetCell(object obj)
        {
            var rct = new Rectangle
            {
                Width = this._rectangleWidth,
                Height = this._rectangleHeight,
                Fill = new SolidColorBrush(Color.FromRgb((byte)211, (byte)211, (byte)211)),
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            if (obj is null)
                return rct;

            if (obj.GetType() == typeof(Animal))
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\cow.png");
                //rct.Fill = new SolidColorBrush(Color.FromRgb((byte)30, (byte)144, (byte)255));
                rct.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(path))
                };
                return rct;
            }
            if (obj.GetType() == typeof(List<Animal>))
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\cow.png");
                //rct.Fill = new SolidColorBrush(Color.FromRgb((byte)30, (byte)144, (byte)255));
                rct.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(path))
                };
                return rct;
            }
            if (obj.GetType() == typeof(Grass))
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\grass.png");

                //rct.Fill = new SolidColorBrush(Color.FromRgb((byte)0, (byte)255, (byte)0));
                rct.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(path))
                };
                return rct;
            }

            throw new NotImplementedException();
        }

        void drawMap()
        {
            myCanvas.Children.Clear();

            for(int i = 0; i < this.MAP_WIDTH; i++)
            {
                for(int j= 0; j < this.MAP_HEIGHT; j++)
                {
                    var obj = this.map.objectAt(new Vector2D(i, j));
                    var rct = this.GetCell(obj);

                    Canvas.SetLeft(rct, i * this._rectangleWidth);
                    Canvas.SetTop(rct, j * this._rectangleHeight);
                    myCanvas.Children.Add(rct);
                }
            }

            var animals = this.map.getAnimals();
            var animalsTxt = "";

            var animalsAmount = this.map.AnimalsAmount.ToString();
            var grassesAmount = this.map.GrassesAmount.ToString();

            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();

            tb1.Name = "animalTxtBlock";
            tb1.Width = 100;
            tb1.Height = 50;

            tb2.Name = "grassTxtBlock";
            tb2.Width = 100;
            tb2.Height = 50;

            Canvas.SetLeft(tb1, 400);
            Canvas.SetTop(tb1, 40);

            Canvas.SetLeft(tb2, 400);
            Canvas.SetTop(tb2, 100);

            tb1.Text = $"Animals - {animalsAmount}";
            tb2.Text = $"Grasses - {grassesAmount}";

            myCanvas.Children.Add(tb1);
            myCanvas.Children.Add(tb2);
        }

        public void positionChanged(object obj, EventArgs e)
        {
            this.map=(AbstractWorldMap)obj;
        }



        public void positionChanged(IWorldMap map)
        {
            throw new NotImplementedException();
        }

        public void positionChanged(Vector2D oldPosition, object obj)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.END_GAME = false;
            this.continueGame = true;
            this.startGame();
        }

        public void startGame()
        {
            this.gf = new GrassField(this.MAP_WIDTH, this.MAP_HEIGHT,this.GRASSES_AMOUNT,this.GRASSES_PER_DAY,this.GRASS_ENERGY);
            this.map = this.gf;

            List<Vector2D> inputAnimalPositions = new List<Vector2D>();

            for (int i = 0; i < this.ANIMALS_AMOUNT; i++)
            {
                var rnd = new Random();

                var x = rnd.Next(0, this.MAP_WIDTH);
                var y = rnd.Next(0, this.MAP_HEIGHT);

                inputAnimalPositions.Add(new Vector2D(x, y));
            }
            
            this.gf.addObserver(this);

            foreach (var position in inputAnimalPositions)
            {
                this.map.place(new Animal(this.map, position));
            }
            t1 = new Thread(() =>
            {
                var counter = 1;
                while (counter < 9 && END_GAME == false)
                {
                    if (continueGame)
                    {
                        this.map.moveAnimals(counter - 1);
                        this.map.afterDay();
                        Dispatcher.Invoke(() => drawMap());

                        counter = (counter % 8) + 1;

                        Thread.Sleep(500);
                    }
                }
            });
            this.END_GAME = false;
            t1.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.continueGame = false;
            this.END_GAME = true;
        }

        public void positionChanged(Vector2D oldPosition, Vector2D newPosition)
        {
            throw new NotImplementedException();
        }

        private List<int> getAnimals()
        {
            Hashtable animals = this.map.getAnimals();
            List<int> animalKeys = new List<int>();

            foreach (System.Collections.DictionaryEntry an in animals)
            {
                var key = an.Key;
                animalKeys.Add(Convert.ToInt32(an.Key.GetHashCode()));
            }

            return animalKeys;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
