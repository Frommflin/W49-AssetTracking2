
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
            // Handle showing assets
            Methods.ShowAssets(context);
            break;
        case "2":
            // Handle adding assets
            Methods.AddAsset(context);
            break;
        case "3":
            // Exiting application
            Methods.ShowMessage("Shutting down", "Red");
            return;
        default:
            Methods.ShowMessage("Invalid menu-option.", "Red");
            break;
    }
}