class BarTable : Table{
    private int _type;
    public override int Type {
        get => _type;
        set {
            // als de value van Type positief is en 1 dan wordt er een Char toegevoegd bij ID
            if (int.IsPositive(value) && value == 1){
                ID += "D";
            }
        }
    }

    public BarTable(string id, int type) : base(id){
        Type = type;
    }
}
