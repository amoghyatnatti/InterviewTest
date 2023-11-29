using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;
using InterviewTest.Products;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales()
        {
            float total_sales = 0.0F;
            List<IOrder> order_list = _orderRepository.Get();
            for (var i = 0; i < order_list.Count; i++)
            {
                IOrder order = order_list[i];
                List<OrderedProduct> prod_list = order.Products;
                for (var j = 0; j < prod_list.Count; j++) 
                {
                    IProduct prod =  prod_list[j].Product;
                    total_sales += prod.GetSellingPrice();
                }
            }
            return total_sales;
        }

        public float GetTotalReturns()
        {
            float total_returns = 0.0F;
            List<IReturn> return_list = _returnRepository.Get();
            for (var i = 0; i < return_list.Count; i++)
            {
                List<ReturnedProduct> returned_prods = return_list[i].ReturnedProducts;
                for (var j = 0; j < returned_prods.Count; j++)
                {
                    OrderedProduct ordered_prod = returned_prods[j].OrderProduct;
                    IProduct prod = ordered_prod.Product;
                    total_returns += prod.GetSellingPrice();
                }
            }
            return total_returns;
        }

        public float GetTotalProfit()
        {
            return GetTotalSales() - GetTotalReturns();
        }
    }
}
