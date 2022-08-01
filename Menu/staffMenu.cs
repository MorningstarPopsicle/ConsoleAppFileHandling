using System;
using ECommerceApp.Repositories;
using ECommerceApp.Models;
using ECommerceApp.Enums;

namespace ECommerceApp.Menu
{
    public class StaffMenu
    {

        StaffRepo staffRepo;
        ProductRepo productRepo;
        // CustomerRepo customerRepo;
        CartsRepo cartsRepo;
        OrderRepo orderRepo;



        public StaffMenu(StaffRepo _staffRepo, OrderRepo _orderRepo, ProductRepo _productrepo, CartsRepo _cartsRepo)
        {
            staffRepo = _staffRepo;
            orderRepo = _orderRepo;
            productRepo = _productrepo;
            cartsRepo = _cartsRepo;
        }


        public void StaffMainMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("0. Back to MainMenu");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input");
                StaffMainMenu();
            }
            switch (option)
            {
                case 1:
                    StaffLogin();
                    break;
                case 0:

                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    StaffMainMenu();
                    break;
            }
        }
        public void StaffLogin()
        {
            Console.Write("Enter E-mail here: ");
            string email = Console.ReadLine();
            Console.Write("Enter password here: ");
            string password = Console.ReadLine();
            var staff = staffRepo.Login(email, password);
            if (staff == null)
            {
                Console.WriteLine("Invalid Username or Password");
                StaffLogin();
                return;
            }

            if (staff.Role == Role.Manager)
            {
                ManagerMenu();
            }
            else
            {
                OtherStaffMenu();
            }
        }
        public void ManagerMenu()
        {
            Console.WriteLine("1. To add staff");
            Console.WriteLine("2. To edit staff");
            Console.WriteLine("3. To view staff");
            Console.WriteLine("4. To add product");
            Console.WriteLine("5. To edit product");
            Console.WriteLine("6. To view product");
            Console.WriteLine("7. To check customer's order");
            Console.WriteLine("8. To check firm's order");
            Console.WriteLine("0. To return to staff mainmenu");

            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input");
                ManagerMenu();
            }
            switch (option)
            {
                case 1:
                    RegisterStaff();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                // case 2:
                //     productRepo.ReturnProducts();
                //     AddAProduct();
                //     Console.WriteLine("Press any key to continue");
                //     Console.ReadKey();
                //     Console.WriteLine();
                //     ManagerMenu();
                //     break;
                case 3:
                    ViewStaff();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 4:
                    AddAProduct();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 5:
                    productRepo.EditProduct();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 6:
                    ViewProduct();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 7:
                    CheckCustomerOrder();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 8:
                    orderRepo.PrintAllOrder();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 0:
                    StaffMainMenu();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Invalid input, try again");
                    ManagerMenu();
                    break;
            }
        }
        public void OtherStaffMenu()
        {
            Console.WriteLine("1. To check Customer's order");
            Console.WriteLine("2. To check firm's order");
            Console.WriteLine("0. To return to  staff mainmenu");
            int option;
            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input");
                OtherStaffMenu();
            }
            switch (option)
            {
                case 1:
                    CheckCustomerOrder();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    OtherStaffMenu();
                    break;
                case 2:
                    orderRepo.PrintAllOrder();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine();
                    ManagerMenu();
                    break;
                case 0:
                    StaffMainMenu();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid input, try again");
                    OtherStaffMenu();
                    break;
            }
        }

        public void RegisterStaff()
        {
            Console.WriteLine("Enter first name here: ");
            string fName = Console.ReadLine();
            Console.WriteLine("Enter last name here: ");
            string lName = Console.ReadLine();
            Console.WriteLine("Enter email here: ");
            string email = Console.ReadLine();
            string check = "@";
            bool isIn = email.Contains(check);
            while (!(isIn))
            {
                Console.WriteLine("Invalid Input, enter email again");
                email = Console.ReadLine();
                isIn = email.Contains(check);

            }
            foreach (var item in StaffRepo.staffs)
            {
                if (item.Email == email)
                {
                    Console.WriteLine("This Email is not available, enter another username");
                    email = Console.ReadLine();
                }
            }
            Console.WriteLine("Enter password ");
            string password = Console.ReadLine();
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
            Console.WriteLine("Enter Phone number: ");
            string cellNo = Console.ReadLine();
            Console.WriteLine("Enter address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Next of Kin");
            string nextofKin = Console.ReadLine();
            Role role = Role.Staff;

            staffRepo.AddNewStaff(fName, lName, email, password, gender, dateOfBirth, cellNo,
                                address, nextofKin, role);
            Console.WriteLine($"Staff {lName} {fName} has been successfully added");
        }

        public void AddAProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter price here: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter quantity here: ");
            int quantity = int.Parse(Console.ReadLine());
            productRepo.AddProduct(name, price, quantity);

        }
        public void CheckCustomerOrder()
        {
            // Console.Write("Enter Customer's ID");
            // var customer = int.Parse(Console.ReadLine());
            orderRepo.PrintCustomerOrder();
        }
        public void ViewStaff()
        {
            staffRepo.ReturnStaff();
        }
        public void ViewProduct()
        {
            productRepo.ReturnProducts();
        }

    }


}

