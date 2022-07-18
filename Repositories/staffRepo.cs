using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class StaffRepo
    {
        private static int count = 1;
        private static decimal wallet = 200m;
        public static List<Staff> staffs;
        public StaffRepo()
        {
            staffs = new List<Staff>();
            string path = "Staff.txt";
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path: "Staff.txt");
                foreach (var item in lines)
                {
                    var staffNew = Staff.FormatLine(item);
                    staffs.Add(staffNew);
                }
            }
            var staff = new Staff(1, "Popsicle", "Morningstar", "1234",
                       Gender.Female, DateTime.Parse("1960-07-20"), "1234", "08065374591", "Abk",
                       "Lucifer Morningstar", Role.Manager, 200);
            staffs.Add(staff);


        }
        public void AddNewStaff(string fName, string lName, string email, Gender gender, DateTime dateofBirth,
                               string password, string cellNo, string address, string nextofKin, Role role)
        {
            Staff staff = new Staff(count, fName, lName, email, gender, dateofBirth,
                                password, cellNo, address, nextofKin, role, wallet);
            staffs.Add(staff);
            count++;

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
            foreach (var item in staffs)
            {
                if ((item != null) && item.Role == Role.Manager)
                {
                    return item;
                }

            }
            return null;
        }


        public void ReturnStaff()
        {
            int i = 1;
            foreach (var item in staffs)
            {
                
                Console.WriteLine($"Current staffs are: \n{i}. Staff Name: {item.FullName()}\t Staff E-mail: {item.Email} \tStaff CellNo: {item.PhoneNo} \tStaff Address: {item.Address} \tStaff Gender: {item.Gender}");
                i++;
            }
        }
        public Staff GetStaff(string email)
        {
            foreach (var item in staffs)
            {
                if ((item != null) && item.Email == email)
                {
                    return item;
                }

            }
            return null;
        }
    }
}