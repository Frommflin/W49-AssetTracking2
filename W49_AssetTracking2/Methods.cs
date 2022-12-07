using System.Diagnostics;
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

        /*  
         *  --------------------------------------------
         *  ---              Level 2                 ---
         *  --------------------------------------------
        */

        /*
        public static void ShowAssets(DatabaseContext context, bool showIds)
        {
            // Collection all data from Hardwares table in db and ordering in a List
            List<Hardware> orderedAssets = context.Hardwares
                .OrderBy(a => a.Type)
                .ThenBy(a => a.DateOfPurchase)
                .ToList();

            DateTime maxLifeTime = DateTime.Now.AddYears(-3);

            if (showIds) // showing table with IDs
            {
                // Printing out sorted list to user
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Id".PadRight(10) + "Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Price (USD)".PadRight(20) + "Date of purchase");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                Console.ResetColor();

                foreach (Hardware hardware in orderedAssets)
                {
                    TimeSpan diff = hardware.DateOfPurchase - maxLifeTime;

                    // Check if date of purchase is less than 3 months away from 3 years and make RED
                    if (diff.Days < 90)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(hardware.Id.ToString().PadRight(10) + hardware.Type.PadRight(20) + hardware.Brand.PadRight(20) + hardware.Model.PadRight(20) + hardware.Price.ToString().PadRight(20) + hardware.DateOfPurchase.ToString("yyyy-MM-dd"));
                    Console.ResetColor();
                }
            }
            else // showing table without IDs
            {
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
            }
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

        public static void UpdateAsset(DatabaseContext context)
        {
            string input;
            string[] options = { "Brand", "Model", "Price", "Date of Purchase", "Save & exit editing" };
            string editString = "";
            int editPrice = 0;
            DateTime editDate = new DateTime(0001,01,01);
            bool changeEntered = false;

            ShowMessage("Chose asset to edit. \n   Enter 'Q' to leave at any time in the process.", "Blue");
            ShowAssets(context, true);
            InputInstruction("Asset ID:");
            input = Console.ReadLine();
            input.Trim();
            Console.WriteLine();

            if ( input.ToLower() == "q")
            {
                return;
            }
            else
            {
                Hardware editAsset = context.Hardwares.FirstOrDefault(x => x.Id == int.Parse(input));

                if (editAsset != null)
                {
                    ShowMessage($"Editing {editAsset.Brand} {editAsset.Model} with id {editAsset.Id}\n", "Green");

                    do
                    {
                        ShowMessage("What field would you like to edit?", "Blue");
                        MultiOptionList(options);

                        do
                        {
                            InputInstruction("Chose field to edit:");
                            input = Console.ReadLine();
                            input.Trim();
                            if (input == "1") // editing brand
                            {
                                do
                                {
                                    InputInstruction("Enter new brand:");
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
                                        editString = input;
                                    }
                                } while (editString == "");

                                editAsset.Brand = editString;
                                changeEntered = true;
                            }
                            else if (input == "2") // editing model
                            {
                                do
                                {
                                    InputInstruction("Enter new model:");
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
                                        editString = input;
                                    }
                                } while (editString == "");

                                editAsset.Model = editString;
                                changeEntered = true;
                            }
                            else if (input == "3") // editing price
                            {
                                do
                                {
                                    InputInstruction("Enter new price:");
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
                                            value = 0;
                                        }
                                        editPrice = value;
                                    }
                                    else
                                    {
                                        ShowMessage("Price has to be a number!", "Red");
                                    }
                                } while (editPrice == 0);

                                editAsset.Price = editPrice;
                                changeEntered = true;
                            }
                            else if (input == "4") // editing date
                            {
                                do
                                {
                                    InputInstruction("Enter new date of purchase (yyyy-mm-dd):");
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
                                        editDate = purchased;
                                    }
                                    else
                                    {
                                        ShowMessage("Invalid date", "Red");
                                    }
                                } while (editDate == new DateTime(0001, 01, 01));

                                editAsset.DateOfPurchase = editDate;
                                changeEntered = true;
                            }
                            else if (input == "5")
                            {
                                changeEntered = true;
                            }
                            else if (input.ToLower() == "q")
                            {
                                return;
                            }
                            else
                            {
                                ShowMessage("Invalid option", "Red");
                            }
                        } while (changeEntered == false);
                    } while (input != "5");

                    // update and save hardware item
                    context.Hardwares.Update(editAsset);
                    context.SaveChanges();
                    ShowMessage("Changes saved", "Green");
                }
                else
                {
                    ShowMessage("Invalid asset ID\n", "Red");
                    return;
                }
            }
        }
        
        public static void RemoveAsset(DatabaseContext context)
        {
            string input;

            ShowMessage("Chose asset to remove. \n   Enter 'Q' to leave.", "Blue");
            ShowAssets(context, true);
            InputInstruction("Asset ID:");
            input = Console.ReadLine();
            input.Trim();
            Console.WriteLine();

            if (input.ToLower() == "q")
            {
                return;
            }
            else
            {
                Hardware removeAsset = context.Hardwares.FirstOrDefault(x => x.Id == int.Parse(input));

                if (removeAsset != null)
                {
                    // remove hardware and save changes to table
                    context.Hardwares.Remove(removeAsset);
                    context.SaveChanges();
                    ShowMessage($"Successfully removed {removeAsset.Brand} {removeAsset.Model}\n", "Green");
                }
                else
                {
                    ShowMessage("Invalid asset ID\n", "Red");
                    return;
                }
            }
        }
        */

        /*  
         *  --------------------------------------------
         *  ---              Level 3                 ---
         *  --------------------------------------------
        */

        public static void ShowAssets(DatabaseContext context, bool showIds)
        {
            ShowMessage("Level 3 under Construction", "Red");
            /*
            // Collection all data from Hardwares table in db and ordering in a List
            List<Hardware> orderedAssets = context.Hardwares
                .OrderBy(a => a.Type)
                .ThenBy(a => a.DateOfPurchase)
                .ToList();

            DateTime maxLifeTime = DateTime.Now.AddYears(-3);

            if (showIds) // showing table with IDs
            {
                // Printing out sorted list to user
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Id".PadRight(10) + "Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Price (USD)".PadRight(20) + "Date of purchase");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                Console.ResetColor();

                foreach (Hardware hardware in orderedAssets)
                {
                    TimeSpan diff = hardware.DateOfPurchase - maxLifeTime;

                    // Check if date of purchase is less than 3 months away from 3 years and make RED
                    if (diff.Days < 90)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(hardware.Id.ToString().PadRight(10) + hardware.Type.PadRight(20) + hardware.Brand.PadRight(20) + hardware.Model.PadRight(20) + hardware.Price.ToString().PadRight(20) + hardware.DateOfPurchase.ToString("yyyy-MM-dd"));
                    Console.ResetColor();
                }
            }
            else // showing table without IDs
            {
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
            }
            Console.WriteLine();
            */
        }

        public static void AddAsset(DatabaseContext context)
        {
            ShowMessage("Level 3 under Construction", "Red");
            /*
            string input;
            string[] types = { "Computer", "Laptop", "Phone" };

            string type = "";
            string brand = "";
            string model = "";
            int price = 0;
            DateTime date = new DateTime(0001, 01, 01);

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
            */
        }

        public static void UpdateAsset(DatabaseContext context)
        {
            ShowMessage("Level 3 under Construction", "Red");
            /*
            string input;
            string[] options = { "Brand", "Model", "Price", "Date of Purchase", "Save & exit editing" };
            string editString = "";
            int editPrice = 0;
            DateTime editDate = new DateTime(0001, 01, 01);
            bool changeEntered = false;

            ShowMessage("Chose asset to edit. \n   Enter 'Q' to leave at any time in the process.", "Blue");
            ShowAssets(context, true);
            InputInstruction("Asset ID:");
            input = Console.ReadLine();
            input.Trim();
            Console.WriteLine();

            if (input.ToLower() == "q")
            {
                return;
            }
            else
            {
                Hardware editAsset = context.Hardwares.FirstOrDefault(x => x.Id == int.Parse(input));

                if (editAsset != null)
                {
                    ShowMessage($"Editing {editAsset.Brand} {editAsset.Model} with id {editAsset.Id}\n", "Green");

                    do
                    {
                        ShowMessage("What field would you like to edit?", "Blue");
                        MultiOptionList(options);

                        do
                        {
                            InputInstruction("Chose field to edit:");
                            input = Console.ReadLine();
                            input.Trim();
                            if (input == "1") // editing brand
                            {
                                do
                                {
                                    InputInstruction("Enter new brand:");
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
                                        editString = input;
                                    }
                                } while (editString == "");

                                editAsset.Brand = editString;
                                changeEntered = true;
                            }
                            else if (input == "2") // editing model
                            {
                                do
                                {
                                    InputInstruction("Enter new model:");
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
                                        editString = input;
                                    }
                                } while (editString == "");

                                editAsset.Model = editString;
                                changeEntered = true;
                            }
                            else if (input == "3") // editing price
                            {
                                do
                                {
                                    InputInstruction("Enter new price:");
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
                                            value = 0;
                                        }
                                        editPrice = value;
                                    }
                                    else
                                    {
                                        ShowMessage("Price has to be a number!", "Red");
                                    }
                                } while (editPrice == 0);

                                editAsset.Price = editPrice;
                                changeEntered = true;
                            }
                            else if (input == "4") // editing date
                            {
                                do
                                {
                                    InputInstruction("Enter new date of purchase (yyyy-mm-dd):");
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
                                        editDate = purchased;
                                    }
                                    else
                                    {
                                        ShowMessage("Invalid date", "Red");
                                    }
                                } while (editDate == new DateTime(0001, 01, 01));

                                editAsset.DateOfPurchase = editDate;
                                changeEntered = true;
                            }
                            else if (input == "5")
                            {
                                changeEntered = true;
                            }
                            else if (input.ToLower() == "q")
                            {
                                return;
                            }
                            else
                            {
                                ShowMessage("Invalid option", "Red");
                            }
                        } while (changeEntered == false);
                    } while (input != "5");

                    // update and save hardware item
                    context.Hardwares.Update(editAsset);
                    context.SaveChanges();
                    ShowMessage("Changes saved", "Green");
                }
                else
                {
                    ShowMessage("Invalid asset ID\n", "Red");
                    return;
                }
            }
            */
        }

        public static void RemoveAsset(DatabaseContext context)
        {
            ShowMessage("Level 3 under Construction", "Red");
            /*
            string input;

            ShowMessage("Chose asset to remove. \n   Enter 'Q' to leave.", "Blue");
            ShowAssets(context, true);
            InputInstruction("Asset ID:");
            input = Console.ReadLine();
            input.Trim();
            Console.WriteLine();

            if (input.ToLower() == "q")
            {
                return;
            }
            else
            {
                Hardware removeAsset = context.Hardwares.FirstOrDefault(x => x.Id == int.Parse(input));

                if (removeAsset != null)
                {
                    // remove hardware and save changes to table
                    context.Hardwares.Remove(removeAsset);
                    context.SaveChanges();
                    ShowMessage($"Successfully removed {removeAsset.Brand} {removeAsset.Model}\n", "Green");
                }
                else
                {
                    ShowMessage("Invalid asset ID\n", "Red");
                    return;
                }
            }
        }
        */
        }
}
