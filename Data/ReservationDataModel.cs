using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
public class ReservationDataModel : IComparable<ReservationDataModel>
// [{Date: {"datum":{{}, {}, {}, {}}, {"datum": {{}, ...}}]
{
    [JsonPropertyName("GuestID")]
    public int GuestID {get; set;}

    [JsonPropertyName("First name")]
    public string FirstName {get; set;}

    [JsonPropertyName("Last name")]

    public string LastName {get; set;}
    
    [JsonPropertyName("Phone number")]
    public string PhoneNumber {get; set;}

    [JsonPropertyName("Email address")]
    public string EmailAddress {get; set;}

    [JsonPropertyName("Date")]
    public string Date {get; set;}

    [JsonPropertyName("Time")]

    public string Time {get; set;}

    [JsonPropertyName("Table")]
    public List<Table> Tables = new List<Table>();


    [JsonConstructor]
    public ReservationDataModel(int gastID, string firstName,string lastName, string phoneNumber,string emailAddress, string date, string time, List<Table> tables)
    {
        GuestID = gastID;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        Date = date;
        Time = time;
        Tables = tables;
    }

    public int CompareTo(ReservationDataModel other)
    {
        if (DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("nl-NL")).CompareTo(DateTime.ParseExact(other.Date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("nl-NL"))) != 0)
        {
            return DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("nl-NL")).CompareTo(DateTime.ParseExact(other.Date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("nl-NL"))); // ASC op basis van date
        }
        else if (DateTime.ParseExact(Time, "HH:mm", CultureInfo.GetCultureInfo("nl-Nl")).CompareTo(DateTime.ParseExact(other.Time, "HH:mm", CultureInfo.GetCultureInfo("nl-NL"))) != 0) // Als het op zelfde dag ASC op basis van tijd
        {
            return DateTime.ParseExact(Time, "HH:mm", CultureInfo.GetCultureInfo("nl-Nl")).CompareTo(DateTime.ParseExact(other.Time, "HH:mm", CultureInfo.GetCultureInfo("nl-NL")));
        }
        else
        {
            return 0;
        }
    }
}
