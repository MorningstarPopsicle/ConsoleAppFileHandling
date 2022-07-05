using System;
using ECommerceApp.Enums;
namespace ECommerceApp.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string NextOfKin { get; set; }
        public Person(int id, string firstName, string lastName, string eMail, Gender gender, DateTime dateOfBirth,
                        string passWord, string phoneNo, string address, string nextOfKin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = eMail;
            Gender = gender;
            Password = passWord;
            PhoneNo = phoneNo;
            Address = address;
            NextOfKin = nextOfKin;
            DateOfBirth = dateOfBirth;
        }
        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

    }
}