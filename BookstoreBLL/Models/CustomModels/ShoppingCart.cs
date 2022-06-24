//using BookstoreDAL.DatabaseSecret;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Models.CustomModels
{
    public class ShoppingCart
    {
        private List<CartItem> itemCollection = new List<CartItem>();
        public void AddItem(BookData book, string userName, int quantity)
        {
            CartItem item = itemCollection
                .Where(g => g.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (item == null)
            {
                itemCollection.Add(new CartItem
                {
                    Book = book,
                    Quantity = quantity,
                    UserName = userName
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveLine(BookData book)
        {
            itemCollection.RemoveAll(l => l.Book.BookId == book.BookId);
        }

        public decimal ComputeTotalValue()
        {
            return itemCollection.Sum(e => e.Book.Price * e.Quantity);
        }

        public void Clear()
        {
            itemCollection.Clear();
        }

        public IEnumerable<CartItem> Items => itemCollection;

    }
}

