namespace Domain.Entities
{
    public record Order(int ProductId, int CustomerId, String Status, DateTimeOffset CreatedDate, DateTimeOffset UpdatedDate);
}

