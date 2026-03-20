namespace TeaShop.DTO
{
    public class TeaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
