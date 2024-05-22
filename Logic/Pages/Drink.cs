public class Drink
{
    public string Name { get; }
    public List<string> Ingredients { get; }

    public Drink(string name, List<string> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
}
