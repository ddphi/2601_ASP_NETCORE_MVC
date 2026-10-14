namespace Lesson09.Lab.Models.DevXuongMoc
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Total => Price * Quantity;
    }
}