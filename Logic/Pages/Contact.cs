public class Contact : Page
{
    private string _name = "Contact";

    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        System.Console.WriteLine("[C]: Contact or ?");
        base.Options();
        //MAKE READLINE WORK
    }
    public override void Contents()
    {
        
    }
}
