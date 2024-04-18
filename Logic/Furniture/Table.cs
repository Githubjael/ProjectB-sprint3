public class Table
{
    // zeker nodig
    public string ID { get; set; }
    public bool Reserved { get; set; }
    public virtual int Type { get; set; }
    // niet zeker
    // public int AmountOfChairs { get; set; }
    // gewone constructor
    public Table(string id) // int id is een int, om het makkelijk om tafels te instantiaten
    {
        ID = $"{id}";
        Reserved = false;
    }
    // als we een tafel moeten maken dat al reserved is? niet zeker
        public void IsReserved()
    {
        Reserved = true;
    }
    public void Cancelled()
    {
        Reserved = false;
    }
}
