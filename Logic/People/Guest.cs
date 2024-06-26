public class Guest : Person
{
    private bool PasswordIsSet = false;

    public Guest(string firstName, string lastName, string emailAddress, string phoneNumber, string password) 
        : base(firstName, lastName, emailAddress, phoneNumber, password)
    {
        // wachtwoort word nu geset
        this.PasswordIsSet = true;
    }

    // Wachtwoord word alleen geset als PasswordIsSet true is. 
    public new string Password 
    {
        get => base.Password;
        set
        {
            if (PasswordIsSet)
            {
                base.Password = value;
            }
        }
    }
}
