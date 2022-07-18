using System;
using ECommerceApp.Models;
using System.Collections.Generic;
namespace ECommerceApp.Repositories
{
    public class CartsRepo
    {
        private static int count = 1;
        public static List<Carts> carts;
        private readonly ProductRepo productRepo;
        private readonly CustomerRepo customerRepo;
        public CartsRepo(ProductRepo _productRepo, CustomerRepo _customerRepo)
        {

            productRepo = _productRepo;
            customerRepo = _customerRepo;

            carts = new List<Carts>();
            string path = "Carts.txt";
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path: "Carts.txt");
                foreach (var item in lines)
                {
                    var cartNew = Carts.FormatLine(item);
                    carts.Add(cartNew);
                }
            }
        }
        public void AddToCart(Customer customer, Products product, int quantity)
        {
            
            string cartNo = $"{count.ToString("00")}";
            int keep = 0;
            foreach (var item in carts)
            {
                if (customer.Email == CustomerRepo.customerLogIn && item.ProductName == product.ProductName)
                {
                    item.Quantity += quantity;
                    keep++;
                    break;
                }
            }

            if (keep == 0)
            {
                var newCart = new Carts(product.ProductName, quantity, cartNo, customer.Email);
                carts.Add(newCart);
                Console.WriteLine($"You have successfully added your product to cart");
            }


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
        public void Printcart(List<Carts> cart)
        {

            foreach (var item in cart)
            {
                int i = 1;
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
            foreach (var item in cart)
            {
                var product = productRepo.GetProduct(item.ProductName);
                decimal c = item.Quantity * product.Price;
                total += c;
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