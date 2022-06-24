using System;
using System.Collections.Generic;

namespace BookLibrary.BLL.Models.CustomModels.OrderModel
{
    public class OrderData
    {
        public int Id { get; set; }
        public IEnumerable<OrderDetailData> OrderLines { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public string UserId { get; set; }
        //public UserData User { get; set; }
    }
}
