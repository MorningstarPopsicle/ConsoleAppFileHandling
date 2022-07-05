using System;
using ECommerceApp.Models;
using System.Collections.Generic;
namespace ECommerceApp.Repositories
{
    public class CartsRepo
    {
        private static int count = 1;
        public static List<Carts> carts = new List<Carts>();
        private readonly ProductRepo productRepo;
        private readonly CustomerRepo customerRepo;
        public CartsRepo(ProductRepo _productRepo, CustomerRepo _customerRepo)
        {

            productRepo = _productRepo;
            customerRepo = _customerRepo;
        }
        public void AddToCart(Customer customer, Products product, int quantity)
        {
            string cartNo = $"{count.ToString("00")}"; 
            int keep = 0;
            for (int i = 0; i < carts.Count; i++)
            {
                if (carts[i].ProductName == product.ProductName)
                {
                    carts[i].Quantity += quantity;
                    keep++;
                    break;
                }
            }
            if (keep == 0)
            {
                var newCart = new Carts(product.ProductName, quantity, cartNo);
                carts.Add(newCart);
            }
            Console.WriteLine($"You have successfully added your product to cart");
        }
        // public void RemoveInCart(Customer customer, Products product, int quantity)
        // {
        //     Console.Write("Enter Product name here: ");
        //     string number = Console.ReadLine();
        //     var productName = productRepo.GetProduct(number).ProductName;
        //     var newCart = new Carts( productName, quantity);
        //     for (int i = 0; i < myIndex; i++)
        //     {
        //         if (newCart.ProductName == productName)
        //         {
        //             carts.Remove(newCart);
        //         }
        //     }
        //     Console.WriteLine($"You have successfully added your product to cart");
        // }
        public void Printcart()
        {
            int i = 1;
            foreach (var item in carts)
            {
                Console.WriteLine($"{i}. {item.ProductName} {item.Quantity}");
                i++;
            }

        }
        public void ReduceProductQuantity(string productNo, int quantity)
        {
            var product = productRepo.GetProduct(productNo);
            product.Quantity -= quantity;
        }
        public decimal GetTotalPrice(List<Carts> cart)
        {
            decimal total = 0;
            foreach (var item in carts)
            {
                var product = productRepo.GetProduct(item.ProductName);
                total += (item.Quantity * product.Price);
            }
            
            return total;
        }
        
        // public Carts ClearCart()
        // {
        //     for(int i = 0; i < carts.Count; i++)
        //     {
        //         return carts[i] = null;
        //     }
        //     return null;
        // }
    }
}