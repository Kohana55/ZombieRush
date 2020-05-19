using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZombieRush
{
    class Timer
    {

        /// <summary>
        /// 
        /// </summary>
        public int TimeRemaining {
            get
            {
                return timeRemaining;
            }
            set
            {
                timeRemaining = value;
            }
        }
        private int timeRemaining = 30;

        /// <summary>
        /// 
        /// </summary>
        public Timer()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            while(timeRemaining != 0)
            {
                Thread.Sleep(1000);
                timeRemaining -= 1;
            }
        }

    }
}
