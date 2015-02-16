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
        //Main
        static void Main(string[] args) 
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 90;
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


                Thread.Sleep(100);
            }
        }

    }
}
