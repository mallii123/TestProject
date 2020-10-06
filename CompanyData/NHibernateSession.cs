using NHibernate;
using NHibernate.Cfg;

namespace CompanyData
{
    class NHibernateSession
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory(); 
                return _sessionFactory;
            }
        }
        private static void InitializeSessionFactory()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();
            _sessionFactory = cfg.BuildSessionFactory();       
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
