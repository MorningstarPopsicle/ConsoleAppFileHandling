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
        internal static Products FormatLine(string line)
        {
            var props = line.Split('\t');
            return new Products(int.Parse(props[0]), props[1], props[2],  decimal.Parse(props[3]), int.Parse(props[4]) );

        }

        public int Id {get; set;}
        public string ProductNo {get; set;}
        public decimal Price {get; set;}
        public string ProductName {get; set;}
        public int Quantity {get; set;}

    }
}