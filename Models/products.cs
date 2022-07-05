using System;
namespace ECommerceApp.Models
{
    public  class Products
    {
        public Products(int id,  string productNo, string productName, decimal price, int quantity)
        {
            Id = id;
            ProductNo = productNo;
            Price = price;
            ProductName = productName;
            Quantity = quantity;

        }

        public int Id {get; set;}
        public string ProductNo {get; set;}
        public decimal Price {get; set;}
        public string ProductName {get; set;}
        public int Quantity {get; set;}

    }
}