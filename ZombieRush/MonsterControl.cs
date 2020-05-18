using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZombieRush
{
    class MonsterControl
    {
        List<Monster> monsters = new List<Monster>();
        public bool gameOver = false;

        public MonsterControl(List<Monster> _monsters, bool gamO)
        {
            gameOver = gamO;
            monsters = _monsters;
        }

        public void Run()
        {
            while(gameOver == false)
            {
                Thread.Sleep(750);
                for(int i = 0; i<monsters.Count;i++)
                {
                    monsters[i].Move();
                }
            }
        }

    }
}
