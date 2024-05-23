public class MenuItem{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public string Symbol { get; set; }
    public List<string> Ingredients { get; }

    public MenuItem(string name, double price, string category, List<string> ingredients){
        Name = name;
        Price = price;
        Category = category;
        Ingredients = ingredients;

    }
        public MenuItem(string name, double price, string category, string symbol){
        Name = name;
        Price = price;
        Category = category;
        Symbol = symbol;
    }
//MAYBE DOESNT NEED TO bE OVERLOADED
}
