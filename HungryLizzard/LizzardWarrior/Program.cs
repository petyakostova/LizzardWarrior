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
    class Program
    {
        public static Coordinate Hero { get; set; }

        static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);

        } 
        
        static void InitGame()//initializes the game and sets the Hero in the middle of the console
        {
            

            Hero = new Coordinate()
            {
                X = Console.WindowWidth/2,
                Y = Console.WindowHeight -1
            };
            MoveHero(0);

        }

        static void MoveHero(int a)//moves the Hero, where a is number of positions its moved
        {
            Coordinate newHero = new Coordinate
            {
                X = Hero.X + a,
                Y = Console.WindowHeight - 4
            };
            if (CanMove(newHero))
            {               
                RemoveHero();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(newHero.X, newHero.Y-1);
                Console.Write("\\  /");
                Console.SetCursorPosition(newHero.X, newHero.Y);
                Console.Write("****");
                Hero = newHero;
            }
            
        }

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
            Console.BackgroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
        }
        //Main
        static void Main(string[] args) 
        {
            InitConsole();
            InitGame();

            while (true)
            {
                
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

                DrawGrid();
                Thread.Sleep(100);
            }
        }

    }
}
