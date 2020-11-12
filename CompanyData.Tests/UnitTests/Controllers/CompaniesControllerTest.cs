using CompanyData.Services;
using CompanyData.Models;
using CompanyData.Controllers;
using CompanyData.Designs;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using NHibernate;
using Moq;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CompanyData.Tests.UnitTests
{
    [TestFixture]
    public class CompaniesControllerTest
    {
        CompaniesController companiesController;
        Company company1, company2;

        [SetUp]
        public void Setup()
        {
            companiesController = new CompaniesController();
            company1 = new Company();
            company2 = new Company();

            var companyActionsMock = new Mock<CompanyActions>();
            companiesController._companyActions = companyActionsMock.Object;
            companyActionsMock.Setup(_companyActions => _companyActions.Create(company1)).Returns(16L);
        }

        [Test]
        public void Create_CompanyCreated_Pass()
        {

            var okStatus = companiesController.Create(company1);
            var dumpOkStatus = companiesController.Ok(16);

            Assert.AreEqual(dumpOkStatus.GetType(), okStatus.Result.GetType());
        }
        [Test]
        public void Create_CompanyCreated_Fail()
        {
            bool exception = false;

            try
            {
                var com = companiesController.Create(company2);
            }
            catch
            {
                exception = true;
            }

            Assert.IsTrue(exception);
        }
    }
}
