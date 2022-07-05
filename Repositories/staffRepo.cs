using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class StaffRepo
    {
        private static int count = 1;
        public static int myIndex = 0;
        private static decimal wallet = 200m;
        public static Staff[] staffs = new Staff[50];
        public StaffRepo()
        {
            var staff = new Staff(1, "Popsicle", "Morningstar", "1234",
                       Gender.Female, DateTime.Parse("1960-07-20"), "1234", "08065374591", "Abk",
                       "Lucifer Morningstar", Role.Manager, 200);
            staffs[0] = staff;
            myIndex++;

        }
        public void AddNewStaff(string fName, string lName, string email, Gender gender, DateTime dateofBirth,
                               string password, string cellNo, string address, string nextofKin, Role role)
        {
            int id = ++count;
            Staff staff = new Staff(id, fName, lName, email, gender, dateofBirth,
                                password, cellNo, address, nextofKin, role, wallet);
            staffs[myIndex] = staff;
            myIndex++;

        }
        public Staff Login(string email, string passWord)
        {
            var staff = GetStaff(email);
            if (staff != null && staff.Password == passWord)
            {
                return staff;
            }
            return null;
        }
        public Staff GetManager(string email)
        {
            for (int i = 0; i < myIndex; i++)
            {
                if ((staffs[i] != null) && staffs[i].Role == Role.Manager)
                {
                    return staffs[i];
                }

            }
            return null;
        }


        public void ReturnStaff()
        {
            for (int i = 0; i < myIndex; i++)
            {
                Console.WriteLine($"{i + 1}. Staff Name: {staffs[i].FullName()} Staff E-mail: {staffs[i].Email} Staff CellNo: {staffs[i].PhoneNo} Staff Address: {staffs[i].Address} Staff Gender: {staffs[i].Gender}");
            }
        }
        public Staff GetStaff(string email)
        {
            for (int i = 0; i < myIndex; i++)
            {
                if ((staffs[i] != null) && staffs[i].Email == email)
                {
                    return staffs[i];
                }

            }
            return null;
        }
    }
}