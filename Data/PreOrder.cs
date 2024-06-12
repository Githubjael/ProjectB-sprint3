using System.Text.Json.Serialization;
public class PreOrder
{
    [JsonPropertyName("guest ID")]
    public string GuestID {get; set;}
    [JsonPropertyName("Order")]
    public List<string> Dishes {get; set;}
    [JsonPropertyName("Time")]
    public TimeOnly Time{get; set;}

    [JsonPropertyName("Date")]
    public DateOnly Date{get; set;}

    [JsonConstructor]
    public PreOrder(string guestID, List<string> dishes, string date, string time)
    {
        GuestID = guestID;
        Dishes = dishes;
        // Time only om te sorteren op basis vantijd. Use LINQ ORDERBY
        Time = TimeOnly.Parse(time);
        Date = DateOnly.Parse(date);
    }

}
