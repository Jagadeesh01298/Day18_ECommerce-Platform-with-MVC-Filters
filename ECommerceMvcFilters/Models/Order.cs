namespace ECommerceMvcFilters.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }
    }
}
