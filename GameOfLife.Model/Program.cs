using System;

namespace GameOfLife.Model
{
    class Program
    {
        enum Direction
        {
            FORWARD,
            BACKWORD,
            RIGHT,
            LEFT
        }


        static void Main(string[] args)
        {
            Console.WriteLine("START");
            foreach(var a in args)
            {
                var consoleMessage = a switch
                {
                    "f" => "Do Przodu",
                    "b" => "Do Tyłu",
                    "r" => "W Prawo",
                    "l" => "W Lewo",
                    _ => ""
                };

                Console.WriteLine(consoleMessage);
            }
            Console.WriteLine("STOP");
        }
    }
}
