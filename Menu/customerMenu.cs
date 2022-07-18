using System;
using ECommerceApp.Models;
using ECommerceApp.Repositories;
using ECommerceApp.Enums;
namespace ECommerceApp.Menu
{
    public class CustomerMenu
    {
        
        public Customer customer { get; set; }
        CustomerRepo customerRepo;
        OrderRepo orderRepo;
        // ProductRepo productRepo;
        // CartsRepo cartRepo;
        public CustomerMenu(CustomerRepo _customerRepo, OrderRepo _orderRepo)
        {
            customerRepo = _customerRepo;
            orderRepo = _orderRepo;
        }
        public void CustomerMainMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Back to MainMenu");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input");
                CustomerMainMenu();
            }
            switch (option)
            {
                case 1:
                    CustomerLogin();
                    break;
                case 2:
                    RegisterCustomer();
                    CustomerMainMenu();
                    break;
                case 0:
                   
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    CustomerMainMenu();
                    break;
            }
        }
        public void CustomerLogin()
        {
            Console.Write("Enter E-mail here: ");
            string email = Console.ReadLine();
            Console.Write("Enter password here: ");
            string password = Console.ReadLine();
            customer = customerRepo.Login(email, password);
            if (customer == null)
            {
                Console.WriteLine("Invalid Username or Password");
                CustomerMainMenu();
                return;
            }

            else
            {
                CustomerSubMenu();
            }
        }
        public void CustomerSubMenu()
        {
            Console.WriteLine("1. Buy a product");
            Console.WriteLine("2. View cart");
             Console.WriteLine("3. Pay");
            Console.WriteLine("0. Log out");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Try again.");
                CustomerSubMenu();
            }
            switch (option)
            {
                case 1:

                    BuyGoods();
                    CustomerSubMenu();
                    break;
                case 2:

                    orderRepo.ViewCart();
                    CustomerSubMenu();
                    break;
                case 3:

                    orderRepo.Pay();
                    CustomerSubMenu();
                    break;

                case 0:
                    orderRepo.LogOut();
                    CustomerMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    CustomerSubMenu();
                    break;

            }
        }
        private void BuyGoods()
        {
            var customer = customerRepo.GetCustomer(CustomerRepo.customerLogIn);
            orderRepo.BuyProduct(customer);
        }    
        public void RegisterCustomer()
        {
            Console.WriteLine("Enter first name here: ");
            string fName = Console.ReadLine();
            Console.WriteLine("Enter last name here: ");
            string lName = Console.ReadLine();
            Console.WriteLine("Enter email here: ");
            string email = Console.ReadLine();
            Console.WriteLine("Choose Gender(1,2,3): ");
            int i = 1;
            foreach (Gender genderValue in Enum.GetValues(typeof(Gender)))
            {
                Console.WriteLine($"{i}.    {genderValue}");
                i++;
            }
            Gender gender = (Gender)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Date of Birth(yyyy-mm-dd): ");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.WriteLine("Invalid format");
                Console.WriteLine("Enter date of birth again (yyyy-mm-dd):");
            }
            Console.WriteLine("Enter password ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter Phone number: ");
            string cellNo = Console.ReadLine();
            Console.WriteLine("Enter address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Next of Kin");
            string nextofKin = Console.ReadLine();

            customerRepo.Register(fName, lName, email, gender, dateOfBirth, password, cellNo,
                                address, nextofKin);
            Console.WriteLine($"You have been successfully registered");
        }
    }
}