// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace EmployeeManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Truyền đường dẫn tuyệt đối
            EmployeeManager manager = new EmployeeManager(
                @"C:\Users\Hai\source\repos\PRN212\ConsoleApp1\EmployeeManager\Employees.txt"
            );
            manager.Load();

            while (true)
            {
                Console.WriteLine("\n==== Employee Management ====");
                Console.WriteLine("1. List Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Edit Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Search Employee");
                Console.WriteLine("6. Save & Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.ListEmployees();
                        break;
                    case "2":
                        manager.AddEmployee();
                        break;
                    case "3":
                        manager.EditEmployee();
                        break;
                    case "4":
                        manager.DeleteEmployee();
                        break;
                    case "5":
                        manager.SearchEmployee();
                        break;
                    case "6":
                        manager.Save();
                        Console.WriteLine("Saved. Bye!");
                        return;
                }
            }
        }
    }
}
