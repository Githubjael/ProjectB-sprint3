using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json.Serialization;
public class ContactDataModel
// [{Date: {"datum":{{}, {}, {}, {}}, {"datum": {{}, ...}}]
{
    [JsonPropertyName("Address")]
    public string Adress {get; set;}

    [JsonPropertyName("PhoneNumber")]
    public string PhoneNumber {get; set;}

    [JsonPropertyName("Email")]

    public string Email {get; set;}

    [JsonConstructor]
    public ContactDataModel(string adress, string phoneNumber, string email)
    {
        Adress = adress;
        PhoneNumber = phoneNumber;
        Email = email;
    }

}