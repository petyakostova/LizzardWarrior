using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Windows.Media;
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
        public string rowFirst { get; set; }
        public string rowSecond { get; set; }
        public string rowThird { get; set; }
        public int lives { get; set; }
        public int fliesEaten { get; set; }
        public bool motionChange { get; set; }
    }

    class MainGame
    {
        public const string FileNameScores = @"..\..\Scores\Scores.txt";
        public const string FileNameSortedScores = @"..\..\Scores\SortedScores.txt";
        static string currentDir = Environment.CurrentDirectory;
        public static Creature Hero { get; set; }
        const int fieldWidthStart = 1;
        const int fieldWidthEnd = 97;
        public static bool AlreadyStarted = false;
        public static ConsoleColor lizardColor = ConsoleColor.Black;
        public static ConsoleColor backroundColor = ConsoleColor.White;


        public static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);

        }

        public static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.Black)
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
                Y = Console.WindowHeight - 6,
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
                //Console.Clear();
                PrintStringOnPosition(newHero.X - 3, newHero.Y, @"\_(\_\", lizardColor);
                PrintStringOnPosition(newHero.X - 3, newHero.Y + 1, @"   \\_", lizardColor);
                PrintStringOnPosition(newHero.X - 3, newHero.Y + 2, @"  <`\\>", lizardColor);
                PrintStringOnPosition(newHero.X - 3, newHero.Y + 3, @"     ))", lizardColor);
                PrintStringOnPosition(newHero.X - 3, newHero.Y + 4, @"     ( ", lizardColor);

                Hero = newHero;
            }
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
            for (int j = 1; j < Console.WindowHeight - 1; j++)
            {
                Console.SetCursorPosition(fieldWidthEnd + 2, j);
                Console.Write("|");
            }
        }

        /// Set Console width and height, set Console background color and make cursor invisible
        public static void InitConsole(ConsoleColor color = ConsoleColor.White, ConsoleColor fColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 126;
            Console.BackgroundColor = color;
            Console.ForegroundColor = fColor;
            Console.CursorVisible = false;
        }
        static bool IsDead(Creature c, System.Windows.Media.MediaPlayer sound, System.Windows.Media.MediaPlayer soundGameOver)
        {
            if (c.lives > 0 && c.points >= 0)
            {
                return false;
            }
            else
            {
                sound.Stop();
                soundGameOver.Open(new Uri(currentDir + @"\MainGame\Sounds\smb_gameover.wav"));
                soundGameOver.Play();
                return true;
            }
        }
        static void PrintInfo(Creature c)
        {
            PrintStringOnPosition(fieldWidthEnd + 6, 5, "Tiny Fly: 10 points", ConsoleColor.Red);
            PrintStringOnPosition(fieldWidthEnd + 6, 6, "Horse Fly: 20 points", ConsoleColor.Blue);
            PrintStringOnPosition(fieldWidthEnd + 6, 7, "Fly Cece: 30 points", ConsoleColor.DarkGreen);
            PrintStringOnPosition(fieldWidthEnd + 6, 8, "Brick: -100 points", ConsoleColor.DarkRed);
            PrintStringOnPosition(fieldWidthEnd + 6, 10, "Your points: " + c.points, ConsoleColor.Black);
            PrintStringOnPosition(fieldWidthEnd + 6, 11, "Your lives: ", ConsoleColor.Black);
            for (int i = fieldWidthEnd + 18; i < c.lives + fieldWidthEnd + 18; i++)
            {
                PrintOnPosition(i, 11, '\u2665', ConsoleColor.Red);
            }
        }
        static Creature AddRandomFly(Random r, int[] p)
        {
            Creature newRandomFly = new Creature();
            newRandomFly.X = p[r.Next(0, 7)];
            newRandomFly.Y = 1;
            newRandomFly.symbol = '%';
            newRandomFly.rowFirst = null;
            newRandomFly.rowSecond = null;
            newRandomFly.rowThird = null;
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
                    newRandomFly.symbol = '[';
                    newRandomFly.color = ConsoleColor.DarkRed;
                    newRandomFly.points = -100;
                    break;
                default:
                    break;
            }
            return newRandomFly;
        }

        //Main
        public static void Main()
        {
            Console.Title = "The Hungry Lizard";
            
            InitConsole();
            int level = 0;
            if (!AlreadyStarted)
            {
                level = StartScreen.DrawScreen();
                AlreadyStarted = true;
            }

            InitConsole(backroundColor);
            InitGame();

            int[] positions = { 2, 17, 32, 47, 62, 77, 92 };

            int loopCounter = 0;
            int randomLoop = 10;
            int speed = 180 + level;

            Creature newHero = new Creature()
            {
                points = 0,
                lives = 5,
                fliesEaten = 0
            };

            //initiate the sound efects
            
            var music = new System.Windows.Media.MediaPlayer();
            var burpSound = new System.Windows.Media.MediaPlayer();
            var hitSound = new System.Windows.Media.MediaPlayer();
            var gameOverSound = new System.Windows.Media.MediaPlayer();

            music.Open(new Uri(currentDir + @"\MainGame\Sounds\dungeon.wav"));
            music.Play();
            music.Volume = 0.2;            
            
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
                    newFly.rowFirst = oldFly.rowFirst;
                    newFly.rowSecond = oldFly.rowSecond;
                    newFly.points = oldFly.points;
                    newFly.motionChange = oldFly.motionChange;
                    if (newFly.Y < Console.WindowHeight - 1)
                    {
                        newFlies.Add(newFly);
                    }
                }
                flies = newFlies;

                Console.Clear();//Clears the console
                //DrawGrid();
                MoveHero(0);//Draws hero on its position

                List<Creature> aliveFlies = new List<Creature>();

                foreach (Creature fly in flies)//Prints flies and checks for collision
                {
                    //collision with lizard
                    if ((fly.Y + 2 == Console.WindowHeight - 7 || fly.Y + 2 == Console.WindowHeight - 6) && (fly.X + 1 >= Hero.X - 3 && fly.X + 1 <= Hero.X + 3) && fly.symbol == '[')
                    {
                        newHero.points += fly.points;
                        //Smash head animation"
                        PrintOnPosition(Hero.X, Hero.Y, '|', lizardColor);
                        PrintOnPosition(Hero.X + 1, Hero.Y, '/', lizardColor);
                        PrintOnPosition(Hero.X - 1, Hero.Y, '\\', lizardColor);
                        PrintOnPosition(Hero.X - 2, Hero.Y, '_', lizardColor);
                        PrintOnPosition(Hero.X - 3, Hero.Y, '_', lizardColor);
                        PrintOnPosition(Hero.X + 2, Hero.Y, '_', lizardColor);
                        PrintOnPosition(Hero.X + 3, Hero.Y, '_', lizardColor);
                        PrintOnPosition(Hero.X - 1, Hero.Y + 2, '/', lizardColor);
                        PrintOnPosition(Hero.X + 3, Hero.Y + 2, '\\', lizardColor);
                        PrintOnPosition(Hero.X + 4, Hero.Y + 3, '\\', lizardColor);
                        PrintOnPosition(Hero.X - 2, Hero.Y + 3, '/', lizardColor);

                        hitSound.Open(new Uri(currentDir + @"\MainGame\Sounds\Hit.wav"));
                        hitSound.Play();
                    }
                    else if ((fly.Y + 1 == Console.WindowHeight - 7 || fly.Y + 1 == Console.WindowHeight - 6) && (fly.X + 1 >= Hero.X - 3 && fly.X + 1 <= Hero.X + 3) && fly.symbol == '*')
                    {
                        newHero.points += fly.points;
                        //Eating "animation"
                        PrintOnPosition(Hero.X - 1, Hero.Y - 1, '\\', lizardColor);
                        PrintOnPosition(Hero.X, Hero.Y - 1, '/', lizardColor);
                        PrintOnPosition(Hero.X, Hero.Y, ' ', lizardColor);
                        PrintOnPosition(Hero.X + 1, Hero.Y, ')', lizardColor);
                        PrintOnPosition(Hero.X - 1, Hero.Y, ' ', lizardColor);
                        PrintOnPosition(Hero.X - 2, Hero.Y, '(', lizardColor);

                        burpSound.Open(new Uri(currentDir + @"\MainGame\Sounds\burp.wav"));
                        burpSound.Play();
                        newHero.fliesEaten++;
                    }

                        //collision with floor
                    else if (fly.Y + 1 == Console.WindowHeight - 1 || fly.Y + 2 == Console.WindowHeight - 1)
                    {
                        if (fly.symbol == '[')
                        {
                            PrintOnPosition(fly.X, fly.Y + 1, 'V', ConsoleColor.Green);
                        }
                        else
                        {
                            newHero.lives--;
                            PrintStringOnPosition(fly.X, fly.Y + 1, " X ", ConsoleColor.Red);
                            PrintStringOnPosition(fly.X, fly.Y, "XXX", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (fly.symbol == '[')
                        {
                            fly.rowThird = @"|_|";
                            fly.rowSecond = @"| |";
                            fly.rowFirst = @" _ ";
                            PrintStringOnPosition(fly.X, fly.Y + 2, fly.rowThird, fly.color);
                        }
                        else
                        {
                            if (fly.motionChange == true)
                            {
                                fly.rowSecond = @"/|\";
                                fly.rowFirst = @" "" ";
                            }
                            else
                            {
                                fly.rowFirst = @"\""/";
                                fly.rowSecond = @" | ";
                            }
                        }
                        PrintStringOnPosition(fly.X, fly.Y + 1, fly.rowSecond, fly.color);
                        PrintStringOnPosition(fly.X, fly.Y, fly.rowFirst, fly.color);

                        aliveFlies.Add(fly);
                        fly.motionChange = !fly.motionChange;
                    }
                }
                flies = aliveFlies;

                PrintInfo(newHero);//Prints some info about the game

                if (IsDead(newHero, music, gameOverSound))//Checks if you have more than 0 lives
                {
                    if (newHero.points<0)
                    {
                        newHero.points = 0;
                    }
                    EndScreen.DrawEnd(newHero.points);

                    
                }

                //DrawGrid();

                //Some levels
                if (newHero.points > 300 && newHero.points < 500)
                {
                    speed = 150 + level;
                }
                else if (newHero.points > 500 && newHero.points < 700)
                {
                    speed = 120 + level;
                }
                else if (newHero.points > 700 && newHero.points < 1000)
                {
                    speed = 100 + level;
                }
                else if (newHero.points > 1000 && newHero.points < 1200)
                {
                    speed = 80 + level;
                }
                else if (newHero.points > 1200 && newHero.points < 1500)
                {
                    speed = 70 + level;
                }
                else if (newHero.points > 1500)//God mode
                {
                    speed = 65 + level;
                    //InitConsole(ConsoleColor.Green);
                }

                Thread.Sleep(speed);
                loopCounter++;

            }
        }
    }
}