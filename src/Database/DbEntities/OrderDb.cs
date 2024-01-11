namespace Database.DbEntities
{
    internal class OrderDb
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public String Status { get;set; }
        public DateTimeOffset CreateDate { get; set; }  
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
