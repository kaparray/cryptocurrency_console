using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using QuickType;
using System.Threading;


namespace Lab_10_Strings
{
    class MainClass
    {
        public static Welcome account;
        public static String limit = "10000"; // Starting value eqal 10
        public static String convert = "RUB"; // Starting converable currency is USD "$"



        public static void Main(string[] args)
        {
            // Create new Thread for synchronous code
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                getDataAboutCoins(limit);
            }).Start();
           

            outputGreeting(); //
            outputRegulation();
            menu();

            /*
            foreach (KeyValuePair<string, Datum> item in account.Data)
            {
                Console.WriteLine(item.Value.Name + " ");
            }
            */
        }


        /* 
         * Get data form API coinmarket and deserialize him
        */
        static void getDataAboutCoins(String limit)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://api.coinmarketcap.com/v2/ticker/?limit=" + limit);

                account = JsonConvert.DeserializeObject<Welcome>(json);
            }
       }


        /*
         * Output greeting table in dysplay
        */
        static void outputGreeting()
        {
            Console.WriteLine("⎡¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯⎤"); 
            Console.WriteLine("⎢\t\t  Hello dear user!\t\t  ⎢");
            Console.WriteLine("⎢ \t\t\t\t\t\t  ⎢");
            Console.WriteLine("⎢ This programm oputput data about cryptocurrency ⎢");
            Console.WriteLine("⎢ \t\t\t\t\t\t  ⎢");
            Console.WriteLine("⎣ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱ ̱⎦");
            Console.WriteLine("\n\n"); // three empty line
        }


        static void outputRegulation()
        {
            Console.WriteLine("Hello, here you can find the exchange rate." + "\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("1) Give information about the first n cryptocurrencies");
            Console.WriteLine("2) Give information about a particular crypto currency");
            Console.WriteLine("3) Settings");
            Console.WriteLine("\n" + "To close the program, click  \':q\'");
        }

        static void menu()
        {
            String menu_val;

            Console.Write("Your choice: ");
            while ((menu_val = Console.ReadLine()) != ":q")
            {

                if (menu_val == "1") menu1();
                else if (menu_val == "2") menu2();
                else if (menu_val == "3") menu3();
                else Console.WriteLine("Ooops! Incorrect menu data :(");
                Console.Write("Your choice: ");
            }
        }

        static void menu1()
        {
            Console.WriteLine("\n" + "Enter limit of cryptocurrencies you want to receive");
            Console.Write("Limit = ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            limit = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Good! Wait awhile :)");
            
            foreach (KeyValuePair<string, Datum> item in account.Data)
            {
                outputData(item);
            }
        }

        static void menu2()
        {
            String nameCr;
            KeyValuePair<string, Datum> datum;
            Console.WriteLine("\n" + "Enter name of cryptocurrency");
            Console.Write("Name = ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            nameCr = Console.ReadLine().ToLower();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Good! Wait awhile :)");


            foreach (KeyValuePair<string, Datum> item in account.Data)
            {
                if (item.Value.Name.ToLower().Equals(nameCr)) datum = item; 
            }
            try{
                if (datum.Value.Name != null) outputData(datum);
                else Console.WriteLine("No information found :)" + "\n" + "Try again, or check the correctness of the data");
            } 
            catch (System.NullReferenceException)
            {
               Console.WriteLine("No information found :)" + "\n" + "Try again, or check the correctness of the data");
            }


        }

        static void menu3()
        {
            Console.WriteLine("To change the currency type");
            Console.WriteLine("Available:\n\tUSD\n\tEUR\n\tGBP\n\tRUB\n\tJPY\n\tBTC");
            Console.Write("Your choise currency: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            convert = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (convert.ToLower() != "USD".ToLower() && convert.ToLower() != "EUR".ToLower() && convert.ToLower() != "GBP".ToLower()
                && convert.ToLower() != "RUB".ToLower() && convert.ToLower() != "JPY".ToLower() && convert.ToLower() != "BTC".ToLower())
            {
                Console.WriteLine("Error! Available currency right now is USD");
                convert = "USD";

            }
            else Console.WriteLine("All right! Your choise save :)");


        }

        static void outputData(KeyValuePair<string, Datum> item)
        {

            Console.Write("\tName: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(item.Value.Name.ToUpper());
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\tSymbol: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(item.Value.Symbol);
            Console.ForegroundColor = ConsoleColor.White;


            Console.Write("\tRank: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(item.Value.Rank);
            Console.ForegroundColor = ConsoleColor.White;


            Console.Write("\tPrice: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(item.Value.Quotes.Usd.Price + " ");
            if (convert == "USD") Console.WriteLine("$");
            else if (convert == "EUR") Console.WriteLine("€");
            else if (convert == "GBP") Console.WriteLine("£");
            else if (convert == "RUB") Console.WriteLine("₽");
            else if (convert == "JPY") Console.WriteLine("¥");
            else if (convert == "BTC") Console.WriteLine("BTC");
            Console.ForegroundColor = ConsoleColor.White;



            Console.Write("\tPercent change by 1 hour: ");
            if (item.Value.Quotes.Usd.PercentChange1H > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange1H + "↑");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange1H + "↓");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("\tPercent change by 24 hour: ");
            if (item.Value.Quotes.Usd.PercentChange24H > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange24H + "↑");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange24H + "↓");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("\tPercent change by 7 days: ");
            if (item.Value.Quotes.Usd.PercentChange24H > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange7D + "↑");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item.Value.Quotes.Usd.PercentChange7D + "↓");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("\n");
        }

    }
}