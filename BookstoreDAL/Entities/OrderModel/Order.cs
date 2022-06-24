using System;
using System.Collections.Generic;

namespace BookLibrary.DAL.Entities.OrderModel
{
    public class Order
    {
        public int Id { get; set; }
        public IEnumerable<OrderDetail> OrderLines { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public string UserId { get; set; }
    }
}
