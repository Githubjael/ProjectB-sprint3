public class Menu : Page
{
    private string _name = "Menu";
    public List<MenuItem> Items { get; set; }

    public override string Name => _name;

    public Menu()
    {
        Items = new List<MenuItem>
        {
            new MenuItem("Salmon", 15.99, "Fish"),
            new MenuItem("Steak", 12.99, "Meat"),
            new MenuItem("Caesar Salad", 8.99, "Vegetarian"),
            new MenuItem("Soda", 2.49, "Drink"),
            new MenuItem("Iced Tea", 1.99, "Drink") 
            //CHANGE CATEGORY AND MAKE INTO JSON
        };
    }

    public void AddItem()
    {
    }

    public void RemoveItem()
    {

    }

    public void ChangeItem() 
    {
    }

    public void DisplayMenu()
    {
    }

    public override void Options()
    {
        Console.WriteLine("[V] View menu");
        Console.WriteLine("[VC] View a specific category");
        base.Options();
        //READLINE AND MAKE IT WORK
    }

    public override void Contents()
    {
    }
}
