public class MenuItem{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    
    public List<string> Ingredients { get; }

    public MenuItem(string name, double price, string category, List<string> ingredients){
        Name = name;
        Price = price;
        Category = category;
        Ingredients = ingredients;

    }
//CHANGE CATEGORY
}
