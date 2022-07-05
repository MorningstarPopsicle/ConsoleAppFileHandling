using System;
using ECommerceApp.Repositories;
using ECommerceApp.Models;
namespace ECommerceApp.Menu
{
    public class MainMenu
    {
        StaffRepo staffRepo;
        ProductRepo productRepo;
        CustomerRepo customerRepo;
        CartsRepo cartsRepo;
        OrderRepo orderRepo;
        StaffMenu staffMenu;
        CustomerMenu customerMenu;
        public MainMenu()
        {
            staffRepo = new StaffRepo();
            productRepo = new ProductRepo();
            customerRepo = new CustomerRepo();
            cartsRepo = new CartsRepo(productRepo, customerRepo);
            orderRepo = new OrderRepo(productRepo, customerRepo, cartsRepo, staffRepo);
            staffMenu = new StaffMenu(staffRepo, orderRepo, productRepo, cartsRepo);
            customerMenu = new CustomerMenu(customerRepo, orderRepo);
        }


        public  void Menu()
        {
            bool continueApp  = true;
            do{
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");
            Console.WriteLine("========WELCOME TO GetItAll MALL==========");
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Customer Dashboard");
            Console.WriteLine("2. Staff Dashboard");
            Console.WriteLine("0. Exit App");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input, enter another input: ");
                Menu();
            }
            switch (option)
            {
                case 1:
                    customerMenu.CustomerMainMenu();
                    break;
                case 2:
                   
                    staffMenu.StaffMainMenu();
                    break;
                case 0:
                    continueApp=false;
                    break;
                default:
                    Console.WriteLine("Invalid Input!");
                    break;
            }
            }while(continueApp);
        }

    }
}