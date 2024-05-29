using System.ComponentModel;
using System.Formats.Asn1;

class Manager : Person
{
    public string EmployeeCode { get; set; } = "AppelTaart";
    public Manager(string firstName, string lastName, string emailAddress, string phoneNumber, string password) : base(firstName, lastName, emailAddress, phoneNumber, password)
    {
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

