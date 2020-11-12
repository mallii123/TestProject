using CompanyData.Services;
using CompanyData.Models;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using NHibernate;
using Moq;
using System.Linq;

namespace CompanyData.Tests.UnitTests
{
    [TestFixture]
    public class CompaniesFiltrsTests
    {
        private List<Company> companies;
        private Company company1, company2, company3, company4;
        ISession session;

        [SetUp]
        public void Setup()
        {
            #region InitializationDumpCompanies
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
            #endregion

            var sessionMock = new Mock<ISession>();
            session = sessionMock.Object;

            sessionMock.Setup(x => x.Query<Company>()).Returns(companies.AsQueryable());
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_KaywordCheck_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "Lewa",
            };
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company2, company3
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

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
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_CheckTitles_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                EmployeeJobTitles = new List<JobTitle>{JobTitle.Manager, JobTitle.Architect}
            };
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1, company2
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

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
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1, company2, company3
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

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
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_EmployeeOnlyFromDOB_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                EmployeeDateOfBirthFrom = new DateTime(1980, 01, 1),
            };
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1, company2
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_EmployeeOnlyToDOB_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {     
                EmployeeDateOfBirthTo = new DateTime(1980, 12, 31),
            };
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company1, company3
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
        [Test]
        public void GetFiltredCompaniesByParametersTest_CompanyWithoutEmployee_Pass()
        {
            var companySearchParameters = new CompanySearchParameters
            {
                Keyword = "WWE",
            };
            List<Company> expectedFiltredCompanies = new List<Company>()
            {
                company4
            };

            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }

        [Test]
        public void GetFiltredCompaniesByParametersTest_EmptyParameters_Pass()
        {
            var companySearchParameters = new CompanySearchParameters();
            List<Company> expectedFiltredCompanies = new List<Company>();


            List<Company> filtredCompanies = CompaniesFiltrs.GetCompaniesByAllParameters(session, companySearchParameters);

            Assert.AreEqual(filtredCompanies, expectedFiltredCompanies);
        }
    }
}
