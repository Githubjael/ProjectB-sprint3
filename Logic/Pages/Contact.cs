public class Contact : Page
{
    private string _name = "Contact";

    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        System.Console.WriteLine("[L]: Leave a review"); //other options?
        base.Options();
    }
    public override void Contents()
    {
        
    }
}