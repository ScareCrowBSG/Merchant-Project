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
        Base homeBase;

        //should be moved to a class later
        float mercPrice;

        public void Start()
        {
            cities = new Dictionary<string, Town>();
            AddTestCities();
            time = new DateTime();
            input = '0';
            homeBase = new Base();

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
                Console.Clear();
                Console.WriteLine("Here's what we have on hand right now:\n");
                foreach(KeyValuePair<string,int> entry in homeBase.inventory)
                {
                    if (entry.Value != 0)
                    {
                        Console.WriteLine(entry.Value + " " + UppercaseFirst(entry.Key));
                    }
                }
                Console.WriteLine("\nPress \'x\' to return to the shop");
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

        class Caravan
        {
            public Dictionary<string, int> inventory;
            public string destinationCity;
            public DateTime arrivalDate;
            public int speed;
            public int defense;
            public float price;
        }

        class Base
        {
            public Dictionary<string, int> inventory;
            public List<Caravan> caravans;

            public Base ()
            {
                inventory = new Dictionary<string, int>();
                AddTestInventory();

            }
            private void AddTestInventory()
            {
                inventory.Add("food", 5);
                inventory.Add("water", 3);
                inventory.Add("guns", 1);
                inventory.Add("steel", 0);
                inventory.Add("iron", 0);
                inventory.Add("coal", 0);
                inventory.Add("alcohol", 3);
                inventory.Add("money", 100);
            }

        }

        struct Pricing
        {
            public float buyPrice;
            public float sellPrice;
        }

        void AddTestCities()
        {

            Town Pittsburgh = new Town();
            Pittsburgh.AddItem("food", 5, 10.0f, 5.0f);
            Pittsburgh.AddItem("water", 7, 5.0f, 2.0f);
            Pittsburgh.AddItem("guns", 10, 75.0f, 25.0f);
            Pittsburgh.AddItem("steel", 10, 20.0f, 10.0f);
            Pittsburgh.AddItem("iron", 0, 50.0f, 10.0f);
            Pittsburgh.AddItem("coal", 0, 50.0f, 10.0f);
            Pittsburgh.AddItem("alcohol", 0, 100.0f, 25.0f);
            cities.Add("Pittsburgh", Pittsburgh);

            Town Minneapolis = new Town();
            Minneapolis.AddItem("food", 5, 10.0f, 5.0f);
            Minneapolis.AddItem("water", 7, 5.0f, 2.0f);
            Minneapolis.AddItem("guns", 10, 25.0f, 10.0f);
            Minneapolis.AddItem("steel", 0, 20.0f, 10.0f);
            Minneapolis.AddItem("iron", 10, 20.0f, 5.0f);
            Minneapolis.AddItem("coal", 0, 30.0f, 10.0f);
            Minneapolis.AddItem("alcohol", 0, 100.0f, 25.0f);
            cities.Add("Minneapolis", Minneapolis);

            Town Omaha = new Town();
            Omaha.AddItem("food", 25, 5.0f, 2.0f);
            Omaha.AddItem("water", 7, 5.0f, 2.0f);
            Omaha.AddItem("guns", 0, 100.0f, 25.0f);
            Omaha.AddItem("steel", 0, 20.0f, 10.0f);
            Omaha.AddItem("iron", 0, 50.0f, 20.0f);
            Omaha.AddItem("coal", 0, 40.0f, 10.0f);
            Omaha.AddItem("alcohol", 0, 100.0f, 25.0f);
            cities.Add("Omaha", Omaha);
            
            Town Welch = new Town();
            Welch.AddItem("food", 3, 50.0f, 25.0f);
            Welch.AddItem("water", 2, 35.0f, 10.0f);
            Welch.AddItem("guns", 5, 85.0f, 45.0f);
            Welch.AddItem("steel", 0, 20.0f, 10.0f);
            Welch.AddItem("iron", 0, 50.0f, 10.0f);
            Welch.AddItem("coal", 10, 20.0f, 10.0f);
            Welch.AddItem("alcohol", 10, 50.0f, 15.0f);
            cities.Add("Welch", Welch);
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
