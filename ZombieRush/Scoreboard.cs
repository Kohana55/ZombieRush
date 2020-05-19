using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZombieRush
{
    class Scoreboard
    {
        public List<ScoreboardEntry> scoreboard = new List<ScoreboardEntry>();

        public Scoreboard()
        {
            // Load() from file, else provide default scoreboard
            if (Load() == false)
            {
                ScoreboardEntry entry = new ScoreboardEntry
                {
                    name = "AAA",
                    score = 20,
                };
                scoreboard.Add(entry);

                entry = new ScoreboardEntry
                {
                    name = "BBB",
                    score = 10,
                };
                scoreboard.Add(entry);

                entry = new ScoreboardEntry
                {
                    name = "CCC",
                    score = 5,
                };
                scoreboard.Add(entry);
            }
        }

        public bool Qualify(Player player)
        {
            if (scoreboard.Count == 0)
            {
                Console.WriteLine("You have qualified for the scoreboard");
                return true;
            }

            if (player.score >= scoreboard[scoreboard.Count-1].score)
            {
                Console.WriteLine("You have qualified for the scoreboard");
                return true;
            }
            return false;
        }

        public void Add(Player player)
        {
            ScoreboardEntry entry = new ScoreboardEntry();
            Console.Write("Name: ");
            entry.name = Console.ReadLine();
            entry.score = player.score;

            for (int i=0;i<scoreboard.Count;i++)
            {
                if (scoreboard[i].score < entry.score)
                {
                    scoreboard.Insert(i, entry);
                    break;
                }
            }
            scoreboard.RemoveAt(3);
            Save();
        }

        public void Save()
        {
            // Save to file
            StreamWriter sw = new StreamWriter("save.txt");

            for (int i=0;i<scoreboard.Count;i++)
            {
                sw.WriteLine(scoreboard[i].name);
                sw.WriteLine(scoreboard[i].score);
            }
            sw.Close();
        }

        public bool Load()
        {
            // Load from file
            try
            {
                StreamReader sr = new StreamReader("save.txt");

                for (int i = 0; i < 3; i++)
                {
                    scoreboard.Add(new ScoreboardEntry());
                    scoreboard[i].name = sr.ReadLine();
                    scoreboard[i].score = Convert.ToInt32(sr.ReadLine());
                }
                sr.Close();
                return true;
            }
            catch(Exception ex)
            {

            }

            return false;
        }
    }
}
