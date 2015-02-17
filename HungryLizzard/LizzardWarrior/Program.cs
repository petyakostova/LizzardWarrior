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
        public int lives { get; set; }
    }

    class Program
    {
        public static Creature Hero { get; set; }
        const int fieldWidthStart = 1;
        const int fieldWidthEnd = 61;


        static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);

        }

        static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static void InitGame()//initializes the game and sets the Hero in the middle of the console
        {
            Hero = new Creature()
            {
                X = ((fieldWidthEnd - fieldWidthStart) / 2) + 1,
                Y = Console.WindowHeight - 4,
                symbol = '@',
                color = ConsoleColor.Black,
                points = 0
            };
            MoveHero(0);
        }

        static void MoveHero(int a)//moves the Hero, where a is number of positions its moved
        {
            Creature newHero = new Creature
            {
                X = Hero.X + a,
                Y = Hero.Y
            };
            if (CanMove(newHero))
            {
                //RemoveHero();
                Console.Clear();
                PrintOnPosition(newHero.X, newHero.Y, '@', ConsoleColor.Black);
                Hero = newHero;
            }

        }

        static void RemoveHero()
        {

            //Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.Write(" ");
        }

        static bool CanMove(Coordinate c)//checks if Hero is not out of borders of console
        {
            if (c.X >= fieldWidthStart && c.X <= fieldWidthEnd)
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
            Console.ForegroundColor = ConsoleColor.Black;


            Console.SetCursorPosition(0, 0);
            Console.Write(new string('=', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight-3);
            Console.Write(new string('=', Console.WindowWidth));

            for (int j = 1; j < Console.WindowHeight - 3; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("|");
                Console.SetCursorPosition(Console.WindowWidth - 1, j);
                Console.Write("|");
                Console.SetCursorPosition(62, j);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("+");
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write("+");
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("+");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 3);
            Console.Write("+");
            Console.SetCursorPosition(62, 0);
            Console.Write("+");
            Console.SetCursorPosition(62, Console.WindowHeight - 3);
            Console.Write("+");
        }

        /// Set Console width and height, set Console background color and make cursor invisible
        static void InitConsole()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 90;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.CursorVisible = false;
        }
        static bool IsDead(Creature c)
        {
            if (c.lives>0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        //Main
        static void Main()
        {
            InitConsole();
            InitGame();

            int[] possitions = { 1, 16, 31, 46, 61 };

            int counter = 0;

            Creature newHero = new Creature()
            {
                points = 0,
                lives = 5
            };

            Random randomGenerator = new Random();
            List<Creature> flies = new List<Creature>();
            while (true)
            {
                if (counter == 9)
                {
                    Creature newRandomFly = new Creature();
                    newRandomFly.X = possitions[randomGenerator.Next(0, 5)];
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
                            newRandomFly.points = 30;
                            break;
                        default:
                            break;
                    }

                    flies.Add(newRandomFly);
                    counter = 0;
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        MoveHero(-15);
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        MoveHero(15);
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
                
                MoveHero(0);
                foreach (Creature fly in flies)
                {

                    if (fly.Y == Console.WindowHeight - 4 && fly.X == Hero.X)
                    {
                        newHero.points += fly.points;
                    }
                    else if (fly.Y == Console.WindowHeight - 4 && fly.X != Hero.X)
                    {
                        newHero.lives--;
                    }
                    PrintOnPosition(fly.X, fly.Y, fly.symbol, fly.color);
                }
                
                PrintStringOnPosition(65, 5, "Tiny Fly: 10 points", ConsoleColor.Red);
                PrintStringOnPosition(65, 6, "Horse Fly: 20 points", ConsoleColor.Blue);
                PrintStringOnPosition(65, 7, "Fly Cece: 30 points", ConsoleColor.DarkGreen);
                PrintStringOnPosition(65, 10, "Your points: " + newHero.points, ConsoleColor.Black);
                PrintStringOnPosition(65, 11, "Your lives: ", ConsoleColor.Black);
                for (int i = 77; i < newHero.lives+77; i++)
                {
                    PrintOnPosition(i, 11, '\u2665', ConsoleColor.Red);
                }
                if (IsDead(newHero))
                {
                    Console.Clear();
                    PrintStringOnPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2, "Game Over!");
                    PrintStringOnPosition(Console.WindowWidth / 2 - 8, (Console.WindowHeight / 2) + 1, "Your score was: " + newHero.points);
                    PrintStringOnPosition(Console.WindowWidth / 2 - 10, (Console.WindowHeight / 2) + 2, "Press any key to exit");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Main();
                    }
                    else
                    {
                        break;
                    }                                  
                }
                DrawGrid();
                Thread.Sleep(50);
                counter++;
            }
        }

    }
}