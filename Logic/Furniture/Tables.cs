public class Tables
{
    public string ID;
    public string Type;
    public bool Reserved;
    // public int Amount;
    public Tables(string id, string type) // omg wanneer er wordt gelezen in json wordt er weer een neiuwe object aangemaakt!
    { // WAARDOOR de ids in json een EXTRA A KRIJGEN!!!!!!!!!
    try{
        Convert.ToInt32(id);
        if (type == "2 persons table")
        {
            ID = $"{id}A";
        }
        else if (type == "4 persons table")
        {
            ID = $"{id}B";
        }
        else if (type == "6 persons table")
        {
            ID = $"{id}C";
        }
    }
    catch (Exception) // Dit zijn voor de tafels die al in json staan en die komen niet in TableTracker
    {
        ID = id;
    }
        Type = type;
    }
    public void IsReserved()
    {
        Reserved = true;
    }
    public void Cancelled()
    {
        Reserved = false;
    }
}
