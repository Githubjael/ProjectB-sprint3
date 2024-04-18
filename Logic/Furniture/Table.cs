public class Table
{
    // zeker nodig
    public string ID { get; set; }
    public bool Reserved { get; set; }
    public int Type { get; set; }
    // niet zeker
    // public int AmountOfChairs { get; set; }
    // gewone constructor
    public Table(int id) // int id is een int, om het makkelijk om tafels te instantiaten
    {
        ID = $"{id}";
        Reserved = false;
    }
    // als we een tafel moeten maken dat al reserved is? niet zeker
    public Table(int id, bool reserved)
    {
        ID = $"{id}";
        Reserved = reserved;
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