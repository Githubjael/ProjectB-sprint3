class TableForTwo : Table
{
    new public int Type {
        get => base.Type;
        set{
            // als de value van Type positief is en 2 dan wordt er een Char toegevoegd bij ID
            if(int.IsPositive(value) && value == 2){
                base.Type = value;
                ID += "A";
            }
            // niet zeker
            // else{
            //     throw new ArgumentException();
            // }
        }
    }
    public TableForTwo(int id, int type) : base(id, type){
        Type = type;
    }
}