using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.DAL.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public int Year { get; set; }
        public int InStock { get; set; }
        
    }
}
