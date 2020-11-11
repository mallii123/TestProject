using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CompanyData.Models;
using NHibernate;
using Microsoft.EntityFrameworkCore.Internal;

namespace CompanyData.Services
{
    public static class CompaniesFiltrs
    {
        public static List<Company> GetCompaniesByAllParameters(ISession session, CompanySearchParameters companySearchParameters)
        {
            List<Company> companiesList = new List<Company>();

            if(!String.IsNullOrEmpty(companySearchParameters.Keyword))
                companiesList.AddRange(GetCompaniesWithKeyword(session, companySearchParameters.Keyword));

            if (companySearchParameters.EmployeeDateOfBirthFrom.HasValue || companySearchParameters.EmployeeDateOfBirthTo.HasValue)
                companiesList.AddRange(GetCompaniesWithDate(session, companySearchParameters.EmployeeDateOfBirthFrom, companySearchParameters.EmployeeDateOfBirthTo));

            if (companySearchParameters.EmployeeJobTitles != null && companySearchParameters.EmployeeJobTitles.Count > 0)
                companiesList.AddRange(GetCompaniesWithTitle(session, companySearchParameters.EmployeeJobTitles));

            return companiesList.Distinct().ToList();
        }
        public static List<Company> GetCompaniesWithKeyword(ISession session, string keyword)
        {
            var filteredCompaniesWithKeyword =
                    from company in session.Query<Company>()
                    where company.Name.Contains(keyword)
                    || (
                            from employee in company.Employees
                            where employee.FirstName.Contains(keyword)
                            || employee.LastName.Contains(keyword)
                            select employee
                            ).Count() > 0
                    select company;


            filteredCompaniesWithKeyword = session.Query<Company>().Where(b => b.Name == keyword);

            return filteredCompaniesWithKeyword.ToList();
        }
        public static List<Company> GetCompaniesWithDate(ISession session, DateTime? employeeDateOfBirthFrom, DateTime? employeeDateOfBirthTo)
        {


            var filteredCompaniesWithDate =
                from company in session.Query<Company>()
                where (
                        from employee in company.Employees
                        where employee.DateOfBirth.Value.CompareTo(employeeDateOfBirthFrom == null
                                                                ? DateTime.MinValue : employeeDateOfBirthFrom.Value) >= 0
                        && employee.DateOfBirth.Value.CompareTo(employeeDateOfBirthTo == null 
                                                            ? DateTime.MaxValue : employeeDateOfBirthTo.Value) <= 0 
                        select employee
                        ).Count() > 0
                select company;
            return filteredCompaniesWithDate.ToList();
        }
        public static List<Company> GetCompaniesWithTitle(ISession session, List<JobTitle> employeeJobTitles)
        {
            var filteredCompaniesWithTitle =
             from company in session.Query<Company>()
             where (
                     from employee in company.Employees
                     where employeeJobTitles.Contains(employee.JobTitle.Value)
                     select company
                     ).Count() > 0
             select company;
            return filteredCompaniesWithTitle.ToList();
        }
    }
}
