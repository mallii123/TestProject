using System.Collections.Generic;
using System.Linq;
using NHibernate;
using CompanyData.Models;
using CompanyData.Design;

namespace CompanyData.Data
{
    public class EmployeeActions : IRequiredHttpActions<Employee>
    {
        public long Create(Employee employee)
        {
            try
            {
                using (var mySession = NHibernateSession.OpenSession())
                {
                    using (var transaction = mySession.BeginTransaction())
                    {
                        mySession.Save(employee);
                        transaction.Commit();
                    }
                }             
                return employee.ID;
            }
            catch
            {
                return -1;
            }
        }
        public List<Employee> Search(CompanySearchParameters CompanySearchParameters)
        {
            List<Employee> employees = new List<Employee>();
            try
            {               
                using (NHibernate.ISession session = NHibernateSession.OpenSession())
                {
                    employees = session.Query<Employee>().ToList();
                }
                return employees;
            }
            catch
            {
                return employees;
            }
        }
        public bool Update(Employee employee)
        {
            try
            {
                using (NHibernate.ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(employee);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                Employee employee = new Employee();
                using (NHibernate.ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        employee = session.Query<Employee>().Where(b => b.ID == id).FirstOrDefault();
                        session.Delete(employee);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}

