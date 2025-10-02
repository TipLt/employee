// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Globalization;

namespace EmployeeManager
{
    // Delegate for search query
    public delegate IEnumerable<Employee> EmployeeQuery(List<Employee> employees);

    public class EmployeeManager
    {
        private string filePath;
        private List<Employee> employees = new List<Employee>();

        public EmployeeManager(string path)
        {
            filePath = path;
        }

        public void Load()
        {
            if (!File.Exists(filePath)) return;
            employees = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseEmployee)
                .Where(e => e != null)
                .ToList();
        }

        public void Save()
        {
            File.WriteAllLines(filePath, employees.Select(SerializeEmployee));
        }

        private Employee ParseEmployee(string line)
        {
            // id,name,position,sex,dob,salary
            var parts = line.Split(',');
            if (parts.Length != 6) return null;
            try
            {
                return new Employee
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Position = parts[2],
                    Sex = parts[3],
                    Dob = DateTime.ParseExact(parts[4], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Salary = double.Parse(parts[5])
                };
            }
            catch { return null; }
        }

        private string SerializeEmployee(Employee e)
        {
            // Save as: id,name,position,sex,dob,salary
            return $"{e.Id},{e.Name},{e.Position},{e.Sex},{e.Dob:yyyy-MM-dd},{e.Salary}";
        }

        public void ListEmployees()
        {
            Console.WriteLine("{0,-4} {1,-15} {2,-10} {3,-8} {4,-12} {5,-10} {6,-4}",
    "ID", "Name", "Position", "Sex", "DOB", "Salary", "Age");
            foreach (var e in employees)
            {
                Console.WriteLine("{0,-4} {1,-15} {2,-10} {3,-8} {4,-12} {5,-10} {6,-4}",
    e.Id, e.Name, e.Position, e.Sex, e.Dob.ToString("yyyy-MM-dd"), e.Salary.ToString("N0"), e.Age());
            }
        }

        public void AddEmployee()
        {
            Employee e = new Employee();
            Console.Write("Id: "); e.Id = int.Parse(Console.ReadLine());
            Console.Write("Name: "); e.Name = Console.ReadLine();
            Console.Write("Position: "); e.Position = Console.ReadLine();
            Console.Write("Sex: "); e.Sex = Console.ReadLine();
            Console.Write("DOB (yyyy-MM-dd): "); e.Dob = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.Write("Salary: "); e.Salary = double.Parse(Console.ReadLine());
            employees.Add(e);
            Console.WriteLine("Added.");
        }

        public void EditEmployee()
        {
            Console.Write("Enter ID to Edit: ");
            int id = int.Parse(Console.ReadLine());
            var emp = employees.FirstOrDefault(x => x.Id == id);
            if (emp == null) { Console.WriteLine("Not found!"); return; }
            Console.WriteLine("Leave blank to keep old value.");
            Console.Write($"Name ({emp.Name}): "); var val = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(val)) emp.Name = val;
            Console.Write($"Position ({emp.Position}): "); val = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(val)) emp.Position = val;
            Console.Write($"Sex ({emp.Sex}): "); val = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(val)) emp.Sex = val;
            Console.Write($"DOB ({emp.Dob:yyyy-MM-dd}): "); val = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(val)) emp.Dob = DateTime.ParseExact(val, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.Write($"Salary ({emp.Salary}): "); val = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(val)) emp.Salary = double.Parse(val);
            Console.WriteLine("Updated.");
        }

        public void DeleteEmployee()
        {
            Console.Write("Enter ID to Delete: ");
            int id = int.Parse(Console.ReadLine());
            var emp = employees.FirstOrDefault(x => x.Id == id);
            if (emp == null) { Console.WriteLine("Not found!"); return; }
            employees.Remove(emp);
            Console.WriteLine("Deleted.");
        }

        public void SearchEmployee()
        {
            Console.WriteLine("Search by: 1. ID  2. Name/Position/Sex  3. DOB  4. Salary");
            string opt = Console.ReadLine();
            EmployeeQuery query = null;
            switch (opt)
            {
                case "1":
                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());
                    query = emps => emps.Where(e => e.Id == id);
                    break;
                case "2":
                    Console.Write("Enter keyword: ");
                    string kw = Console.ReadLine().ToLower();
                    query = emps => emps.Where(e => e.Name.ToLower().Contains(kw) ||
                                                    e.Position.ToLower().Contains(kw) ||
                                                    e.Sex.ToLower().Contains(kw));
                    break;
                case "3":
                    Console.Write("Enter DOB (yyyy-MM-dd): ");
                    DateTime dob = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    query = emps => emps.Where(e => e.Dob == dob);
                    break;
                case "4":
                    Console.Write("Enter salary: ");
                    double salary = double.Parse(Console.ReadLine());
                    query = emps => emps.Where(e => e.Salary == salary);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }
            var result = query(employees);
            if (!result.Any())
            {
                Console.WriteLine("No results.");
                return;
            }
            foreach (var e in result)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
