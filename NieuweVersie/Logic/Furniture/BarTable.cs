class BarTable : Table{
    private int _type;
    public int Type {
        get => _type;
        set {
            // als de value van Type positief is en 1 dan wordt er een Char toegevoegd bij ID
            if (int.IsPositive(value) && value == 1){
                ID += "D";
            }
        }
    }

    public BarTable(int id, int type) : base(id, false){
        Type = type;
    }
}