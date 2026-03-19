namespace Middleware.DTO
{
    public class ProductPhotoDto
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }

    }
}
