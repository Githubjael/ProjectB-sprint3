class Reviews : Page{
    private string _name = "Reviews";

    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        System.Console.WriteLine(""); //add options later
        base.Options();
    }
    public override void Contents()
    {
        
    }
}