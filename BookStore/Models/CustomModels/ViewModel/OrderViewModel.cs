using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Web.Models.CustomModels.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your zip code")]
        [Display(Name = "Zip code")]
        [StringLength(10, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter date of return")]
        [Range(typeof(DateTime), "19/4/2022", "14/4/2023")]
        public DateTime ReturnDate { get; set; }

        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }
        public bool IsReturned { get; set; }
        public string UserId { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderLines { get; set; }
    }
}