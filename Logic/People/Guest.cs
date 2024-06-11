public class Guest : Person
{
    private bool PassWordIsSet = false;
    private string _passWord;
    public string PassWord 
    {
        get => _passWord;
        set{
            if(PassWordIsSet){
                _passWord = value;
            }
        }
    }
    public Guest(string firstName, string lastName, string emailAddress, string phoneNumber, string password) : base(firstName, lastName, emailAddress, phoneNumber, password)
    {
    }
}
