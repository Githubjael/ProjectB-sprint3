class TableForFour : Table
{
    public TableForFour(string id, int type) : base(id + "C"){
        if (type == 4)
        {
            Type = type;
        }
    }
}
