using System;
using System.Collections.Generic;

namespace MerchantGame
{
    partial class MerchantGame
    {
        bool stop;
        ulong elapsed;
        DateTime time;
        public void start()
        {
            time = new DateTime();

            while(!stop)
            {
                deltaTime();
                Console.WriteLine(elapsed);
                System.Threading.Thread.Sleep(1000);

                if (elapsed > 50)
                {
                    stop = true;
                }
            }
            
        }

        public void deltaTime()
        {
            elapsed += (ulong)DateTime.Now.Subtract(time).Seconds;
            time = DateTime.Now;
        }

    }
}