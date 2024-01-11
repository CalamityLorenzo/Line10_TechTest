namespace Database.DbEntities
{
   internal class ProductDb
    {
        public int Id { get; set; } 
        public String Name { get; set; }
        public String Description { get; set; }
        public String SKU { get; set; }
        public List<OrderDb> ProductOrders { get; } = new();
    }
}
