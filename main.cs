using System;
using System.Collections.Generic;

namespace MerchantGame
{

    struct Pricing
    {
        public int amount;
        public float buyPrice;
        public float sellPrice;
    }

    struct Town
    {
        public string name;
        public Dictionary<string, Pricing> inventory;

        public Town (string theTownName)
        {
            name = theTownName;
            inventory = new Dictionary<string, Pricing>();
        }
    }

    class MerchantGame
    {
        public List<Town> cities;

        void start() 
        {
            cities = new List<Town>();

            AddTestCities();
            


            RunIntegrationTest();
            //we're done here   
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void SlowType(string output)
        {
            Console.WriteLine(output);
            System.Threading.Thread.Sleep(1000);
        }

        void RunIntegrationTest()
        {
            SlowType("\nHowdy!");
            foreach (Town location in cities)
            {
                
                SlowType("We've just arrived at a town.");
                SlowType(location.name + "\'s the name.");
                
                foreach(KeyValuePair<string, Pricing> entry in location.inventory)
                {
                    SlowType("I see this town has " + entry.Value.amount + " " + entry.Key + ".");
                    SlowType("I reckon we could sell for about $" + entry.Value.sellPrice);
                    SlowType("..but it's going to be $" + entry.Value.buyPrice + " to buy it.\n");
                }
            }
        }

        void AddTestCities()
        {
            Town Pittsburgh = new Town("Pittsburgh");
            Pittsburgh.inventory.Add("steel", new Pricing {amount = 10, buyPrice = 20.0f, sellPrice = 10.0f});
            Pittsburgh.inventory.Add("food", new Pricing {amount = 5, buyPrice = 10.0f, sellPrice = 5.0f});
            Pittsburgh.inventory.Add("water", new Pricing {amount = 7, buyPrice = 5.0f, sellPrice = 2.0f});
            Pittsburgh.inventory.Add("guns", new Pricing {amount = 10, buyPrice = 75.0f, sellPrice = 25.0f});
            cities.Add(Pittsburgh);

            Town Minneapolis = new Town("Minneapolis");
            Minneapolis.inventory.Add("guns", new Pricing {amount = 3, buyPrice = 50.0f, sellPrice = 20.0f});
            cities.Add(Minneapolis);

            Town Omaha = new Town("Omaha");
            Omaha.inventory.Add("food", new Pricing {amount = 50, buyPrice = 2.0f, sellPrice = 1.0f});
            cities.Add(Omaha);
            
        }

        class MerchantGameMain
        {
            static void Main(string[] args)
            {
                MerchantGame MG = new MerchantGame();
                MG.start();
            }
        }
    }
}
