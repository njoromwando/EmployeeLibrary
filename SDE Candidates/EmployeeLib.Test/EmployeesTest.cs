using EmployeeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace Employee.UnitTests
{
    [TestClass]
    public class EmployeesTest
    {

        [TestMethod]
        public void TestForSalaryBudgetForaManager()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(1800, employee.SalaryBudgetPerManager("Employee2"));
            Assert.AreEqual(500, employee.SalaryBudgetPerManager("Employee3"));
            Assert.AreEqual(3800, employee.SalaryBudgetPerManager("Employee1"));
        }
        [TestMethod]
        //TestForValidSalary
        public void TestForValidSalary()
        {
            var stringBuilder = new StringBuilder()
               .AppendLine("Employee4,Employee2,1000");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employee.GetEmployees.Count() < 1, "Employee Salary  a valid interger");
        }
        [TestMethod]
        //Tests dublicate employees
        public void TestForDuplicatesAndInEmployee()
        {
            var stringBuilder = new StringBuilder()
               .AppendLine("Employee4,Employee2,500")
               .AppendLine("Employee3,Employee1,800")
               .AppendLine("Employee1,,1000")
                .AppendLine("Employee5,Employee1,500")
               .AppendLine("Employee2,Employee1,500");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(5, employee.GetEmployees.Count);
        }
        [TestMethod]
        //Tests to ascertain that an employee is not reporting to more than one manager
        public void TestThatEmployeesReportsonlyToOnlyOneManager()
        {
            var stringBuilder = new StringBuilder()
                          .AppendLine("Employee4,Employee2,500")
                          .AppendLine("Employee4,Employee1,1200")
                          .AppendLine("Employee2,Employee1,1200")
                          .AppendLine("Employee3,Employee1,1200")
                          .AppendLine("Employee1,Employee4,1200");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(4, employee.GetEmployees.Count);
        }
        [TestMethod]
        public void TestForOnlyOneCeoPresent()
        {
            var stringBuilder = new StringBuilder()
                 .AppendLine("Employee4,Employee2,500")
                 .AppendLine("Employee3,Employee1,800")
                 .AppendLine("Employee1,,1000")
                  .AppendLine("Employee5,Employee1,500")
                 .AppendLine("Employee2,Employee1,500")
                  .AppendLine("Employee6,,1000");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(2, employee.GetEmployees.Where(i => i.ManagerId == "" || i.ManagerId == null).Count());

        }
        [TestMethod]

        public void TestForNoCircularReference()
        {
            var stringBuilder = new StringBuilder()
               .AppendLine("Employee1,Employee2,500")
               .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee1,,1000")
                .AppendLine("Employee5,Employee1,500")
               .AppendLine("Employee2,Employee1,500")
                .AppendLine("Employee6,,1000");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.AreEqual(1, employee.GetEmployees.Count());
        }
        [TestMethod]
        //Tests for a manager who is not an employee
        public void TestForManagersnotEmployee()
        {
            var stringBuilder = new StringBuilder()
                          .AppendLine("Employee4,,1200")
                          .AppendLine("Employee6,Employee4,1200");
            Employees employee = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employee.GetEmployees.Count() > 1);
        }

        //Test for an employee manager with all the child salaries


    }  
    
}
