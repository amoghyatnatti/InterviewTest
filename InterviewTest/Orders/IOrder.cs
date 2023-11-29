using System.Collections.Generic;
using InterviewTest.Customers;
using System;

namespace InterviewTest.Orders
{
    public interface IOrder
    {
        ICustomer Customer { get; }
        string OrderNumber { get; }
        List<OrderedProduct> Products { get; }
        DateTime Date { get; }
        void AddProduct(Products.IProduct product);
    }
}