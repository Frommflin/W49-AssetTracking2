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
    }
}
