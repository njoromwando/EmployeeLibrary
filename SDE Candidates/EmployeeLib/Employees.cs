using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
namespace EmployeeLib
{
    public class Employees 
    {
        public string Id { get; set; }
        public string ManagerId { get; set; }
        public int Salary { get; set; }
        public List<Employees> GetEmployees { get; set; }

        //Constructor To pass Csv 
        private readonly string csvInput;
        public Employees(string csv)
        {

            csvInput = csv;
            SetEmployees();
            ValidateEmployee();
        }
        //Sets employees list
        private void SetEmployees()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
            MappingHelper csvMapper = new MappingHelper();
            CsvParser<Employees> csvParser = new CsvParser<Employees>(csvParserOptions, csvMapper);
            GetEmployees = csvParser
                .ReadFromString(csvReaderOptions, csvInput)
                .Select(x => x.Result).ToList();

        }
        // validations
        private void ValidateEmployee()
        {
            GetEmployees = GetEmployees.OrderBy(i => i.Id).Distinct(new CheckDublicate()).ToList();
            CircularReferenceValidate();
        }
        //Validates for circular reference and a manager who is not an employee
        private void CircularReferenceValidate()
        {
            for (int i = GetEmployees.Count - 1; i >= 0; i--)
            {
                var item = GetEmployees[i];
                GetEmployees.RemoveAll(m => m.ManagerId == item.Id && m.Id == item.ManagerId);
                if (!string.IsNullOrEmpty(item.ManagerId))
                {
                    ValidateManager(item.ManagerId);
                }
            }



        }
       //Validates Manager with No employee
      private void ValidateManager(string manager_Id)
        {
            if (GetEmployees.FirstOrDefault(i => i.Id.Trim() == manager_Id.Trim()) == null)
            {
                GetEmployees.RemoveAll(i => i.ManagerId.Trim() == manager_Id.Trim());
            }

        }

        //Returns salary budget for a specified manager
        //that is salary of all employee reporting directly and indirectly to the manager
        public long SalaryBudgetPerManager(string ManagerId)
        {
            long salary = 0;
            var emp = GetEmployees.FirstOrDefault(i => i.Id.Trim() == ManagerId.Trim());
            if (emp == null)
            {
                return 0;
            }
            salary += emp.Salary;

            foreach (var item in GetEmployees.Where(i => i.ManagerId == ManagerId))
            {
                salary += SalaryBudgetPerManager(item.Id);
            }
            return salary;

        }

        //CSV Maping
        public Employees()
        {

        }
        private class MappingHelper : CsvMapping<Employees>
        {
            public MappingHelper()
                : base()
            {
                MapProperty(0, x => x.Id);
                MapProperty(1, x => x.ManagerId);
                MapProperty(2, x => x.Salary);
            }

        }

   
    }
}
