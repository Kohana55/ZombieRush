using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieRush
{
    class ConsoleGraphicsManager
    {
        string sceneBuffer = "";


        public void DrawScene()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.WriteLine(sceneBuffer);
        }

        public void UpdateScene(Player player, Map map, Scoreboard scoreboard)
        {
            int boardPosition = 0;

            sceneBuffer = "";
            sceneBuffer += "\t\tZOMBIE RUSH\n\n";
            sceneBuffer += "\tSUPPLIES: " + player.score + "\tDAYS: "+ player.moves +"\n\n";
            sceneBuffer += "Remaining: ";
            for (int i = 0; i < map.items.Count; i++) sceneBuffer += "* ";
            sceneBuffer += "   ";
            sceneBuffer += "\n\n";

            // Draw north wall
            for (int j = 0; j < map.BoardSize; j++)
            {
                sceneBuffer += "___";
            }
            sceneBuffer += "\n";

            for (int i = 0; i < map.BoardSize; i++)
            {
                // Draw Map
                sceneBuffer += "|";
                for (int j = 0; j < map.BoardSize; j++)
                {
                    sceneBuffer += map.board[i, j].tile;
                }
                sceneBuffer += "|";

                // Draw Scoreboard
                if (i==0)
                {
                    sceneBuffer += "\tSCOREBOARD";
                }
                if (i >= 1 && i <= 6)
                {
                    if (i % 2 == 0)
                    {
                        sceneBuffer += "\tSupplies: " + scoreboard.scoreboard[boardPosition].score +
                                        " | Days: " + scoreboard.scoreboard[boardPosition].turns;
                        boardPosition++;
                    }
                    else
                    {
                        sceneBuffer += "\t" + scoreboard.scoreboard[boardPosition].name + "          ";
                    }
                }

                sceneBuffer += "\n";
            }
            // Draw north wall
            for (int j = 0; j < map.BoardSize; j++)
            {
                sceneBuffer += "---";
            }
        }

    }
}
