class Contact : Page
{
    public override string Name => "Contact";
    public override void Options()
    {
        base.Options();
    }
    public override void Contents()
    {
        System.Console.WriteLine("Email: retaurant@example.nl");
        System.Console.WriteLine("Phone number: 06 11 22 33 44");
        System.Console.WriteLine("You can call between 10:00 and 22:00");
    }
}
