class Guest : Person
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
    public Guest(string firstName, string lastName, string emailAddress, string phoneNumber) : base(firstName, lastName, emailAddress, phoneNumber)
    {
    }

    public void MakePassWord(string passWord){
        PassWord = passWord;
        PassWordIsSet = true;
    }
    public bool LogIn(string passWord){
        if (PassWord == passWord){
            return true;
        }
        return false;
    }

    public void ChangePassWord(){
        PassWordIsSet = false;
    }
}