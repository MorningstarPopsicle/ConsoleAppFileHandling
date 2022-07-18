using System;
using ECommerceApp.Enums;

namespace ECommerceApp.Models
{
    public class Customer : Person
    {
        public string CustomerNo { get; set;}
        public decimal Wallet { get; set; }


        public Customer(int id, string firstName, string lastName, string eMail, Gender gender,
        DateTime dateOfBirth, string passWord, string phoneNo, string address, string nextOfKin) :
        base(id, firstName, lastName, eMail, gender, dateOfBirth,
       passWord, phoneNo, address, nextOfKin)
        {
            CustomerNo = $"CU{Guid.NewGuid().ToString().Replace("-", " ").Substring(0, 5).ToUpper()}";
            Wallet = 1000.00m;
        }
        internal static Customer FormatLine(string line)
        {
            var props = line.Split('\t');
            return new Customer(int.Parse(props[0]), props[1], props[2], props[3], (Gender)Enum.Parse(typeof(Gender),props[4]),DateTime.Parse(props[5]),
            props[6], props[7], props[8], props[9]);

        }

    }
}