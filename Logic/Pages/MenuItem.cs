public class MenuItem{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public string Symbol { get; set; }
    public List<string> Ingredients { get; }

    public MenuItem(string id, string name, double price, string category, List<string> ingredients, string symbol){
        Id = id;
        Name = name;
        Price = price;
        Category = category;
        Ingredients = ingredients;
        Symbol = symbol;
    }
}
