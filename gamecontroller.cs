using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchantGame
{
    partial class MerchantGame
    {
        bool stop;
        char input;
        Dictionary<string, Town> cities;
        Base homeBase;

        //should be moved to a class later
        float mercPrice;

        public void Start()
        {
            cities = new Dictionary<string, Town>();
            AddTestCities();
            input = '0';
            homeBase = new Base();

            while(!stop)
            {
                Console.Clear();
                Console.WriteLine("Howdy Boss. Welcome to the shop.");
                Console.WriteLine("a : caravans");
                Console.WriteLine("b : inventory");
                Console.WriteLine("Go ahead and press \'x\' to leave.");

                input = getInput();

                Console.WriteLine(input);

                if (input == 'a')
                {
                    CaravanScreen();
                }
                if (input == 'b')
                {
                    InventoryScreen();
                }
                if (input == 'x')
                {
                    stop = true;
                }
            }
        }

        public void CaravanScreen()
        {
            bool canInteractWithCaravans = false;
            while (input != 'x')
            {
                if (homeBase.caravans.Count > 0)
                {
                    canInteractWithCaravans = true;
                }
                Console.Clear();
                Console.WriteLine("Here's where we stand with our caravan right now:\n");
                if (!canInteractWithCaravans)
                {
                    Console.WriteLine("Sorry to say, ain\'t got no caravans currently assigned.");
                }
                else
                {
                    for (int i = 0; i < homeBase.caravans.Count; i++)
                    {
                        Console.WriteLine("Caravan number " + (i+1) + " is carrying " 
                            + homeBase.caravans[i].inventory.Values.Sum() + " out of " 
                            + homeBase.caravans[i].capacity + " items.");
                    }
                }
                Console.WriteLine("\na : Hire caravans");
                if (canInteractWithCaravans)
                {
                    Console.WriteLine("# : View caravan details");
                    Console.WriteLine("f : Fire caravan");
                }

                Console.WriteLine("x : Return to the shop");

                input = getInput();

                if (input == 'a')
                {
                    HireCaravanScreen();
                }
                else if (Char.IsNumber(input) && canInteractWithCaravans)
                {
                    int caravanIndex = (int)Char.GetNumericValue(input);
                    if (caravanIndex <= homeBase.caravans.Count && caravanIndex != 0)
                    {
                        CaravanDetailsScreen(caravanIndex-1);
                    }
                }
            }
            input = '0';
            return;
        }

        public void CaravanDetailsScreen(int i)
        {
            Console.Clear();
            Console.WriteLine("Details about Caravan #" + (i+1) + ":\n");
            if (homeBase.caravans[i].travelling)
            {
                Console.WriteLine("She's heading towards " + homeBase.caravans[i].destinationCity);
                Console.WriteLine("Expected Arrival Date: " + homeBase.caravans[i].arrivalDate);
            }
            else if (homeBase.caravans[i].destinationCity == "Base")
            {
                Console.WriteLine("She's at the home base, waiting for orders");
            }
            else
            {
                Console.WriteLine("She's at " + homeBase.caravans[i].destinationCity
                    + ", waiting for orders");
            }

            Console.WriteLine("Speed: " + homeBase.caravans[i].speed);
            Console.WriteLine("Defense: " + homeBase.caravans[i].defense);
            Console.WriteLine("Capacity: " + homeBase.caravans[i].inventory.Values.Sum()
                + " out of " + homeBase.caravans[i].capacity + " items.");
            Console.WriteLine("\nPress any key to continue.");
            getInput();
            input = '0';
            return;
        }

        public void HireCaravanScreen()
        {
            Caravan newCaravan = new Caravan();
            Console.Clear();
            Console.WriteLine("Alright. Let's hire a caravan.\n");
            Console.WriteLine("We have $" + homeBase.money + " to work with.");
            Console.WriteLine("This caravan is going to cost $" + newCaravan.price);
            Console.WriteLine("That okay?\n");
            Console.WriteLine("y: accept");
            Console.WriteLine("any key: exit");

            input = getInput();

            if (input == 'y')
            {
                if (homeBase.money < newCaravan.price)
                {
                    Console.Clear();
                    Console.WriteLine("We can't afford this caravan.");
                    Console.WriteLine("Press any key to continue.");
                    getInput();
                }
                else
                {
                    homeBase.money -= newCaravan.price;
                    homeBase.caravans.Add(newCaravan);
                }
            }
        }

        public void InventoryScreen()
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

        public char getInput()
        {
            return Console.ReadKey(false).KeyChar;
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
            public bool travelling;
            public DateTime arrivalDate;
            public int speed;
            public int defense;
            public int capacity;
            public float price;

            public Caravan ()
            {
                inventory = new Dictionary<string, int>();
                destinationCity = "Base";
                travelling = false;
                //arrivalDate = null;
                speed = 1000; //not final value
                defense = 1;
                capacity = 100;
                price = 50.0f;
            }
        }

        class Base
        {
            public Dictionary<string, int> inventory;
            public List<Caravan> caravans;
            public float money;

            public Base ()
            {
                inventory = new Dictionary<string, int>();
                caravans = new List<Caravan>();
                money = 100.0f;
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
