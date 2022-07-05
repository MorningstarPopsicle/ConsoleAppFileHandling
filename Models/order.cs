using System;
using System.Collections.Generic;
namespace ECommerceApp.Models
{
    public  class Order
    {
        public int Id {get; set;}
        public int CustomerId {get; set;}
        public string ReferenceNo {get; set;}
        public string CustomerName {get; set;}
        public List<Carts> Carts {get; set;}
        public decimal TotalPrice {get;set;}
        public Order(int id, int customerId, string referenceNo, string customerName, List<Carts> Cart, decimal totalPrice)
        {
            Id = id;
            CustomerId = customerId;
            ReferenceNo = $"{Guid.NewGuid().ToString().Replace("-","").Substring(0,6).ToUpper()}";
            CustomerName = customerName;
            Carts = Cart;
            TotalPrice = totalPrice;
        }

       

    }
}