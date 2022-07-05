using System;
namespace ECommerceApp.Models
{
    public  class Carts
    {
        public  string ProductName {get; set;}
    
        public int Quantity {get; set;}
        public  string CartId {get; set;}
        
       
        public Carts(string productName, int quantity, string cartId)
        {
           
           ProductName = productName;
           Quantity = quantity;
           CartId = cartId;
           
           

        }

        

    }
}