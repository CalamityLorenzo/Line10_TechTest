namespace Domain.Entities
{
    public record Customer(int Id, String FirstName, String LastName, String Phone, String Email)
    {
        public Customer(String FirstName, String LastName, String Phone, String Email): this(0, FirstName, LastName, Phone, Email) { }
    }
    
    public record Product(int Id, String Name, String Description, String SKU);

    public record Order(int ProductId, int CustomerId, String Status, DateTimeOffset CreatedDate, DateTimeOffset UpdatedDate);
    
}
