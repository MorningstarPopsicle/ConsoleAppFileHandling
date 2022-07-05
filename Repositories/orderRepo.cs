using System;
using ECommerceApp.Models;
using System.Collections.Generic;
using System.Text;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class OrderRepo
    {

        public static int myIndex = 0;
        private static int count = 1;
        private readonly ProductRepo productRepo;
        private readonly CustomerRepo customerRepo;
        private readonly StaffRepo staffRepo;
        private readonly CartsRepo cartsRepo;
        List<Carts> cart = new List<Carts>();
        public Order[] orders = new Order[50];
        public OrderRepo(ProductRepo _productRepo, CustomerRepo _customerRepo,
                          CartsRepo _cartsRepo,
                         StaffRepo _staffRepo)
        {
            productRepo = _productRepo;
            customerRepo = _customerRepo;
            cartsRepo = _cartsRepo;
            staffRepo = _staffRepo;
        }
        public void PlaceOrder(int id, int customerId, string referenceNo, string customerName, List<Carts> cart, decimal totalPrice)
        {
            var newOrder = new Order(id, customerId, referenceNo, customerName, cart, totalPrice);
            orders[myIndex] = newOrder;
            Console.WriteLine($"You have successfully ordered your product");
            count++;
            myIndex++;
        }
        public void BuyProduct(Customer customer)
        {
            string allAvailable = GetAvailableProduct();
            Console.Write("Please choose a product from the available product above: ");
            string chosenProduct;
            do
            {
                chosenProduct = Console.ReadLine().ToUpper();
            }
            while (!IsAvailable(allAvailable, chosenProduct));
            var product = productRepo.GetProduct(chosenProduct);
            Console.Write("Enter quantity here: "); ;
            int quantity = int.Parse(Console.ReadLine());
            cartsRepo.AddToCart(customer, product, quantity);

            Console.WriteLine($"You have successfully addedd {product.ProductName} to cart. \tEnter 1 To add more products to cart\tEnter 2 To remove product from cart\t 0 To continue");
            int option;
            bool makePayment = false;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Enter 1, 2 or 0");
            }
            if (option == 1)
            {
                Console.Write("Enter email here: ");
                string email = Console.ReadLine();
                var currentCustomer = customerRepo.GetCustomer(email);
                BuyProduct(currentCustomer);
            }
            else if (option == 2)
            {
                ReduceProduct(cart);
                makePayment = true;
            }
            else if(option == 0)
            {
                makePayment = true;
            }
            
            if (makePayment)
            {
                cartsRepo.Printcart();
                var bill = PrintBill();
                Console.WriteLine($"Your bill is {bill}");
                if (customer.Wallet >= bill)
                {
                    customer.Wallet -= bill;

                    PayToManager();
                    Console.WriteLine("Thanks for your patronage");
                    ReduceProductQuantity(cart);
                    
                }
                else
                {
                    bool flag = false;
                    while (customer.Wallet < bill)
                    {
                        Console.Write("Insufficient balance, Please enter 1 to fund your Wallet or any other key to cancel order:");
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            customerRepo.FundWallet(customer);
                        }
                        else
                        {
                            flag = true;
                        }
                    
                        if (flag)
                        {
                            break;
                        }
                    }
                    
                    if (!flag)
                    {
                        customer.Wallet -= bill;
                        PayToManager();
                        Console.WriteLine("Thanks for your patronage");
                        ReduceProductQuantity(cart);
                        
                    }
                }
            }


        }

        public string GetAvailableProduct()
        {
            StringBuilder allAvailable = new StringBuilder();
            for (int i = 0; i < ProductRepo.myIndex; i++)
            {
                var product = ProductRepo.products[i];
                if (product != null && product.Quantity > 0)
                {
                    Print(product);
                    allAvailable.Append($"{product.ProductName.ToUpper()},");
                }
            }
            return allAvailable.ToString();
        }
        private void Print(Products product)
        {
            Console.WriteLine($"{product.ProductNo} {product.ProductName}\t{product.Price}");
        }
        private bool IsAvailable(string all, string choosed)
        {
            var allSplit = all.Split(",");
            return allSplit.Contains(choosed);
        }
        public void ReduceProduct(List<Carts> cart)
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            foreach (var item in cart)
            {
                if (item.ProductName == productName && item.Quantity > quantity)
                {
                    item.Quantity -= quantity;
                }
            }
        }
        public decimal PrintBill()
        {
            var amount = cartsRepo.GetTotalPrice(cart);
            return amount;
        }
        public void PayToManager()
        {
            Console.Write("Enter Manager's email here: ");
            string eMail = Console.ReadLine();
            var manager = staffRepo.GetManager(eMail);
            var amount = PrintBill();
            manager.Wallet += amount;
            Console.WriteLine("Payment successful");
        }
        public void ReduceProductQuantity(List<Carts> cart)
        {
            foreach (var item in cart)
            {
                var product = productRepo.GetProduct(item.ProductName);
                product.Quantity -= item.Quantity;

            }


        }
        public decimal PrintCustomerOrder(int customerId)
        {
            Console.Write("Enter Reference number here: ");
            string refNo = Console.ReadLine();
            decimal totalPrice = 0;
            for (int i = 0; i < myIndex; i++)
            {
                if (orders[i].CustomerId == customerId)

                {
                    Console.WriteLine($"{orders[i].CustomerName} {orders[i].TotalPrice}");
                    totalPrice += orders[i].TotalPrice;
                }
            }
            return totalPrice;

        }
        public void PrintAllOrder()
        {
            for (int i = 0; i < myIndex; i++)
            {
                int count = 1;
                if (orders[i] != null)
                {
                    Console.WriteLine($"{count}{orders[i].CustomerName}\t {orders[i].TotalPrice}");
                    count++;
                }

            }
        }
        public Order GetOrder(string refNo)
        {
            for (int i = 0; i < myIndex; i++)
            {
                if (orders[i] != null && orders[i].ReferenceNo == refNo)
                {
                    return orders[i];
                }
            }
            return null;

        }
        
    }
}



