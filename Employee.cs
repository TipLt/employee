// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace EmployeeManager
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Sex { get; set; }
        public DateTime Dob { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Position}, {Sex}, {Dob:yyyy-MM-dd}, {Salary:N0}";
        }
    }
    public static class EmployeeExtensions
    {
        // Extension method: Age
        public static int Age(this Employee emp)
        {
            var now = DateTime.Now;
            int age = now.Year - emp.Dob.Year;
            if (now < emp.Dob.AddYears(age)) age--;
            return age;
        }
    }
}
