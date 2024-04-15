class TableForSix : Table
{
    private int _type;
    public int Type {
        get => _type;
        set{
            // als de value van Type positief is en 6 dan wordt er een Char toegevoegd bij ID
            if(int.IsPositive(value) && value == 6){
                _type = value;
                ID += "C";
            }
            // niet zeker
            // else{
            //     throw new ArgumentException();
            // }
        }
    }
    public TableForSix(int id, int type) : base(id, false){
        Type = type;
    }
}