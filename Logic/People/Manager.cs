using System.ComponentModel;
using System.Formats.Asn1;

class Manager : Person
{
    protected internal string EmployeeCode { get; } = "AppelTaart";

    public Manager(string firstName, string lastName, string emailAddress, string phoneNumber, string password) :
    base(firstName, lastName, emailAddress, phoneNumber, password){}

}

