using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

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
        public int fliesEaten { get; set; }
    }

    class Program
    {
        private const string FileNameStart = @"..\..\StartScreen.txt";
        private const string FileNameEnd = @"..\..\EndScreen.txt";
        private const string FileNameScores = @"..\..\Scores.txt";
        public static Creature Hero { get; set; }
        const int fieldWidthStart = 1;
        const int fieldWidthEnd = 61;
        public static bool AlreadyStarted = false;


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

        /// Loading screen
        static void StartScreen()
        {
            AlreadyStarted = true;
            using (StreamReader reader = new StreamReader(FileNameStart))
            {
                string text = reader.ReadToEnd();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(text);
                Console.WriteLine("\n");

                for (int i = 0; i < 90; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write('|');
                    Thread.Sleep(20);
                }
            }
        }

        static void EndScreen(Creature c)
        {
            using (StreamReader reader = new StreamReader(FileNameEnd))
            {
                string text = reader.ReadToEnd();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("{0}{1}", text, c.points);
            }
            Console.CursorVisible = true;
            string enterName = "Enter your name: ";           
            string pressKey = "Press ENTER to play again, ESC to exit!";
            PrintStringOnPosition((Console.WindowWidth/2) - (enterName.Length / 2) , Console.WindowHeight / 2 + 5, enterName);
            string name = Console.ReadLine();
            Console.CursorVisible = false;
            PrintStringOnPosition((Console.WindowWidth / 2) - (pressKey.Length / 2), Console.WindowHeight / 2 + 6, pressKey);
            WriteHighScore(c, name);
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                Main();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        static void InitGame()//initializes the game and sets the Hero in the middle of the console
        {
            Hero = new Creature()
            {
                X = ((fieldWidthEnd - fieldWidthStart) / 2) + 1,
                Y = Console.WindowHeight - 1,
                color = ConsoleColor.Black,
                points = 0
            };
            MoveHero(0);
        }

        static void WriteHighScore(Creature c, string s)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileNameScores, true))
            {
                file.WriteLine("{0} - {1}", s, c.points);
            }
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
                //Console.Clear();
                PrintStringOnPosition(newHero.X - 1, newHero.Y, "_^_", ConsoleColor.Black);
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
            for (int j = 0; j < Console.WindowHeight; j++)
            {
                Console.SetCursorPosition(fieldWidthEnd+1, j);
                Console.Write("|");
            }

        }

        /// Set Console width and height, set Console background color and make cursor invisible
        static void InitConsole(ConsoleColor color = ConsoleColor.White)
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 90;
            Console.BackgroundColor = color;
            Console.CursorVisible = false;
        }
        static bool IsDead(Creature c)
        {
            if (c.lives > 0 && c.points >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static void PrintInfo(Creature c)
        {
            PrintStringOnPosition(65, 5, "Tiny Fly: 10 points", ConsoleColor.Red);
            PrintStringOnPosition(65, 6, "Horse Fly: 20 points", ConsoleColor.Blue);
            PrintStringOnPosition(65, 7, "Fly Cece: 30 points", ConsoleColor.DarkGreen);
            PrintStringOnPosition(65, 10, "Your points: " + c.points, ConsoleColor.Black);
            PrintStringOnPosition(65, 11, "Your lives: ", ConsoleColor.Black);
            for (int i = 77; i < c.lives + 77; i++)
            {
                PrintOnPosition(i, 11, '\u2665', ConsoleColor.Red);
            }
        }
        static Creature AddRandomFly(Random r, int[] p)
        {
            Creature newRandomFly = new Creature();
            newRandomFly.X = p[r.Next(0, 5)];
            newRandomFly.Y = 1;
            newRandomFly.symbol = '%';
            int flyType = r.Next(0, 4);
            switch (flyType)
            {
                case 0:
                    newRandomFly.symbol = '*';
                    newRandomFly.color = ConsoleColor.Red;
                    newRandomFly.points = 10;
                    break;
                case 1:
                    newRandomFly.symbol = '*';
                    newRandomFly.color = ConsoleColor.Blue;
                    newRandomFly.points = 20;
                    break;
                case 2:
                    newRandomFly.symbol = '*';
                    newRandomFly.color = ConsoleColor.DarkGreen;
                    newRandomFly.points = 30;
                    break;
                case 3:
                    newRandomFly.symbol = 'O';
                    newRandomFly.color = ConsoleColor.Red;
                    newRandomFly.points = -100;
                    break;
                default:
                    break;
            }

            return newRandomFly;
        }

        //Main
        static void Main()
        {
            if (!AlreadyStarted)
            {
                StartScreen();                
            }
            InitConsole();
            InitGame();

            int[] positions = { 1, 16, 31, 46, 61 };

            int loopCounter = 0;
            int randomLoop = 10;
            int speed = 200;

            Creature newHero = new Creature()
            {
                points = 0,
                lives = 5, 
                fliesEaten = 0
            };

            //Creates new flies
            Random randomGenerator = new Random();
            List<Creature> flies = new List<Creature>();
            while (true)
            {

                if (Console.KeyAvailable)//Checks for key pressed
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

                if (loopCounter == randomLoop)
                {
                    flies.Add(AddRandomFly(randomGenerator, positions));
                    randomLoop = randomGenerator.Next(8, 15);
                    loopCounter = 0;
                }
              
                List<Creature> newFlies = new List<Creature>();//Puts Flies into a list of flies
                for (int i = 0; i < flies.Count; i++)
                {
                    Creature oldFly = flies[i];
                    Creature newFly = new Creature();
                    newFly.X = oldFly.X;
                    newFly.Y = oldFly.Y + 1;
                    newFly.color = oldFly.color;
                    newFly.symbol = oldFly.symbol;
                    newFly.points = oldFly.points;
                    if (newFly.Y < Console.WindowHeight)
                    {
                        newFlies.Add(newFly);
                    }

                }
                flies = newFlies;
              
                Console.Clear();//Clears the console
          
                MoveHero(0);//Draws hero on its position

                DrawGrid();
                
                foreach (Creature fly in flies)//Prints flies and checks for collision
                {

                    if (fly.Y == Console.WindowHeight - 1 && fly.X == Hero.X)
                    {
                        newHero.points += fly.points;
                        PrintOnPosition(fly.X, fly.Y, '-', ConsoleColor.Black);//Eating "annimation"
                        newHero.fliesEaten++;
                    }
                    else if (fly.Y == Console.WindowHeight - 1 && fly.X != Hero.X)
                    {
                        newHero.lives--;
                        PrintOnPosition(fly.X, fly.Y, 'X', ConsoleColor.Red);
                    }
                    else PrintOnPosition(fly.X, fly.Y, fly.symbol, fly.color);
                }
   
                PrintInfo(newHero);//Prints some info about the game
                
                if (IsDead(newHero))//Checks if you have more than 0 lives
                {
                    if (newHero.points < 0)
                    {
                        newHero.points = 0;
                    }
                    EndScreen(newHero);
                }

                //DrawGrid();

                //Some levels
                if (newHero.points > 300 && newHero.points < 500)
                {
                    speed = 180;
                }
                else if (newHero.points > 500 && newHero.points < 700)
                {
                    speed = 160;
                }
                else if (newHero.points > 700 && newHero.points < 1000)
                {
                    speed = 130;
                }
                else if (newHero.points > 1000 && newHero.points < 1200)
                {
                    speed = 100;
                }
                else if (newHero.points > 1200 && newHero.points < 1500)
                {
                    speed = 80;
                }
                else if (newHero.points > 1500)//God mode
                {
                    speed = 70;
                    InitConsole(ConsoleColor.Green);
                }

                Thread.Sleep(speed);
                loopCounter++;
            }
        }

    }
}