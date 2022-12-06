using System.Net.Sockets;

namespace W49_AssetTracking2
{
    internal class Methods
    {
        public static void MultiOptionList(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(i + 1);
                Console.ResetColor();
                Console.WriteLine($") {options[i]}");
            }
        }

        public static void ShowMenu()
        {
            string[] menu = { 
                "Show Assets", 
                "Add new Asset", 
                "Update an asset", 
                "Delete an asset", 
                "Exit AssetTracker" 
            };

            Console.WriteLine("Menu: Type the number for the desired option to move on");
            Console.WriteLine("**************************");
            MultiOptionList(menu);
            Console.WriteLine("**************************");
        }

        public static void ShowMessage(string message, string color) 
        {
            //Showing line of message/instruction in specified color 
            if(color == "Blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            } 
            else if (color == "Green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void InputInstruction(string message)
        {
            //Showing line of instruction before user input
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{message} ");
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

        public static void AddAsset(DatabaseContext context)
        {
            string input;
            string[] types = { "Computer", "Laptop", "Phone" };

            string type = "";
            string brand = "";
            string model = "";
            int price = 0;
            DateTime date = new DateTime(0001,01,01);

            ShowMessage("Enter information about the new asset. \n   Enter 'Q' to leave at any time in the process.", "Blue");
            MultiOptionList(types);

            //do-while loops preventing inputs from being left empty or contain invalid data
            do
            {
                InputInstruction("Type of asset:");
                input = Console.ReadLine();
                input.Trim();

                switch (input)
                {
                    case "1":
                        type = "Computer";
                        break;
                    case "2":
                        type = "Laptop";
                        break;
                    case "3":
                        type = "Phone";
                        break;
                    case "q":
                        return;
                    default:
                        ShowMessage("Not a valid option", "Red");
                        break;
                }
            } while (type == "");

            do
            {
                InputInstruction("Brand:");
                input = Console.ReadLine();
                input.Trim();

                if (String.IsNullOrEmpty(input) || input == " ")
                {
                    ShowMessage("Brand cannot be left empty", "Red");
                }
                else if (input == "q")
                {
                    return;
                }
                else
                {
                    brand = input;
                }
            } while (brand == "");

            do
            {
                InputInstruction("Model:");
                input = Console.ReadLine();
                input.Trim();

                if (String.IsNullOrEmpty(input) || input == " ")
                {
                    ShowMessage("Model cannot be left empty", "Red");
                }
                else if (input == "q")
                {
                    return;
                }
                else
                {
                    model = input;
                }
            } while (model == "");

            do
            {
                InputInstruction("Price:");
                input = Console.ReadLine();
                input.Trim();

                if (String.IsNullOrEmpty(input) || input == " ")
                {
                    ShowMessage("Price cannot be left empty!", "Red");
                }
                else if (input == "q")
                {
                    return;
                }
                else if (int.TryParse(input, out int value))
                {
                    if (value == 0)
                    {
                        ShowMessage("Price cannot be 0!", "Red");
                    }
                    else if (value < 0)
                    {
                        ShowMessage("Price cannot be negative", "Red");
                    }
                    price = value;
                }
                else
                {
                    ShowMessage("Price has to be a number!", "Red");
                }
            } while (price == 0);

            do
            {
                InputInstruction("Date of purchase (yyyy-mm-dd):");
                input = Console.ReadLine();
                input.Trim();

                if (String.IsNullOrEmpty(input) || input == " ")
                {
                    ShowMessage("Date cannot be left empty!", "Red");
                }
                else if (input == "q")
                {
                    return;
                }
                else if (DateTime.TryParse(input, out DateTime purchased))
                {
                    date = purchased;
                }
                else
                {
                    ShowMessage("Invalid date", "Red");
                }
            } while (date == new DateTime(0001, 01, 01));

            Console.WriteLine();

            // Creating hardware to be added to database
            Hardware asset = new Hardware();
            asset.Type = type;
            asset.Brand = brand;
            asset.Model = model;
            asset.Price = price;
            asset.DateOfPurchase = date;

            // Adding to database and saving
            context.Add(asset);
            context.SaveChanges();

            // Confirmation on successfull add
            ShowMessage("Asset successfully added!", "Green");
            Console.WriteLine();
        }
    }
}
