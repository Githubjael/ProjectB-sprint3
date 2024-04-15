class Person{
    // Every person of ze nu employee of gast zijn hebben een voor- en achternaam (obviously) en ze hebben een email en telefoonr
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string EmailAddress {get; set;}
    public string PhoneNumber {get; set;}
    public Person(string firstName, string lastName, string emailAddress, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }
}