using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaffeluDemo
{
    class Program
    {
        const ConsoleColor HERO_COLOR = ConsoleColor.Gray;
        const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Gray;

        public static Coordinate Hero { get; set; } //Will represent our here that's moving around :P/>

        static void Main(string[] args)
        {
            
            InitGame();

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        MoveHero(Console.WindowWidth/10, 0);
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveHero(-(Console.WindowWidth/10), 0);
                        break;
                        
                }

            }
        }

        /// <summary>
        /// Paint the new hero
        /// </summary>
        static void MoveHero(int x, int y)
        {
            Coordinate newHero = new Coordinate()
            {
                X = Hero.X + x,
                Y = Hero.Y + y
            };

            if (CanMove(newHero))
            {
                
                RemoveHero();
                Console.Clear();
                Console.BackgroundColor = HERO_COLOR;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(newHero.X, newHero.Y);
                Console.Write(@"  __  ,          ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 1);
                Console.Write(@" (' \ \          ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 2);
                Console.Write(@"  \ \\/          ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 3);
                Console.Write(@"'\/\ \\          ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 4);
                Console.Write(@"    \ \\/\,      ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 5);
                Console.Write(@"     \ \\        ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 6);
                Console.Write(@"     /\ \\       ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 7);
                Console.Write(@"     \ `\\\      ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 8);
                Console.Write(@"     '   `\\     ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 9);
                Console.Write(@"           \\    ");
                Console.SetCursorPosition(newHero.X, newHero.Y + 10);
                Console.Write(@"            `---'");

                Hero = newHero;
            }
        }

        /// <summary>
        /// Overpaint the old hero
        /// </summary>
        static void RemoveHero()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the
        /// console window (since that will cause a runtime crash
        /// </summary>
        static bool CanMove(Coordinate c)
        {
            if (c.X < 0 || c.X >= Console.WindowWidth-16)
                return false;

            if (c.Y < 0 || c.Y >= Console.WindowHeight)
                return false;

            return true;
        }

        /// <summary>
        /// Paint a background color
        /// </summary>
        /// <remarks>
        /// It is very important that you run the Clear() method after
        /// changing the background color since this causes a repaint of the background
        /// </remarks>
        static void SetBackgroundColor()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.Clear(); //Important!
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the hero
        /// </summary>
        static void InitGame()
        {
            SetBackgroundColor();

            Hero = new Coordinate()
            {
                X = Console.WindowWidth/2,
                Y = Console.WindowHeight -1
            };

            MoveHero(0, 0);

        }
   
    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
    }
    
}
