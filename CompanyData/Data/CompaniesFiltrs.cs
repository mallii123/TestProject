using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CompanyData.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace CompanyData.Data
{
    public class CompaniesFiltrs
    {
        private List<Company> companies;
        private CompanySearchParameters companySearchParameters;

        public CompaniesFiltrs(List<Company> companies, CompanySearchParameters companySearchParameters)
        {
            this.companies = companies;
            this.companySearchParameters = companySearchParameters;
        }
        public List<Company> GetFiltredCompaniesByParameters()
        {
            List<Company> filteredCompanies = new List<Company>();

            if(!String.IsNullOrEmpty(companySearchParameters.Keyword))
                filteredCompanies.AddRange(GetFilteredCompaniesWithKeyword());

            if (companySearchParameters.EmployeeDateOfBirthFrom.HasValue || companySearchParameters.EmployeeDateOfBirthTo.HasValue)
                filteredCompanies.AddRange(GetFilteredCompaniesWithDate());

            if (companySearchParameters.EmployeeJobTitles != null && companySearchParameters.EmployeeJobTitles.Count > 0)
                filteredCompanies.AddRange(GetFilteredCompaniesWithTitle());

            return filteredCompanies.Distinct().ToList();
        }
        public List<Company> GetFilteredCompaniesWithKeyword()
        {
            var filteredCompaniesWithKeyword =
                    from company in companies
                    where company.Name.Contains(companySearchParameters.Keyword)
                    || (
                            from employee in company.Employees
                            where employee.FirstName.Contains(companySearchParameters.Keyword)
                            || employee.LastName.Contains(companySearchParameters.Keyword)
                            select employee
                            ).Count() > 0
                    select company;
            return filteredCompaniesWithKeyword.ToList();
        }
        public List<Company> GetFilteredCompaniesWithDate()
        {
            var filteredCompaniesWithDate =
                from company in companies
                where (
                        from employee in company.Employees
                        where employee.DateOfBirth.Value.CompareTo(companySearchParameters.EmployeeDateOfBirthFrom ==null
                                                                ? DateTime.MinValue : companySearchParameters.EmployeeDateOfBirthFrom) >= 0
                        && employee.DateOfBirth.Value.CompareTo(companySearchParameters.EmployeeDateOfBirthTo == null 
                                                            ? DateTime.MaxValue : companySearchParameters.EmployeeDateOfBirthTo) <= 0 
                        select employee
                        ).Count() > 0
                select company;
            return filteredCompaniesWithDate.ToList();
        }
        public List<Company> GetFilteredCompaniesWithTitle()
        {
            var filteredCompaniesWithTitle =
             from company in companies
             where (
                     from employee in company.Employees
                     where companySearchParameters.EmployeeJobTitles.Contains(employee.JobTitle.Value)
                     select company
                     ).Count() > 0
             select company;
            return filteredCompaniesWithTitle.ToList();
        }
    }
}
