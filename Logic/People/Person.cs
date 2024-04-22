public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string phoneNumber { get; set; }
    public string Password { get; set; }

    public Person(string firstName, string lastName, string emailAddress, string phonenumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        phoneNumber = phonenumber;
        Password = password;
    }
}
