using System.Collections.Generic;
using InterviewTest.Customers;
using InterviewTest.Products;
using System;

namespace InterviewTest.Orders
{
    public class Order : IOrder
    {
        public Order(string orderNumber, ICustomer customer)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            Products = new List<OrderedProduct>();
            Date = DateTime.Today;
        }

        public string OrderNumber { get; }
        public ICustomer Customer { get; }
        public List<OrderedProduct> Products { get; }
        public DateTime Date { get; }
        public void AddProduct(IProduct product)
        {
            Products.Add(new OrderedProduct(product));
        }
    }
}
