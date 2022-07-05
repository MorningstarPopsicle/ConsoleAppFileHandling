using System;
using ECommerceApp.Enums;
namespace ECommerceApp.Models
{
    public class Staff: Person
    {
        public string StaffNo {get; set;}
        public Role Role {get; set;}
        public decimal Wallet {get; set;}
        public Staff(int id, string firstName, string lastName, string eMail, Gender gender, 
                        DateTime dateOfBirth, string passWord,string phoneNo, string address,string nextOfKin, Role role, decimal wallet): 
                        base(id,  firstName,  lastName,  eMail,  gender, dateOfBirth,
                        passWord,  phoneNo,  address, nextOfKin)
        {
            StaffNo = $"ST{Guid.NewGuid().ToString().Replace("-"," ").Substring(0,5).ToUpper()}";
            Role = role;
            Wallet = 200m;
        }
    }
}