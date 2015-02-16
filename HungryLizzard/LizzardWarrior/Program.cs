using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HungryLizard
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Fly : Coordinate
    {
        public int points { get; set; }
        public ConsoleColor color { get; set; }
        public char symbol { get; set; }
    }

    class Hero : Coordinate
    {
        public char symbol { get; set; }
        public ConsoleColor color { get; set; }

    }
    class Program
    {
        public static Hero Hero { get; set; }

        static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);

        } 
        
        static void InitGame()//initializes the game and sets the Hero in the middle of the console
        {
            Hero = new Hero()
            {
                X = Console.WindowWidth/2,
                Y = Console.WindowHeight - 4,
                symbol = '@',
                color = ConsoleColor.Black
            };
            MoveHero(0);
        }

        /// <summary>
        /// New MoveHero() function
        /// </summary>
        /// <param name="a"></param>
        static void MoveHero(int a)//moves the Hero, where a is number of positions its moved
        {
            Hero.X = Hero.X + a;
           
        }
        //static void MoveHero(int a)//moves the Hero, where a is number of positions its moved
        //{
        //    Coordinate newHero = new Coordinate
        //    {
        //    Hero.X = Hero.X + a;
        //        Y = Console.WindowHeight - 4
        //    };
        //    if (CanMove(newHero))
        //    {               
        //        RemoveHero();
        //        Console.Clear();
        //        Console.ForegroundColor = ConsoleColor.Black;
        //        Console.SetCursorPosition(newHero.X, newHero.Y-1);
        //        Console.Write("\\  /");
        //        Console.SetCursorPosition(newHero.X, newHero.Y);
        //        Console.Write("****");
        //        Hero = newHero;
        //    }
            
        //}

        static void RemoveHero()
        {

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.Write(" ");
        }

        static bool CanMove(Coordinate c)//checks if Hero is not out of borders of console
        {
            if (c.X>=1 && c.X<=Console.WindowWidth-5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void DrawGrid()//draws frame
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("+");
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write("+");
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("+");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 3);
            Console.Write("+");
            for (int i = 1; i < Console.WindowWidth-1; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("=");
                Console.SetCursorPosition(i, Console.WindowHeight-3);
                Console.Write("=");
            }
            for (int j = 1; j < Console.WindowHeight-3; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("|");
                Console.SetCursorPosition(Console.WindowWidth-1, j);
                Console.Write("|");
            }
        }

        /// <summary>
        /// Set Console width and height, set Console background color and make cursor invisible
        /// </summary>
        static void InitConsole()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 90;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.CursorVisible = false;
        }
        //Main
        static void Main(string[] args) 
        {
            InitConsole();
            InitGame();

            Random randomGenerator = new Random();
            List<Fly> flies = new List<Fly>();
            while (true)
            {
                Fly newRandomFly = new Fly();
                newRandomFly.color = ConsoleColor.Red;
                newRandomFly.X = randomGenerator.Next(0, 89);
                newRandomFly.Y = 1;
                newRandomFly.symbol = '%';
                flies.Add(newRandomFly);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        MoveHero(-1);
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        MoveHero(1);
                    }
                }

                List<Fly> newFlies = new List<Fly>();
                for (int i = 0; i < flies.Count; i++)
                {
                    Fly oldFly = flies[i];
                    Fly newFly = new Fly();
                    newFly.X = oldFly.X;
                    newFly.Y = oldFly.Y + 1;
                    newFly.color = oldFly.color;
                    newFly.symbol = oldFly.symbol;
                    if (newFly.Y < Console.WindowHeight - 3)
                    {
                        newFlies.Add(newFly);
                    }
                }
                flies = newFlies;

                Console.Clear();
                PrintOnPosition(Hero.X, Hero.Y, Hero.symbol, Hero.color);
                foreach (Fly fly in flies)
                {
                    PrintOnPosition(fly.X, fly.Y, fly.symbol, fly.color);
                }
                // DrawGrid();
                Thread.Sleep(150);
            }
        }

    }
}
