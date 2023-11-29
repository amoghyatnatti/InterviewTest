using System.Collections.Generic;
using InterviewTest.Customers;

namespace InterviewTest.Orders
{
    public interface IOrder
    {
        ICustomer Customer { get; }
        string OrderNumber { get; }
        List<OrderedProduct> Products { get; }
        DateTime date1 { get; }
        void AddProduct(Products.IProduct product);
    }
}