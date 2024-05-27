class TableForTwo : Table
{
    public TableForTwo(string id, int type) : base(id + "B"){
        if(type == 2)
        {
            Type = type;
        }
    }
}
