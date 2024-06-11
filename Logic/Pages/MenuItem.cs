public class MenuItem{
    public string Id;
    public string Name;
    public double Price;
    public string Category;
    public string Symbol;
    public List<string> Ingredients;

    public MenuItem(string id, string name, double price, string category, List<string> ingredients, string symbol){
        Id = id;
        Name = name;
        Price = price;
        Category = category;
        Ingredients = ingredients;
        Symbol = symbol;
    }
}
