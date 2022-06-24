namespace BookLibrary.Web.Models.CustomModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
