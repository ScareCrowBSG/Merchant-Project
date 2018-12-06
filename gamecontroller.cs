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
            char input = '0';

            while(!stop)
            {
                deltaTime();
                Console.Clear();
                Console.WriteLine("Howdy Boss. We've been up: " + elapsed + " seconds.");
                Console.WriteLine("Go ahead and press \'x\' to leave.");

                if (Console.KeyAvailable)
                {
                    input = getInput();
                }
                
                Console.WriteLine(input);
                if (input == 'x')
                {
                    stop = true;
                }
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        public void deltaTime()
        {
            elapsed += (ulong)DateTime.Now.Subtract(time).Seconds;
            time = DateTime.Now;
        }

        public char getInput()
        {
            return Console.ReadKey(true).KeyChar;
        }

    }
}