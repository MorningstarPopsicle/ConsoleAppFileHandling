using System;
using ECommerceApp.Enums;

namespace ECommerceApp.Models
{
    public class Customer : Person
    {
        public string CustomerNo { get; set;}
        public decimal Wallet { get; set; }


        public Customer(int id, string firstName, string lastName, string eMail, string passWord, Gender gender,
        DateTime dateOfBirth, string phoneNo, string address, string nextOfKin) :
        base(id, firstName, lastName, eMail, passWord, gender, dateOfBirth,
        phoneNo, address, nextOfKin)
        {
            CustomerNo = $"CU{Guid.NewGuid().ToString().Replace("-", " ").Substring(0, 5).ToUpper()}";
            Wallet = 1000.00m;
        }
        internal static Customer FormatLine(string line)
        {
            var props = line.Split('\t');
            var id = int.Parse(props[0]);
            var firstName = props[1];
            var lastName = props[2];
            var email = props[3];
            var passWord = props[4];
            var gender = (Gender)Enum.Parse(typeof(Gender),props[5]);
            var dob = DateTime.Parse(props[6]);
            var no = props[7];
            var address = props[8];
            var nok = props[9];

            return new Customer(int.Parse(props[0]), props[1], props[2], props[3], props[4], (Gender)Enum.Parse(typeof(Gender),props[5]),DateTime.Parse(props[6]),
             props[7], props[8], props[9]);

        }
        public override string ToString()
        {
            return $"{Id}\t{FirstName}\t{LastName}\t{Email}\t{Password}\t{Gender}\t{DateOfBirth}\t{PhoneNo}\t{Address}\t{NextOfKin}\t{CustomerNo}\t{Wallet}";
        }

    }
}