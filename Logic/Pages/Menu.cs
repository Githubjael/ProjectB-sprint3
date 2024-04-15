public class Menu : Page{
    private string _name = "Menu";

    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        System.Console.WriteLine("add more options here"); //view menu if logged in as customer
        base.Options();
    }
    public override void Contents()
    {
        
    }
}