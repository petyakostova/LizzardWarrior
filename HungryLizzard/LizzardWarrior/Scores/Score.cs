using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Score
{
    public static string highestScore = string.Empty;
    public static void SortScores()
    {


        Dictionary<string, int> scores = new Dictionary<string, int>();
        using (StreamReader file = new StreamReader(HungryLizard.MainGame.FileNameScores, true))
        {
            string line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                string[] currLine = line.Split(' ');
                scores.Add(currLine[0] + " " +currLine[1], int.Parse(currLine[2]));
            }
        }
            
        var sortedScores = from entry in scores orderby entry.Value descending select entry;


        if (File.Exists(HungryLizard.MainGame.FileNameSortedScores))
        {
            File.Delete(HungryLizard.MainGame.FileNameSortedScores);
        }
        using (StreamWriter file = new StreamWriter(HungryLizard.MainGame.FileNameSortedScores, true))
        {
            
            foreach (var pair in sortedScores)
            {
                file.WriteLine("{0} - {1}", pair.Key.Split(' ').GetValue(0), pair.Value);
            }
        }
        using (StreamReader file = new StreamReader(HungryLizard.MainGame.FileNameSortedScores, true))
        {

            highestScore = file.ReadLine();
        }
    }
}

