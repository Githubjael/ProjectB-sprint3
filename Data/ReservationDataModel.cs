using System.Text.Json.Serialization;
public class ReservationDataModel
// [{Date: {"datum":{{}, {}, {}, {}}, {"datum": {{}, ...}}]
{
    [JsonPropertyName("Table")]
    public Table table {get; set;}

    [JsonPropertyName("GuestID")]
    public int GuestID {get; set;}

    [JsonPropertyName("Date")]
    public string Date {get; set;}

    [JsonPropertyName("Time")]

    public string Time {get; set;}

    [JsonPropertyName("First name")]
    public string FirstName {get; set;}

    [JsonPropertyName("Last name")]

    public string LastName {get; set;}

    [JsonPropertyName("Email address")]
    public string EmailAddress {get; set;}

    [JsonPropertyName("Phone number")]
    public string PhoneNumber {get; set;}
    
    [JsonConstructor]
    public ReservationDataModel(Table Table, int gastID, string date, string time, string firstName, string lastName, string emailAddress, string phoneNumber)
    {
        this.table = Table;
        GuestID = gastID;
        FirstName = firstName;
        LastName = lastName;
        Date = date;
        Time = time;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }
}
