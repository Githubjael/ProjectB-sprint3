class Table
{
    // zeker nodig
    public string ID { get; set; }
    public bool Reserved { get; set; }
    // niet zeker
    // public int Type { get; set; }
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
}