class TableForSix : Table
{
        new public int Type {
        get => base.Type;
        set{
            // als de value van Type positief is en 4 dan wordt er een Char toegevoegd bij ID
            if(int.IsPositive(value) && value == 6){
                base.Type = value;
                ID += "B";
            }
            // niet zeker
            // else{
            //     throw new ArgumentException();
            // }
        }
    }
    public TableForSix(int id, int type) : base(id, type){
        Type = type;
    }
}