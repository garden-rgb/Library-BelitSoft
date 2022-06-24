using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Web.Models.CustomModels.ViewModel
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public int Year { get; set; }
        public string Total { get => (Price * InStock).ToString(); }
        public int Amount { get; set; } = 1;
        public int InStock { get; set; }

    }
}
