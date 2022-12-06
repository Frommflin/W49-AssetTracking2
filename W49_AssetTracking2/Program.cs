
using W49_AssetTracking2;

DatabaseContext context = new DatabaseContext();
string input;

while (true)
{
    Methods.ShowMenu();
    Console.Write("Go to option: ");
    input = Console.ReadLine();
    input.Trim();
    Console.WriteLine();

    switch (input)
    {
        case "1":
            //Handle showing assets
            Methods.ShowAssets(context);
            break;
        case "2":
            //Handle adding assets
            Methods.ShowMessage("Adding a new asset!!", "Blue");
            break;
        case "3":
            // Exiting application
            Methods.ShowMessage("Shutting down", "Red");
            return;
        default:
            Methods.ShowMessage("Invalid menuoption.", "Red");
            break;
    }
}