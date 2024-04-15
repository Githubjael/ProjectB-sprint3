class TableForTwo : Table
{
    private int _type;
    public int Type {
        get => _type;
        set{
            // als de value van Type positief is en 2 dan wordt er een Char toegevoegd bij ID
            if(int.IsPositive(value) && value == 2){
                _type = value;
                ID += "A";
            }
            // niet zeker
            // else{
            //     throw new ArgumentException();
            // }
        }
    }
    public TableForTwo(int id, int type) : base(id, false){
        Type = type;
    }
}