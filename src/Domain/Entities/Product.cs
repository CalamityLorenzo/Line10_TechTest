namespace Domain.Entities
{
    public record Product(int Id, String Name, String Description, String SKU)
    {
        public Product(String Name, String Description, String SKU) : this(0, Name, Description, SKU) { }
    }

}
