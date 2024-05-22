class TableForTwo : Table
{
    public TableForTwo(string id, int type) : base(id + "A"){
        if(type == 2)
        {
            Type = type;
        }
    }
}
