namespace W49_AssetTracking2
{
    internal class Methods
    {
        public static void ShowMenu()
        {
            string[] menu = { "Show Assets", "Add new Asset", "Exit AssetTracker" };

            Console.WriteLine("Menu: Type the number for the desired option to move on");
            Console.WriteLine("**************************");
            for (int i = 0; i < menu.Length; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(i + 1);
                Console.ResetColor();
                Console.WriteLine($") {menu[i]}");
            }
            Console.WriteLine("**************************");
        }

        public static void ShowMessage(string message, string color) 
        {
            //Showing line of message/instruction in specified color 
            if(color == "Blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void ShowAssets(DatabaseContext context)
        {
            // Collection all data from Hardwares table in db and ordering in a List
            List<Hardware> orderedAssets = context.Hardwares
                .OrderBy(a => a.Type)
                .ThenBy(a => a.DateOfPurchase)
                .ToList();

            DateTime maxLifeTime = DateTime.Now.AddYears(-3);

            // Printing out sorted list to user
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Price (USD)".PadRight(20) + "Date of purchase");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.ResetColor();

            foreach (Hardware hardware in orderedAssets)
            {
                TimeSpan diff = hardware.DateOfPurchase - maxLifeTime;
                
                // Check if date of purchase is less than 3 months away from 3 years and make RED
                if (diff.Days < 90) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(hardware.Type.PadRight(20) + hardware.Brand.PadRight(20) + hardware.Model.PadRight(20) + hardware.Price.ToString().PadRight(20) + hardware.DateOfPurchase.ToString("yyyy-MM-dd"));
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
