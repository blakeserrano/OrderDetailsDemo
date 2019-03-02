using Newtonsoft.Json;
using OrderDetailsDemo.BL;
using OrderDetailsDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OrderDetailsDemo.Controllers
{
    public class OrderController : ApiController
    {
        public double CalculateTotal(List<Item> prices)
        {
            double totalPrice = 0.00;
            foreach(var i in prices)
            {
                totalPrice += i.quantity * i.price;
            }
            return totalPrice;
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                OrderViewModel model = OrderManager.GetOrder(id);
                model.totalCost = CalculateTotal(model.items);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
           
        }

        [HttpPost]
        public IHttpActionResult Post(OrderViewModel order)
        {
            List<string> badProducts = new List<string>();
            List<Order> allItems = new List<Order>();
            Guid OrderNumber = Guid.NewGuid();           

            foreach (var o in order.items)
            {             

                using (var client = new System.Net.Http.HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("https://vrwiht4anb.execute-api.us-east-1.amazonaws.com/default/product/" + o.productId).Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    var apiResult = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<APIResult>(result);
                    if (apiResult.statusCode != 200)
                    {
                        badProducts.Add(o.productId);
                    }
                    else
                    {
                        Order newOrder = new Order
                        {
                            OrderNumber = OrderNumber,
                            CustomerId = order.customerId,
                            ProductId = o.productId,
                            Quantity = o.quantity,
                            ProductName = apiResult.body.name,
                            ProductPrice = apiResult.body.price
                        };
                        allItems.Add(newOrder);
                    }
                }
            }
            if (badProducts.Count > 0)
            {
                return BadRequest("Your order was not able to be placed because the following product id(s) are invalid " + String.Join(",", badProducts));
            }
            else
            {
                OrderManager.CreateOrder(allItems);
                return Ok("Your order has been placed and your order number is " + OrderNumber);
            }

        }
    }
}