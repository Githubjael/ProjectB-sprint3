public abstract class Table
{
    // zeker nodig
    public string ID { get; set; }
    public bool Reserved { get; set; }
    public int Type { get; set; }
    // niet zeker
    // public int AmountOfChairs { get; set; }
    // gewone constructor
    public Table(string id) // int id is een int, om het makkelijk om tafels te instantiaten
    {
        ID = $"{id}";
        Reserved = false;
    }
    public abstract void IsReserved();
    public abstract void Cancelled();
}
