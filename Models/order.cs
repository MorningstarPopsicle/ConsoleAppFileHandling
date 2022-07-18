using System;
using System.Collections.Generic;
using System.Text;
namespace ECommerceApp.Models
{
    public  class Order
    {
        public int Id {get; set;}
        public int CustomerId {get; set;}
        public string Email {get; set;}
        public string CustomerName {get; set;}
        // public List<Carts> Carts {get; set;}
        public decimal TotalPrice {get;set;}
        public Order(int id, int customerId, string email, string customerName, decimal totalPrice)
        {
            Id = id;
            CustomerId = customerId;
            Email = email;
            CustomerName = customerName;
            // Carts = Cart;
            TotalPrice = totalPrice;
        }
        
        internal static Order FormatLine(string line)
        {
            var props = line.Split('\t');
            return new Order(int.Parse(props[0]), int.Parse(props[1]), props[2], props[3],  decimal.Parse(props[4]) );

        }
        

       

    }
}