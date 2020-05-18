using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZombieRush
{
    class ConsoleGraphicsManager
    {
        string sceneBuffer = "";

        // To enable ANSI encoding
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        /// <summary>
        /// Constructor sets up the console on Windows 
        /// to allow for ANSI encoding support
        /// NOTE: ANSI output already supported on other platforms
        /// </summary>
        public ConsoleGraphicsManager()
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            GetConsoleMode(iStdOut, out uint outConsoleMode);


            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            SetConsoleMode(iStdOut, outConsoleMode);
        }



        public void DrawScene()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.WriteLine(sceneBuffer);
        }

        public void UpdateScene(Player player, Map map, Scoreboard scoreboard)
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            GetConsoleMode(iStdOut, out uint outConsoleMode);

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            SetConsoleMode(iStdOut, outConsoleMode);
            int boardPosition = 0;

            sceneBuffer = "";
            sceneBuffer += "\t\tZOMBIE RUSH\r\n\r\n";
            sceneBuffer += "\tSUPPLIES: " + player.score + "\tDAYS: "+ player.moves +"\r\n\r\n";
            sceneBuffer += "Remaining: ";
            for (int i = 0; i < map.items.Count; i++) sceneBuffer += "* ";
            sceneBuffer += "   ";
            sceneBuffer += "\r\n\r\n";

            // Draw north wall
            for (int j = 0; j < map.BoardSize; j++)
            {
                sceneBuffer += "___";
            }
            sceneBuffer += "\r\n";

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

                sceneBuffer += "\r\n";
            }
            // Draw north wall
            for (int j = 0; j < map.BoardSize; j++)
            {
                sceneBuffer += "---";
            }
        }

    }
}
