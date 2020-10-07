using CompanyData.Data;
using CompanyData.Models;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using Moq;

namespace CompanyData.Tests
{
    [TestFixture]
    public class CompaniesFiltrsTests
    {
        private List<Company> companies;
        private Company company1, company2, company3, company4;

        [SetUp]
        public void Setup()
        {
            companies = new List<Company>();

            company1 = new Company
            {
                ID = 1,
                Name = "Ccc",
                EstablishmentYear = 1999,
                Employees = new HashSet<Employee>
                {
                    new Employee
                    {
                        ID = 1,
                        FirstName = "Adam",
                        LastName = "Malysz",
                        DateOfBirth = new DateTime(1970, 12, 3),
                        JobTitle = JobTitle.Manager

                    },
                    new Employee
                    {
                        ID = 2,
                        FirstName = "Kamil",
                        LastName = "Stock",
                        DateOfBirth = new DateTime(1987, 05, 25),
                        JobTitle = JobTitle.Developer

                    }
                }
            };
            companies.Add(company1);

            company2 = new Company
            {
                ID = 2,
                Name = "ABB",
                EstablishmentYear = 1988,
                Employees = new HashSet<Employee>
                {
                    new Employee
                    {
                        ID = 3,
                        FirstName = "Robert",
                        LastName = "Lewandowski",
                        DateOfBirth = new DateTime(1988, 8, 21),
                        JobTitle = JobTitle.Architect
                    }
                }
            };
            companies.Add(company2);

            company3 = new Company
            {
                ID = 3,
                Name = "PrawaLewa",
                EstablishmentYear = 1988,
                Employees = new HashSet<Employee>
                {
                    new Employee
                    {
                        ID = 4,
                        FirstName = "Marcin",
                        LastName = "Daniec",
                        DateOfBirth = new DateTime(1957, 9, 1),
                        JobTitle = JobTitle.Administrator
                    }
                }
            };
            companies.Add(company3);

            company4 = new Company
            {
                ID = 4,
                Name = "WWE",
                EstablishmentYear = 1993,
                Employees = new HashSet<Employee>()

            };
            companies.Add(company4);
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_KaywordCheck_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "Lewa",
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company2);
            expectedFiltredCompanies.Add(company3);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_SpecificDataTimeCheck_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {  
                EmployeeDateOfBirthFrom = new DateTime(1969, 05, 25),
                EmployeeDateOfBirthTo = new DateTime(1971, 05, 25),
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_CheckTitles_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                EmployeeJobTitles = new List<JobTitle>{JobTitle.Manager, JobTitle.Architect}
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);
            expectedFiltredCompanies.Add(company2);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_AllParametersCheck_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "Adam",
                EmployeeDateOfBirthFrom = new DateTime(1988, 01, 1),
                EmployeeDateOfBirthTo = new DateTime(1988, 12, 31),
                EmployeeJobTitles = new List<JobTitle> { JobTitle.Administrator }
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);
            expectedFiltredCompanies.Add(company2);
            expectedFiltredCompanies.Add(company3);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_EmptyEmployeesListCheck_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "Malysz",
                EmployeeJobTitles = new List<JobTitle>()
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_EmployeeOnlyFromDOB_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                EmployeeDateOfBirthFrom = new DateTime(1980, 01, 1),
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);
            expectedFiltredCompanies.Add(company2);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_EmployeeOnlyToDOB_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {     
                EmployeeDateOfBirthTo = new DateTime(1980, 12, 31),
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company1);
            expectedFiltredCompanies.Add(company3);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_CompanyWithoutEmployee_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "WWE",
            };

            CompaniesFiltrs companiesFiltr = new CompaniesFiltrs(companies, companySearchParameters);
            List<Company> filtredCompanies = companiesFiltr.GetFiltredCompaniesByParameters();

            List<Company> expectedFiltredCompanies = new List<Company>();
            expectedFiltredCompanies.Add(company4);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
    }
}
