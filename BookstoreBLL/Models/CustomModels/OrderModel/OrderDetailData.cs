
namespace BookLibrary.BLL.Models.CustomModels.OrderModel
{
    public class OrderDetailData
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual BookData Book { get; set; }
        public virtual OrderData Order { get; set; }

    }
}
