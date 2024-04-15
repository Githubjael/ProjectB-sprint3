class Reviews : Page{
    private string _name = "Reviews";

    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        System.Console.WriteLine("[L]: Leave a review");
        System.Console.WriteLine("[S]: See all review");
        base.Options();
        //MAKE READLINE WORK
    }
    public override void Contents()
    {
        
    }
}
