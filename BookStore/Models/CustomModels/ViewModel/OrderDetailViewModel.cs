namespace BookLibrary.Web.Models.CustomModels.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual BookViewModel Book { get; set; }
    }
}
