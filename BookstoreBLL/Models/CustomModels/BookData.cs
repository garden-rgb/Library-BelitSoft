using System.ComponentModel.DataAnnotations;

namespace BookLibrary.BLL.Models.CustomModels
{
    public class BookData
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public int Year { get; set; }
        public int InStock { get; set; }

    }
}
