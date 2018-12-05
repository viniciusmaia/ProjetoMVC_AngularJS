using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ViniciusMaiaITIXWebApp.DAO.Map;

namespace ViniciusMaiaITIXWebApp.Tests.DAOs
{
    public class SessionTestFactory
    {
        public static ISession OpenSessionTest()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS; Server=localhost; Initial Catalog=VINICIUSMAIAITIXDB_TEST; 
                                        Persist Security Info=True; Integrated Security=SSPI;";
            ISessionFactory sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql()).
                    Mappings(m => m.FluentMappings.AddFromAssemblyOf<PacienteMap>()).
                    ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false)).BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
