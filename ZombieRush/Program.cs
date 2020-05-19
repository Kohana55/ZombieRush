using System;
using System.Collections.Generic;
using System.Threading;

namespace ZombieRush
{
    class Program
    {
        static void Main(string[] args)
        {
            // The Graphics Controll Manager will manage our scene
            // and render it in console
            ConsoleGraphicsManager cgm = new ConsoleGraphicsManager();
            Scoreboard scoreboard = new Scoreboard(); 

            bool exit = false;
            while (exit == false)
            {
                // Player, Monster and Items make up our game  
                bool gameOver = false;
                Player player = new Player(gameOver);
                Thread playerThread = new Thread(player.Move);
                List<Monster> monsters = new List<Monster>();
                MonsterControl monsterControl = new MonsterControl(monsters, gameOver);
                Thread monsterControlThread = new Thread(monsterControl.Run);
                List<Item> items = new List<Item>();
                Timer timer = new Timer();
                Thread timerThread = new Thread(timer.Run);

                // Create a map and populate it with our player
                // monsters and items
                Map map = new Map(player, monsters, items, timer, gameOver);               
                map.Update();

                // Initial drawing of gamescene
                cgm.UpdateScene(player, map, scoreboard, timer);
                cgm.DrawScene();

                monsterControlThread.Start();
                playerThread.Start();
                timerThread.Start();
                // ---GAME LOOP---                      
                while (true)
                {
                    map.Update();

                    if (player.dead == true || timer.TimeRemaining == 0)
                    {
                        monsterControl.gameOver = true;
                        player.gameOver = true;
                        break;
                    }

                    // Items respawn
                    if (items.Count == 0)
                    {
                        map.SpawnItems();
                        map.Update();
                    }

                    // Update and Draw the scene
                    cgm.UpdateScene(player, map, scoreboard, timer);
                    cgm.DrawScene();

                    // Just so we don't hammer the CPU
                    Thread.Sleep(1);
                } // End GAME LOOP

                // Update and Draw scene so player can see where collision happened
                cgm.UpdateScene(player, map, scoreboard, timer);
                cgm.DrawScene();

                Console.WriteLine("\n!!!!GOOD EFFORT!!!!");
                Console.WriteLine("You collected {0} supplies over {1} days", player.score, player.moves);

                // Add to scoreboard if player qualifies
                if (scoreboard.Qualify(player))
                    scoreboard.Add(player);

                // Update and Draw screen once more to display
                // possible leaderboard changes
                cgm.UpdateScene(player, map, scoreboard, timer);
                cgm.DrawScene();

                // Check if user wishes to quit the game
                Console.Write("Any key to play again, 'n' to quit: ");
                string choice = Console.ReadLine();
                if (choice == "n")
                    break;
                Console.Clear();
            }
        }
    }
}
