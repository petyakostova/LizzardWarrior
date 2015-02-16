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

    class Creature : Coordinate
    {
        public int points { get; set; }
        public ConsoleColor color { get; set; }
        public char symbol { get; set; }
    }
    class Program
    {
        public static Creature Hero { get; set; }

        static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);

        }

        static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
        
        static void InitGame()//initializes the game and sets the Hero in the middle of the console
        {
            Hero = new Creature()
            {
                X = Console.WindowWidth/3,
                Y = Console.WindowHeight - 4,
                symbol = '@',
                color = ConsoleColor.Black,
                points = 0
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
        enum FlyTypes
        {
            tinyFly = 0,
            HorseFly,
            Cece
        }
        static void Main(string[] args) 
        {
            InitConsole();
            InitGame();
            int playerFieldWidth = 60;

            Random randomGenerator = new Random();
            List<Creature> flies = new List<Creature>();
            while (true)
            {
                Creature newRandomFly = new Creature();
                newRandomFly.X = randomGenerator.Next(1, playerFieldWidth);
                newRandomFly.Y = 1;
                newRandomFly.symbol = '%';
                int flyType = randomGenerator.Next(0, 3);
                switch (flyType)
                {
                    case 0:
                        newRandomFly.symbol = '%';
                        newRandomFly.color = ConsoleColor.Red;
                        newRandomFly.points = 10;
                        break;
                    case 1:
                        newRandomFly.symbol = '$';
                        newRandomFly.color = ConsoleColor.Blue;
                        newRandomFly.points = 20;
                        break;
                    case 2:
                        newRandomFly.symbol = '#';
                        newRandomFly.color = ConsoleColor.DarkGreen;
                        newRandomFly.points = 10;
                        break;
                    default:
                        break;
                }

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

                List<Creature> newFlies = new List<Creature>();
                for (int i = 0; i < flies.Count; i++)
                {
                    Creature oldFly = flies[i];
                    Creature newFly = new Creature();
                    newFly.X = oldFly.X;
                    newFly.Y = oldFly.Y + 1;
                    newFly.color = oldFly.color;
                    newFly.symbol = oldFly.symbol;
                    newFly.points = oldFly.points;
                    if (newFly.Y < Console.WindowHeight - 3)
                    {
                        newFlies.Add(newFly);
                    }
                }
                flies = newFlies;

                Console.Clear();
                PrintOnPosition(Hero.X, Hero.Y, Hero.symbol, Hero.color);
                foreach (Creature fly in flies)
                {
                    PrintOnPosition(fly.X, fly.Y, fly.symbol, fly.color);
                }
                // DrawGrid();
                PrintStringOnPosition(65, 5, "Tiny Fly: 10 points", ConsoleColor.Red);
                PrintStringOnPosition(65, 6, "Horse Fly: 20 points", ConsoleColor.Blue);
                PrintStringOnPosition(65, 7, "Fly Cece: 30 points", ConsoleColor.DarkGreen);
                PrintStringOnPosition(65, 10, "Your points: " + Hero.points, ConsoleColor.Black);

                Thread.Sleep(200);
            }
        }

    }
}
