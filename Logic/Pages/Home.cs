class Home : Page
{
    public string IsLoggedIn {get; private set;}
    private string _name = "Home";
    public override string Name
    {
        get => _name;
    }
    public override void Options()
    {
        base.Options();
        System.Console.WriteLine("[L]: Log in");
        System.Console.WriteLine("[S]: Sign up");
        //make readline here 
    }
    public override void Contents()
    {
        
    }

    public void SignUp()
    {
        System.Console.WriteLine("Enter your first name");
        System.Console.WriteLine("Enter your last name");
        System.Console.WriteLine("Enter your email address.");
        System.Console.WriteLine("Enter a password");
        System.Console.WriteLine("confirm your password");
        // Zet het in Json
    }
    public void LogIn()
    {
        System.Console.WriteLine("Full Name:");
        System.Console.WriteLine("Email address:");
        System.Console.WriteLine("Password");
        System.Console.WriteLine("Click enter to confirm.");
        // Kijk na met Json
        // Als email en password kloppen --> IsLoggedIn = true;
        // Als wachtwoord niet klopt dan maak reserveer je maar zonder account
    }
}
