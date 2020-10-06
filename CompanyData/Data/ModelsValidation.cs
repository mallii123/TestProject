using System;
using CompanyData.Models;

// Repleced by System.ComponentModel.DataAnnotations in Models;

namespace CompanyData.Data
{
    [Obsolete("Old validation implementation")]
    static public class ModelsValidation
    {
        public static string GetValidateCompanyInfo(Company company)
        {
            string errorsString = string.Empty;
            if (String.IsNullOrEmpty(company.Name))
            {
                errorsString += "-Name cannot be empty\n";
            }
            if (company.EstablishmentYear <= 0)
            {
                errorsString += "-EstablishmentYear cannot be empty, zero or less\n";
            }
            if (company.EstablishmentYear.CompareTo(DateTime.Now.Year) > 0)
            {
                errorsString += "-EstablishmentYear cannot be bigger than current year\n";
            }
            if (company.Employees != null)
            {
                int employeeCounter = 0;

                foreach (Employee employee in company.Employees)
                {
                    employeeCounter++;
                    string employeeErrorString = GetValidateEmployeeInfo(employee);
                    if (employeeErrorString != String.Empty)
                    {
                        errorsString += " Employee " + employeeCounter + " errors:\n" + employeeErrorString;
                    }
                }
            }
            if (errorsString != String.Empty)
            {
                errorsString = "Validation errors:\n" + errorsString;
            }

            return errorsString;
        }
        public static string GetValidateEmployeeInfo(Employee employee)
        {
            string employeeErrorString = string.Empty;
            if (String.IsNullOrEmpty(employee.FirstName))
            {
                employeeErrorString += " -FirstName cannot be empty\n";
            }
            if (String.IsNullOrEmpty(employee.LastName))
            {
                employeeErrorString += " -LastName cannot be empty\n";
            }
            if (employee.DateOfBirth == null || employee.DateOfBirth == DateTime.MinValue)
            {
                employeeErrorString += " -DateOfBirth cannot be empty\n";
            }
            else if (employee.DateOfBirth != null && DateTime.Now.CompareTo(employee.DateOfBirth) < 0)
            {
                employeeErrorString += " -DateOfBirth cannot be bigger than current year\n";
            }

            if (employee.JobTitle == 0)
            {
                employeeErrorString += " -JobTitle cannot be empty\n";
            }
            return employeeErrorString;
        }
    }
}
