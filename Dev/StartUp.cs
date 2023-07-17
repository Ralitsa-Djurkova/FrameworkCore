using System;
using System.Linq;
using System.Text;
using TestTwo.Data;
using TestTwo.Model;

namespace TestTwo
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();
            //string result = GetEployeesWithSalaryOver50000(db);
            string result = GetEmployeesFromResearchAndDevelopment(db);

            Console.WriteLine(result);
      

        }
        public static string GetEployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            //get all employees
            Employee[] employees = context//SoftUniContext
                .Employees//DbSet<Employee>
                .Where(e=>e.Salary > 50000)
                .OrderBy(e=>e.FirstName)
                .ToArray();//kraq na zaqwkata

            foreach(Employee employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employess = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .ToArray();

            foreach(var e in employess)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.Salary}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
