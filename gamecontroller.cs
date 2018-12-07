using System;
using System.Collections.Generic;

namespace MerchantGame
{
    partial class MerchantGame
    {
        bool stop;
        ulong elapsed;
        DateTime time;
        char input;
        Dictionary<string, Town> cities;

        public void Start()
        {
            cities = new Dictionary<string, Town>();
            AddTestCities();
            time = new DateTime();
            input = '0';

            while(!stop)
            {
                DeltaTime();
                Console.Clear();
                Console.WriteLine("Howdy Boss. Welcome to the shop.");
                Console.WriteLine("a : caravans");
                Console.WriteLine("b : inventory");
                Console.WriteLine("Go ahead and press \'x\' to leave.");

                input = getInput();

                Console.WriteLine(input);

                if (input == 'a')
                {
                    caravanScreen();
                }
                if (input == 'b')
                {
                    inventoryScreen();
                }
                if (input == 'x')
                {
                    stop = true;
                }
                System.Threading.Thread.Sleep(1500);
            }
            
        }

        public void caravanScreen()
        {
            while (input != 'x')
            {
                Console.Clear();
                Console.WriteLine("Here's where we stand with our caravan right now:");
                Console.WriteLine("Press \'x\' to return to the shop");
                input = getInput();
            }
            input = '0';
            return;
        }

        public void inventoryScreen()
        {
            while (input != 'x')
            {
                Town theBase = cities["Base"];
                Console.Clear();
                Console.WriteLine("Here's where we stand with our inventory right now:\n");
                foreach(KeyValuePair<string,int> entry in theBase.inventory)
                {
                    Console.WriteLine(UppercaseFirst(entry.Key));
                    Console.WriteLine("Amt: " + entry.Value 
                        + " Buy Price: " + theBase.prices[entry.Key].buyPrice 
                        + " Sell Price: " + theBase.prices[entry.Key].sellPrice + "\n");
                }
                Console.WriteLine("Press \'x\' to return to the shop");
                input = getInput();
            }
            input = '0';
            return;
        }

        public void DeltaTime()
        {
            elapsed += (ulong)DateTime.Now.Subtract(time).Seconds;
            time = DateTime.Now;
        }

        public char getInput()
        {
            return Console.ReadKey(true).KeyChar;
        }

        class Town
        {
            public Dictionary<string, int> inventory;
            public Dictionary<string, Pricing> prices;

            public Town ()
            {
                inventory = new Dictionary<string, int>();
                prices = new Dictionary<string, Pricing>();
            }
            public void AddItem(string itemName, int amount)
            {
                inventory.Add(itemName, amount);
                prices.Add(itemName, new Pricing {buyPrice = 0, sellPrice = 0});
            }
            public void AddItem(string itemName, int amount, float buyFrom, float sellTo)
            {
                inventory.Add(itemName, amount);
                prices.Add(itemName, new Pricing {buyPrice = buyFrom, sellPrice = sellTo});
            }
        }

        struct Pricing
        {
            public float buyPrice;
            public float sellPrice;
        }

        void AddTestCities()
        {
            Town Base = new Town();
            Base.AddItem("food", 5);
            Base.AddItem("water", 3);
            Base.AddItem("guns", 1);
            cities.Add("Base", Base);

            Town Pittsburgh = new Town();
            Pittsburgh.AddItem("steel", 10, 20.0f, 10.0f);
            Pittsburgh.AddItem("food", 5, 10.0f, 5.0f);
            Pittsburgh.AddItem("water", 7, 5.0f, 2.0f);
            Pittsburgh.AddItem("guns", 10, 75.0f, 25.0f);
            cities.Add("Pittsburgh", Pittsburgh);

            Town Minneapolis = new Town();
            Minneapolis.AddItem("guns", 3, 50.0f, 20.0f);
            cities.Add("Minneapolis", Minneapolis);

            Town Omaha = new Town();
            Omaha.AddItem("food", 50, 2.0f, 1.0f);
            cities.Add("Omaha", Omaha);
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

}
