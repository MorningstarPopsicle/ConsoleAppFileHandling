using System;
namespace ECommerceApp.Models
{
    public  class Carts
    {
        public  string ProductName {get; set;}
    
        public int Quantity {get; set;}
        public  string CartId {get; set;}
        public string CustomerEmail {get; set;}
        
       
        public Carts(string productName, int quantity, string cartId, string customerEmail)
        {
           
           ProductName = productName;
           Quantity = quantity;
           CartId = cartId;
           CustomerEmail = customerEmail;
        }
        internal static Carts FormatLine(string line)
        {
            var props = line.Split('\t');
            return new Carts(props[0], int.Parse(props[1]), props[2], props[3]);

        }

        

    }
}