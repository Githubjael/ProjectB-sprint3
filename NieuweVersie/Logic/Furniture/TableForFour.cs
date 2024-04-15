class TableForFour : Table
{
    private int _type;
    public int Type {
        get => _type;
        set{
            // als de value van Type positief is en 4 dan wordt er een Char toegevoegd bij ID
            if(int.IsPositive(value) && value == 4){
                _type = value;
                ID += "B";
            }
            // niet zeker
            // else{
            //     throw new ArgumentException();
            // }
        }
    }
    public TableForFour(int id, int type) : base(id, false){
        Type = type;
    }
}