using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using CompanyData.Models;
using CompanyData.Design;
using CompanyData.Helpers;

namespace CompanyData.Data
{
    public class CompanyActions : ICompanyRequiredHttpActions
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public long Create(Company company)
        {
            try
            {
                using (var mySession = NHibernateSession.OpenSession())
                {
                    using (var transaction = mySession.BeginTransaction())
                    {
                        mySession.Save(company);
                        transaction.Commit();
                    }
                }             
                return company.ID;
            }
            catch(Exception e)
            {
                _log.Error("Error in long Create(Company company)", e);
                return -1;
            }
        }
        public List<Company> Search(CompanySearchParameters CompanySearchParameters)
        {
            List<Company> companies = new List<Company>();
            try
            {               
                using (ISession session = NHibernateSession.OpenSession())
                {
                    companies = session.Query<Company>().ToList();
                }
                CompaniesFiltrs CompaniesFiltrs = new CompaniesFiltrs(companies, CompanySearchParameters);
                return CompaniesFiltrs.GetFiltredCompaniesByParameters();
            }
            catch(Exception e)
            {
                _log.Error("Error in List<Company> Search(CompanySearchParameters CompanySearchParameters)", e);
                return companies;
            }
        }

        public bool Update(Company company)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(company);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                _log.Error("Error in bool Update(Company company)", e);
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                Company company = new Company();
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        company = session.Query<Company>().Where(b => b.ID == id).FirstOrDefault();
                        session.Delete(company);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                _log.Error("Error in bool Delete(long id)", e);
                return false;
            }
            
        }
    }
}

