using System;
using System.Collections.Generic;

namespace MerchantGame
{
    struct Value
    {
        public int amount;
        public float buyPrice;
        public float sellPrice;
    }

    struct Town
    {
        public string name;
        public Dictionary<string, Value> inventory;

        public Town (string theTownName)
        {
            name = theTownName;
            inventory = new Dictionary<string, Value>();
        }
    }

    class MechantGameMain 
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("\nHowdy!");

            Town Pittsburgh = new Town("Pittsburgh");
            Pittsburgh.inventory.Add("steel", new Value {amount = 10, buyPrice = 2.0f, sellPrice = 1.0f});

            SlowType("We've just arrived at a town.");
            SlowType(Pittsburgh.name + "\'s the name.");
            
            foreach(KeyValuePair<string, Value> entry in Pittsburgh.inventory)
            {

                SlowType("I see this town has " + entry.Value.amount + " " + entry.Key + ".");
                SlowType("I reckon we could sell it for about $" + entry.Value.sellPrice);
                SlowType("..but it's going to be $" + entry.Value.buyPrice + " to buy it.");

            }
            
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void SlowType(string output)
        {
            Console.WriteLine(output);
            System.Threading.Thread.Sleep(3000);
        }
    }
}

