using System.Runtime.CompilerServices;

class Manager : Person, IEmployee
{
    // Wat is uniek aan een manager?
    // Managers hebben een Employee Code!
    // Employee Code kan alleen maar gelezen worden en wordt zelf binnen de class gemaakt?
    private string _employeeCode = "InformaticaStinkt";
    public string EmployeeCode {get;}
    public Manager(string firstName, string lastName, string emailAddress, string phoneNumber) : base(firstName, lastName, emailAddress, phoneNumber)
    {
    }
// [1] Change the restaurant info
// [2] Change the menu
// [3] See all reservations

    public void ChangeRestaurantInfo(RestaurantInfo Info, string adres, string email, string phoneNumber){
        Info.Adress = adres;
        Info.Email = email;
        Info.Phone_number = phoneNumber;
    }
    public void ChangeMenu(){}
    public void SeeReservations(){}
}