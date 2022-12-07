using Microsoft.EntityFrameworkCore;
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
            if (color == "Blue")
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
            // Collection all data from Hardwares table in db and ordering in a List
            List<Asset> orderedAssets = context.Assets
                .Include(a => a.Office)
                .OrderBy(a => a.Office.Country)
                .ThenBy(a => a.DateOfPurchase)
                .ToList();

            DateTime maxLifeTime = DateTime.Now.AddYears(-3);

            // Printing table header
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if (showIds)
            {
                Console.WriteLine("ID".PadRight(5) + "Type".PadRight(10) + "Brand".PadRight(15) + "Model".PadRight(17) + "Date of purchase".PadRight(20) + "Office".PadRight(10) + "Currency".PadRight(10) + "Price (USD)".PadRight(15) + "Local price");
            }
            else
            {
                Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(17) + "Date of purchase".PadRight(20) + "Office".PadRight(10) + "Currency".PadRight(10) + "Price (USD)".PadRight(15) + "Local price");
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();

            // Printing table content
            foreach (Asset asset in orderedAssets)
            {
                TimeSpan diff = asset.DateOfPurchase - maxLifeTime;

                // Check if date of purchase is less than 3/6 months away from 3 years and make RED/YELLOW.
                // Counting 1 month as 30 days
                if (diff.Days < 90)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (diff.Days < 180)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                // calculating local price and rounding to maximum 2 decimal places
                double localPrice = Math.Round(asset.Price * asset.Office.CurrencyValue, 2);

                if (showIds)
                {
                    Console.WriteLine(asset.Id.ToString().PadRight(5) + asset.Type.PadRight(10) + asset.Brand.PadRight(15) + asset.Model.PadRight(17) + asset.DateOfPurchase.ToString("yyyy-MM-dd").PadRight(20) + asset.Office.Country.PadRight(10) + asset.Office.LocalCurrency.PadRight(10) + asset.Price.ToString().PadRight(15) + localPrice);
                }
                else
                {
                    Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(17) + asset.DateOfPurchase.ToString("yyyy-MM-dd").PadRight(20) + asset.Office.Country.PadRight(10) + asset.Office.LocalCurrency.PadRight(10) + asset.Price.ToString().PadRight(15) + localPrice);
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public static void AddAsset(DatabaseContext context)
        {
            string[] types = { "Computer", "Laptop", "Phone" };
            string[] offices = { "USA", "Sweden", "Italy" };
            string input;
            string type = "";
            string brand = "";
            string model = "";
            int price = 0;
            int officeId = 0;
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

            ShowMessage("What office is this asset being used in?.", "Blue");
            MultiOptionList(offices);
            do
            {
                InputInstruction("Registering asset in office:");
                input = Console.ReadLine();
                input.Trim();

                switch (input)
                {
                    case "1":
                        officeId = 1;
                        break;
                    case "2":
                        officeId = 2;
                        break;
                    case "3":
                        officeId = 3;
                        break;
                    case "q":
                        return;
                    default:
                        ShowMessage("This office doesn't exist", "Red");
                        break;
                }
            } while (officeId == 0);
            Console.WriteLine();

            // Creating hardware to be added to database
            Asset asset = new Asset();
            asset.Type = type;
            asset.Brand = brand;
            asset.Model = model;
            asset.Price = price;
            asset.DateOfPurchase = date;
            asset.OfficeId = officeId;

            // Adding to database and saving
            context.Add(asset);
            context.SaveChanges();

            // Confirmation on successfull add
            ShowMessage("Asset successfully added!", "Green");
            Console.WriteLine();
        }

        public static void UpdateAsset(DatabaseContext context)
        {
            string[] options = { "Brand", "Model", "Price", "Date of Purchase", "Office", "Save & exit editing" };
            string[] offices = { "USA", "Sweden", "Italy" };
            string input;
            string editString = "";
            int editPrice = 0;
            int officeId = 0;
            DateTime editDate = new DateTime(0001, 01, 01);
            bool changeEntered = false;

            ShowMessage("Choose asset to edit. \n   Enter 'Q' to leave at any time in the process.", "Blue");
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
                Asset editAsset = context.Assets.FirstOrDefault(x => x.Id == int.Parse(input));

                if (editAsset != null)
                {
                    ShowMessage($"Editing {editAsset.Brand} {editAsset.Model} in the {editAsset.Office.Country} office\n", "Green");

                    do
                    {
                        ShowMessage("What field would you like to edit?", "Blue");
                        MultiOptionList(options);

                        do
                        {
                            InputInstruction("Choose field to edit:");
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
                            else if (input == "5") // editing office
                            {
                                ShowMessage("What office is this asset being used in?.", "Blue");
                                MultiOptionList(offices);
                                do
                                {
                                    InputInstruction("Move to new office:");
                                    input = Console.ReadLine();
                                    input.Trim();

                                    switch (input)
                                    {
                                        case "1":
                                            officeId = 1;
                                            break;
                                        case "2":
                                            officeId = 2;
                                            break;
                                        case "3":
                                            officeId = 3;
                                            break;
                                        case "q":
                                            return;
                                        default:
                                            ShowMessage("This office doesn't exist", "Red");
                                            break;
                                    }
                                } while (officeId == 0);

                                editAsset.OfficeId = officeId;
                                changeEntered = true;
                            }
                            else if (input == "6") // save changes
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
                    } while (input != "6");

                    // update and save hardware item
                    context.Assets.Update(editAsset);
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
}
