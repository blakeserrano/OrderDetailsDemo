using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace OrderDetailsDemo.Model
{
    public class Customer
    {
        public string CustomerId { get; set; }
    }
    public class Item
    {
        public string productId { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public double price { get; set; }
    }

    public class Order
    {
        public ObjectId _id { get; set; }
        public Guid OrderNumber { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderViewModel
    {
        [Required(ErrorMessage = "Customer Id is required.")]
        public string customerId { get; set; }
        [Required(ErrorMessage = "Items are required.")]
        public List<Item> items = new List<Item>();
        public double totalCost { get; set; }
    }

    public class APIResult
    {
        public int statusCode { get; set; }
        public Item body { get; set; }
    }
}
