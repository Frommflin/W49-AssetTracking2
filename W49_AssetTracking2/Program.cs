
using W49_AssetTracking2;

DatabaseContext context = new DatabaseContext();
string input;

while (true)
{
    Methods.ShowMenu();
    Methods.InputInstruction("Go to option:");
    input = Console.ReadLine();
    input.Trim();
    Console.WriteLine();

    switch (input)
    {
        case "1":
            // Showing assets
            Methods.ShowAssets(context, false);
            break;
        case "2":
            // Adding assets
            Methods.AddAsset(context);
            break;
        case "3":
            // Updating asset
            Methods.UpdateAsset(context);
            break;
        case "4":
            // Deleting asset
            Methods.ShowMessage("Delete asset", "Blue");
            break;
        case "5":
            // Exiting application
            Methods.ShowMessage("Shutting down", "Red");
            return;
        default:
            Methods.ShowMessage("Invalid menu-option.", "Red");
            break;
    }
}