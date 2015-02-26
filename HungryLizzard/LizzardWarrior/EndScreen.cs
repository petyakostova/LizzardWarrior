using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class EndScreen
{
    public static void DrawEnd(HungryLizard.Creature c)
    {
        HungryLizard.MainGame.InitConsole();
        string end = @"
                                 _____          __  __ ______    ______      ________ _____  _ 
                                / ____|   /\   |  \/  |  ____|  / __ \ \    / /  ____|  __ \| |
                               | |  __   /  \  | \  / | |__    | |  | \ \  / /| |__  | |__) | |
                               | | |_ | / /\ \ | |\/| |  __|   | |  | |\ \/ / |  __| |  _  /| |
                               | |__| |/ ____ \| |  | | |____  | |__| | \  /  | |____| | \ \|_|
                                \_____/_/    \_\_|  |_|______|  \____/   \/   |______|_|  \_(_)";
        HungryLizard.MainGame.PrintStringOnPosition(0, 15, end);
        Console.CursorVisible = true;
        string enterName = "Enter your name: ";
        string pressKey = "Press ENTER to play again, ESC to exit!";;
        HungryLizard.MainGame.PrintStringOnPosition((Console.WindowWidth / 2) - (enterName.Length / 2), Console.WindowHeight / 2 + 5, enterName);
        string name = Console.ReadLine();
        Console.CursorVisible = false;
        HungryLizard.MainGame.PrintStringOnPosition((Console.WindowWidth / 2) - (pressKey.Length / 2), Console.WindowHeight / 2 + 6, pressKey);
        WriteInFile(c, name);
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Enter)
        {
            HungryLizard.MainGame.Main();
        }
        else if (key.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
    }
    public static void WriteInFile(HungryLizard.Creature c, string name)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(HungryLizard.MainGame.FileNameScores, true))
        {
            file.WriteLine("{0} - {1}", name, c.points);
        }
    }
}

