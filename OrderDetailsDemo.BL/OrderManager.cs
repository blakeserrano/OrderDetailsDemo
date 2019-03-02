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
