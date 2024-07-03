public abstract class Person
{
    protected Person(string firstName, string lastName, string emailAddress, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string EmailAddress { get; }

    public string PhoneNumber { get; }
    
    protected internal string Password { get; protected set; }

}
