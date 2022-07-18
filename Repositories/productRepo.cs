using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class ProductRepo
    {
        private static int count = 1;
        public static List<Products> products;
        public ProductRepo()
        {
            products = new List<Products>();
            string path = "Product.txt";
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path: "Product.txt");
                foreach (var item in lines)
                {
                    var productNew = Products.FormatLine(item);
                    products.Add(productNew);
                }
            }

            var product = new Products(1, "01", "Sardine", 700, 12);
            products.Add(product);
            count++;

        }
        public string AddProduct(string productName, decimal price, int quantity)
        {
            string productNo = $"{count.ToString("00")}";
            var newProduct = new Products(count, productNo, productName, price, quantity);
            int keep = 0;
            foreach (var item in products)
            {
                if (item.ProductName == newProduct.ProductName)
                {
                    item.Quantity += newProduct.Quantity;
                    keep++;
                    break;
                }

            }
            if(keep == 0)
            {
            products.Add(newProduct);
            Console.WriteLine($"You have successfully added {newProduct.ProductName}");
            count++;
            }
             return newProduct.ProductNo;
             
        }

        // public void RemoveProduct(string productName)
        // {
        //     foreach(var item in products)
        //     {
        //         if (item.ProductName == productName)
        //         {
        //             item = null;
        //         }
        //     }
        //     Console.WriteLine($"You have successfully removed a product ");

        // }

        public void EditProduct(string productNo, string productName, decimal price, int quantity)
        {
            var product = GetProduct(productNo);
            product.Price = price;
            product.ProductName = productName;
            product.Quantity = quantity;
            Console.WriteLine($"The product has been edited. It is now{product.ProductName} {product.Price} {product.Quantity}");
        }


        public void ReduceQuantity(int quantity, string productid)
        {
            var product = GetProduct(productid);
            product.Quantity -= quantity;
        }
        public void ReturnProducts()
        {
            // Console.WriteLine($"MyIndex is {myIndex}");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.ProductNo}. Product Name: {item.ProductName} Price: {item.Price} Quantity: {item.Quantity} ");
               
            }

        }
        public Products GetProduct(string productName)
        {
            foreach (var item in products)
            {
                if (item != null && item.ProductName.ToUpper() == productName.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }
    }
}