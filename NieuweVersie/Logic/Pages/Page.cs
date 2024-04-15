public abstract class Page
{
    protected List<string> _pages = new(){
        "[H]: Home",
        "[M]: Menu",
        "[R]: Reservation",
        "[RV]: Review",
        "[C]: Contact"
    };
  
    public abstract string Name { get;}

    public virtual void Options()
    {
        System.Console.WriteLine("Options:");
        foreach(string page in _pages)
        {
            switch(Name){
                case "Home":
                if (page != _pages[0])
                {
                    System.Console.WriteLine(page);
                }
                break;
                case "Menu":
                if (page != _pages[1])
                {
                    System.Console.WriteLine(page);
                }
                break;
                case "Reservation":
                if (page != _pages[2])
                {
                    System.Console.WriteLine(page);
                }
                break;
                case "Review":
                if(page != _pages[3])
                {
                    System.Console.WriteLine(page);
                }
                break;
                case "Contact":
                if (page != _pages[4])
                {
                    System.Console.WriteLine(page);
                }
                break;
            }
        }
        System.Console.WriteLine("[E]: Exit");
    }
    // Inhoud is uniek aan de derived Pagina's
    public abstract void Contents();

}
