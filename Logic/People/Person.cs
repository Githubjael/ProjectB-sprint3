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

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
