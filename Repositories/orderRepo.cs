using System;
using ECommerceApp.Models;
using ECommerceApp.Menu;
using System.Collections.Generic;
using System.Text;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class OrderRepo
    {

        private static int count = 1;
        private readonly ProductRepo productRepo;
        private readonly CustomerRepo customerRepo;
        private readonly StaffRepo staffRepo;
        private readonly CartsRepo cartsRepo;
        // private readonly CustomerMenu customerMenu;
        public static List<Carts> cart;
        public List<Order> orders;
        public OrderRepo(ProductRepo _productRepo, CustomerRepo _customerRepo,
                          CartsRepo _cartsRepo,
                         StaffRepo _staffRepo)
        {
            productRepo = _productRepo;
            customerRepo = _customerRepo;
            cartsRepo = _cartsRepo;
            staffRepo = _staffRepo;
            // customerMenu = _customerMenu;
            orders = new List<Order>();
            string path = "Orders.txt";
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path: "Orders.txt");
                foreach (var item in lines)
                {
                    var orderNew = Order.FormatLine(item);
                    orders.Add(orderNew);
                }
            }
        }
        public void PlaceOrder(int id, int customerId, string referenceNo, string customerName, decimal totalPrice)
        {
            var newOrder = new Order(id, customerId, referenceNo, customerName, totalPrice);
            orders.Add(newOrder);
            // Console.WriteLine($"You have successfully ordered your product");
            count++;
        }
        public void BuyProduct(Customer customer)
        {
            // bool leave = false;
            string allAvailable = GetAvailableProduct();
            //  if (allAvailable == null)
            // {
            //    while(!leave)
            //     {
            //         Console.WriteLine("There are no products available \n Enter any key to return to menu");
            //         Console.ReadKey();
            //         Console.WriteLine();
            //         leave = true;
            //     }
            // }
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

            Console.WriteLine($"You have successfully added {product.ProductName} to cart. \tEnter 1 To add more products to cart\tEnter 2 To remove product from cart\t 0 To return to menu");
            ReduceProductQuantity(CartsRepo.carts);
            int option;
            bool exit = false;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Enter 1, 2 or 0");

            }
            while (!exit)
            {
                if (option == 1)
                {
                    BuyProduct(customer);
                    exit = true;
                }
                else if (option == 2)
                {
                    ReduceProduct(CartsRepo.carts);
                    exit = true;
                    // makePayment = true;
                }
                else if (option == 0)
                {
                    exit = true;
                    // customerMenu.CustomerSubMenu();
                    // makePayment = true;

                }
            }

        }
        public void ViewCart()
        {
            cartsRepo.Printcart(CartsRepo.carts);
            Console.WriteLine("Do you want to make payments? If yes, click 1 and if no, click 0 ");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Enter 1 or 0");
            }
            if (option == 1)
            {
                Pay();
            }
        }
        public void LogOut()
        {
            var customer = customerRepo.GetCustomer(CustomerRepo.customerLogIn);
            customer.Email = null;
        }
        public void Pay()
        {
            // bool flag = false;
            bool exit = false;
            var customer = customerRepo.GetCustomer(CustomerRepo.customerLogIn);
            var bill = PrintBill();
            Console.WriteLine($"Your bill is {bill}");
            while (!exit)
            {
                if (customer.Wallet >= bill)
                {
                    customer.Wallet -= bill;

                    PayToManager();
                    Console.WriteLine($"Thanks for your patronage, your new balance is {customer.Wallet}");
                    PlaceOrder(count, customer.Id, customer.Email, customer.FullName(), bill);
                    CartsRepo.carts.Clear();
                    exit = true;
                    // customerMenu.CustomerSubMenu();
                }
                else
                {
                    while (!exit)
                    {
                        while (customer.Wallet < bill)
                        {
                            Console.Write("Insufficient balance, Please enter 1 to fund your Wallet or 0 to cancel order: ");
                            int option;
                            if (!int.TryParse(Console.ReadLine(), out option))
                            {
                                Console.WriteLine("Invalid input. Enter 1 or 0");
                            }
                            if (option == 1)
                            {
                                customerRepo.FundWallet(customer);
                            }
                            else if (option == 0)
                            {
                                exit = true;
                                break;
                            }
                        }
                        if (!exit)
                        {
                            customer.Wallet -= bill;
                            PayToManager();
                            Console.WriteLine($"Thanks for your patronage, your new balance is {customer.Wallet}");
                            PlaceOrder(count, customer.Id, customer.Email, customer.FullName(), bill);
                            CartsRepo.carts.Clear();
                            exit = true;
                            // customerMenu.CustomerSubMenu();
                        }
                    }



                }
            }

        }

        public string GetAvailableProduct()
        {
            StringBuilder allAvailable = new StringBuilder();
            foreach (var item in ProductRepo.products)
            {
                if (item != null && item.Quantity > 0)
                {
                    Print(item);
                    allAvailable.Append($"{item.ProductName.ToUpper()},");
                }
            }
            return allAvailable.ToString();
        }
        private void Print(Products product)
        {
            
                Console.WriteLine($"{product.Id} {product.ProductName}\t{product.Price}");
            
        }
        private bool IsAvailable(string all, string choosed)
        {
            var allSplit = all.Split(",");
            return allSplit.Contains(choosed);
        }
        public void ReduceProduct(List<Carts> cart)
        {
            cartsRepo.Printcart(CartsRepo.carts);
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
            var amount = cartsRepo.GetTotalPrice(CartsRepo.carts);
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
        public void PrintCustomerOrder()
        {
            Console.Write("Enter customer's ID here: ");
            int iD = int.Parse(Console.ReadLine());
            decimal totalPrice = 0;
            foreach (var item in orders)
            {
                if (item.CustomerId == iD)

                {

                    Console.WriteLine($"{item.CustomerName} {item.TotalPrice}");
                    totalPrice += item.TotalPrice;
                }
            }
            //  return customerId;

        }
        public void PrintAllOrder()
        {
            int count = 1;
            foreach (var item in orders)
            {

                if (item != null)
                {

                    Console.WriteLine($"{count}. {item.CustomerName}\t {item.TotalPrice}");
                    count++;
                }

            }
        }
        public Order GetOrder()
        {
            Console.WriteLine("Enter customer email here: ");
            var email = Console.ReadLine();
            foreach (var item in orders)
            {
                if (item != null && item.Email == email)
                {
                    return item;
                }
            }
            return null;

        }
    }
}




