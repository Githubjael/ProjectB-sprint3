using System.ComponentModel;
using System.Formats.Asn1;

class Manager : Person
{
    public string EmployeeCode { get; set; } = "AppelTaart";
    public Manager(string firstName, string lastName, string emailAddress, string phoneNumber, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public void ReservationOptions()
    {
        ManagerOptions.ReservationOptions();
    }

    public void ReviewOptions()
    {
        ManagerOptions.ReviewOptions();
    }

    public void ChangeRestaurantInfo()
    {
        ManagerOptions.ChangeRestaurantInfo();
    }
}

