using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderDetailsDemo;
using OrderDetailsDemo.BL;
using OrderDetailsDemo.Controllers;
using OrderDetailsDemo.Model;

namespace OrderDetailsDemo.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void CalculateTotalTest()
        {
            List<Item> items = new List<Item>();
            items.Add(new Item { price = 6.99, quantity = 1});
            items.Add(new Item { price = 14.99, quantity = 3 });
            items.Add(new Item { price = 4.99, quantity = 2 });
            OrderController controller = new OrderController();
            double result = OrderManager.CalculateTotal(items);
            Assert.IsNotNull(result);
            Assert.AreEqual(61.94, result);
        }
    }
}
