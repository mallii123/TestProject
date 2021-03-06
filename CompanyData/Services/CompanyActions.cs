﻿using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using CompanyData.Models;
using CompanyData.Designs;
using CompanyData.Helpers;
using log4net;

namespace CompanyData.Services
{
    public class CompanyActions : ICompanyRequiredHttpActions
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public virtual long Create(Company company)
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
        public virtual List<Company> Search(CompanySearchParameters CompanySearchParameters)
        {
            List<Company> companies = new List<Company>();
            try
            {                               
                using (ISession session = NHibernateSession.OpenSession())
                {
                    companies = CompaniesFiltrs.GetCompaniesByAllParameters(session, CompanySearchParameters);
                }                              
            }
            catch(Exception e)
            {
                _log.Error("Error in List<Company> Search(CompanySearchParameters CompanySearchParameters)", e);
            }
            return companies;
        }

        public virtual bool Update(Company company)
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
        public virtual bool Delete(long id)
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

