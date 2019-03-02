using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderDetailsDemo.Model;
using System.Configuration;

namespace OrderDetailsDemo.DAL
{
    public static class OrderDAL
    {
        private static string connectionString = ConfigurationManager.AppSettings["LiteDbLocation"];

        public static bool CreateOrder(List<Order> newOrder)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    foreach (var o in newOrder)
                    {
                        // Get customer collection
                        var customers = db.GetCollection<Customer>("Customers");
                        var orders = db.GetCollection<Order>("Orders");
                        var results = customers.FindById(o.CustomerId);
                        if (results == null)
                        {
                            // Create a new customer
                            var newCustomer = new Customer
                            {
                                CustomerId = o.CustomerId
                            };
                            customers.Insert(newCustomer);

                        }
                        //Create a new order for this customer    
                        o._id = ObjectId.NewObjectId();
                        orders.Insert(o);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //Todo add logging
                throw new Exception("There was an error processing your order. If this problem persists please contact customer service");
            }

        }

        public static OrderViewModel GetOrder(Guid id)
        {
            try
            {
                OrderViewModel results = new OrderViewModel();
                using (var db = new LiteDatabase(connectionString))
                {
                    var orders = db.GetCollection<Order>("Orders");
                    results.items.AddRange(orders.Find(x => x.OrderNumber == id).Select(y => new Item { name = y.ProductName, price = y.ProductPrice, productId = y.ProductId, quantity = y.Quantity }));
                    results.customerId = orders.FindOne(x => x.OrderNumber == id).CustomerId;

                }
                return results;
            }
            catch (Exception ex)
            {
                //Todo add logging
                throw new Exception("There was an error finding your order.Please enter another order number or Please contact customer service");
            }
        }

    }
}
