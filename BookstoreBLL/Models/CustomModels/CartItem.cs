namespace BookLibrary.BLL.Models.CustomModels
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public virtual BookData Book { get; set; }
        public string ShoppingCartId { get; set; }
        public string UserName { get; set; }
    }
}
