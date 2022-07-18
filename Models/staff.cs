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
        internal static Staff FormatLine(string line)
        {
            var props = line.Split('\t');
            return new Staff(int.Parse(props[0]), props[1], props[2], props[3], (Gender)Enum.Parse(typeof(Gender),props[4]),DateTime.Parse(props[5]),
            props[6], props[7], props[8], props[9], (Role)Enum.Parse(typeof(Role),props[10]), decimal.Parse(props[11]));

        }
    }
}