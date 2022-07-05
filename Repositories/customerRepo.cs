using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class CustomerRepo
    {
        public static int myIndex = 0;
        private static int count = 1;
        public Customer[] customers = new Customer[50];
        public CustomerRepo()
        {
            var customer = new Customer(1, "Popsicle", "Morningstar", "1234",
                       Gender.Female, DateTime.Parse("1960-07-20"), "1234", "08065374591", "Abk",
                       "Lucifer Morningstar");
            customers[0] = customer;
            myIndex++;

        }
        public void Register(string firstName, string lastName, string eMail, Gender gender, DateTime dateOfBirth,
                       string passWord, string phoneNo, string address, string nextOfKin)
        {
            var customer = new Customer(count, firstName, lastName, eMail, gender, dateOfBirth,
                         passWord, phoneNo, address, nextOfKin);
            customers[myIndex] = customer;
            Console.WriteLine($"You have successfully created an account and your customer number is {customer.CustomerNo}");
            Console.WriteLine($"We have given you a bonus of {customer.Wallet}.");
            count++;
            myIndex++;
        }
        public Customer Login(string email, string passWord)
        {
            var customer = GetCustomer(email);
            if (customer != null && customer.Password == passWord)
            {
                return customer;
            }
            return null;
        }
        public Customer GetCustomer(string email)
        {
            for (int i = 0; i < (myIndex); i++)
            {
                if (customers[i] != null && customers[i].Email == email)
                {
                    return customers[i];
                }

            }
            return null;
        }

         public Customer GetCustomer(int id)
        {
            for (int i = 0; i < (myIndex); i++)
            {
                if ((customers[i] != null) && customers[i].Id == id)
                {
                    return customers[i];
                }

            }
            return null;
        }
        public void FundWallet(Customer customer)
        {
            Console.Write("Enter amount to credit your wallet: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            customer.Wallet += amount;
            Console.WriteLine($"Transaction successfull, your balance is {customer.Wallet}");

        }
        
    }
}