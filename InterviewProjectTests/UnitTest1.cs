using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InterviewProjectTests
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly InterviewTest.Orders.OrderRepository orderTruckRepo = new InterviewTest.Orders.OrderRepository();
        private static readonly InterviewTest.Returns.ReturnRepository returnTruckRepo = new InterviewTest.Returns.ReturnRepository();
        private static readonly InterviewTest.Orders.OrderRepository orderCarRepo = new InterviewTest.Orders.OrderRepository();
        private static readonly InterviewTest.Returns.ReturnRepository returnCarRepo = new InterviewTest.Returns.ReturnRepository();
        [TestMethod]
        public void OrderTruckAccessoriesTest()
        {
            var customer = GetTruckAccessoriesCustomer();

            InterviewTest.Orders.IOrder order = new InterviewTest.Orders.Order("TruckAccessoriesOrder1234", customer);
            order.AddProduct(new InterviewTest.Products.HitchAdapter());
            order.AddProduct(new InterviewTest.Products.BedLiner());
            customer.CreateOrder(order);

            float order_total = customer.GetTotalSales();
            
            Assert.AreEqual(order_total, 220.0F);
            
        }

        [TestMethod]
        public void OrderCarDealershipTest()
        {
            var customer = GetCarDealershipCustomer();

            InterviewTest.Orders.IOrder order = new InterviewTest.Orders.Order("CarDealerShipOrder1234", customer);
            order.AddProduct(new InterviewTest.Products.ReplacementBumper());
            order.AddProduct(new InterviewTest.Products.SyntheticOil());
            customer.CreateOrder(order);

            float order_total = customer.GetTotalSales();

            Assert.AreEqual(order_total, 180.0F);

        }

        [TestMethod]
        public void ReturnTruckAccessoriesTest()
        {
            var customer = GetTruckAccessoriesCustomer();

            InterviewTest.Orders.IOrder order = new InterviewTest.Orders.Order("TruckAccessoriesOrder123", customer);
            order.AddProduct(new InterviewTest.Products.HitchAdapter());
            order.AddProduct(new InterviewTest.Products.BedLiner());
            customer.CreateOrder(order);

            InterviewTest.Returns.IReturn rga = new InterviewTest.Returns.Return("TruckAccessoriesOrder123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            float return_total = customer.GetTotalReturns();

            Assert.AreEqual(return_total, 70.0F);


        }
        
        [TestMethod]
        public void ReturnCarAccessoriesTest()
        {
            var customer = GetCarDealershipCustomer();

            InterviewTest.Orders.IOrder order = new InterviewTest.Orders.Order("CarDealerShipOrder123", customer);
            order.AddProduct(new InterviewTest.Products.ReplacementBumper());
            order.AddProduct(new InterviewTest.Products.SyntheticOil());
            customer.CreateOrder(order);

            InterviewTest.Returns.IReturn rga = new InterviewTest.Returns.Return("CarDealerShipReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            float return_total = customer.GetTotalReturns();

            Assert.AreEqual(return_total, 155.0F);
        }
        private static InterviewTest.Customers.ICustomer GetTruckAccessoriesCustomer()
        {
            return new InterviewTest.Customers.TruckAccessoriesCustomer(orderTruckRepo, returnTruckRepo);
        }

        private static InterviewTest.Customers.ICustomer GetCarDealershipCustomer()
        {
            return new InterviewTest.Customers.CarDealershipCustomer(orderCarRepo, returnCarRepo);
        }
    }
}
