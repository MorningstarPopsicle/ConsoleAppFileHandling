using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class StaffRepo
    {
        private static int count = 1;
        private static decimal wallet = 200m;
        // private TextWriter textWriter;
        public static List<Staff> staffs;
        public StaffRepo()
        {
            staffs = new List<Staff>();
            LoadFile();
            // string path = "Staff.txt";
            // if (File.Exists(path))
            // {
            //     var lines = File.ReadAllLines(path: "Staff.txt");
            //     foreach (var item in lines)
            //     {
            //         var staffNew = Staff.FormatLine(item);
            //         staffs.Add(staffNew);
            //     }
            // }
            var staff = new Staff(1, "Popsicle", "Morningstar", "1234", "1234",
                       Gender.Female, DateTime.Parse("1960-07-20"),  "08065374591", "Abk",
                       "Lucifer Morningstar", Role.Manager, 200);
            staffs.Add(staff);
            // RefreshFile();
            // TextWriter textWriter = new StreamWriter("Staff.txt", true);
            // textWriter.WriteLine(staff.ToString());
            // // textWriter.Flush();
            // textWriter.Close();
            
            count++;
            

        }
        public void AddNewStaff(string fName, string lName, string email, string password,Gender gender, DateTime dateofBirth,
                                string cellNo, string address, string nextofKin, Role role)
        {
            Staff staff = new Staff(count, fName, lName, email, password, gender, dateofBirth,
                                 cellNo, address, nextofKin, role, wallet);
            staffs.Add(staff);
            RefreshFile();
            // TextWriter textWriter = new StreamWriter("Staff.txt", true);
            // textWriter.WriteLine(staff.ToString());
            // // textWriter.Flush();
            // textWriter.Close();
            count++;
            // RefreshFile();

        }
        //  public void EditStaff()
        // {
        //     Console.WriteLine("Enter first name here: ");
        //     string fName = Console.ReadLine();
        //     Console.WriteLine("Enter last name here: ");
        //     string lName = Console.ReadLine();
        //     Console.WriteLine("Enter emial here: ");
        //     string email = Console.ReadLine();
        //     Console.WriteLine("Choose Gender(1,2,3): ");
        //     int i = 1;
        //     foreach (Gender genderValue in Enum.GetValues(typeof(Gender)))
        //     {
        //         Console.WriteLine($"{i}.    {genderValue}");
        //         i++;
        //     }
        //     Gender gender = (Gender)int.Parse(Console.ReadLine());
        //     Console.WriteLine("Enter Date of Birth(yyyy-mm-dd): ");
        //     DateTime dateOfBirth;
        //     while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
        //     {
        //         Console.WriteLine("Invalid format");
        //         Console.WriteLine("Enter date of birth again (yyyy-mm-dd):");
        //     }
        //     Console.WriteLine("Enter password ");
        //     string password = Console.ReadLine();
        //     Console.WriteLine("Enter Phone number: ");
        //     string cellNo = Console.ReadLine();
        //     Console.WriteLine("Enter address: ");
        //     string address = Console.ReadLine();
        //     Console.WriteLine("Enter Next of Kin");
        //     string nextofKin = Console.ReadLine();
        //     Role role = Role.Staff;

        //     var staff = GetStaff(staffNo);
        //     product.Price = price;
        //     product.ProductName = productName;
        //     product.Quantity = quantity;
        //     Console.WriteLine($"The product has been edited. It is now {product.ProductName} {product.Price} {product.Quantity}");
        // }
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
        private void RefreshFile()
        {
            TextWriter writer = new StreamWriter("Staff.txt");
            foreach (var item in staffs)
            {
                writer.WriteLine(item);
            }
            writer.Flush();
            writer.Close();
        }
        private void LoadFile()
        {
            StreamReader reader = new StreamReader("Staff.txt");
            while(!reader.EndOfStream){
                staffs.Add(Staff.FormatLine(reader.ReadLine()));
            }
            reader.Close();
        }
    }
}