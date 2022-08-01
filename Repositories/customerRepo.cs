using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class CustomerRepo
    {
        private static int count = 1;
        public static string customerLogIn;
        public List <Customer> customers;
        public CustomerRepo()
        {
            customers = new List<Customer>();
            // string path = "Customer.txt";
            // if (File.Exists(path))
            // {
            //     var lines = File.ReadAllLines("Customer.txt");
            // }
            LoadFile();
            var customer = new Customer(1, "Popsicle", "Morningstar", "1234", "1234",
                       Gender.Female, DateTime.Parse("1960-07-20"),  "08065374591", "Abk",
                       "Lucifer Morningstar");
            customers.Add(customer);
            TextWriter textWriter = new StreamWriter("Customer.txt", true);
            textWriter.WriteLine(customer.ToString());
            textWriter.Close();
            customerLogIn = customer.Email;
            RefreshFile();
            count++;
            

        }
        public void Register(string firstName, string lastName, string eMail, string passWord, Gender gender, DateTime dateOfBirth,
                        string phoneNo, string address, string nextOfKin)
        {
             
            var customer = new Customer(count, firstName, lastName, eMail, passWord, gender, dateOfBirth,
                          phoneNo, address, nextOfKin);
            customers.Add(customer);
            TextWriter textWriter = new StreamWriter("Customer.txt", true);
            textWriter.WriteLine(customer.ToString());
            textWriter.Close();
            Console.WriteLine($"You have successfully created an account and your customer number is {customer.CustomerNo}");
            Console.WriteLine($"We have given you a bonus of {customer.Wallet}.");
            RefreshFile();
            count++;
            customerLogIn = customer.Email;
        }
        public Customer Login(string email, string passWord)
        {
            var customer = GetCustomer(email);
            if (customer != null && customer.Password == passWord)
            {
                customerLogIn = customer.Email;
                return customer;
            }
            return null;
        }
        public Customer GetCustomer(string email)
        {
            foreach(var item in customers)
            {
                if (item != null && item.Email == email)
                {
                    return item;
                }

            }
            return null;
        }

         public Customer GetCustomer(int id)
        {
            foreach(var item in customers)
            {
                if ((item != null) && item.Id == id)
                {
                    return item;
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
        private void RefreshFile()
        {
            TextWriter writer = new StreamWriter("Customer.txt");
            foreach (var item in customers)
            {
                
                writer.WriteLine(item);
               
            }
            writer.Flush();
            writer.Close();
        }

        private void LoadFile()
        {
            StreamReader reader = new StreamReader("Customer.txt");
            while(!reader.EndOfStream){
                customers.Add(Customer.FormatLine(reader.ReadLine()));
            }
            reader.Close();
        }
        
    }
}