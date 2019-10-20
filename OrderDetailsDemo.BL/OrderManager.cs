using OrderDetailsDemo.DAL;
using OrderDetailsDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDetailsDemo.BL
{
    public static class OrderManager
    {
        public static double CalculateTotal(List<Item> prices)
        {
            double totalPrice = 0.00;
            foreach (var i in prices)
            {
                totalPrice += i.quantity * i.price;
            }
            return totalPrice;
        }

        public static bool CreateOrder(List<Order> order)
        {
            return OrderDAL.CreateOrder(order);
        }

        public static OrderViewModel GetOrder(Guid id)
        {
            return OrderDAL.GetOrder(id);
        }
    }
}
