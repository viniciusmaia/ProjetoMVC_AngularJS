using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Ninject.Activation;
using ViniciusMaiaITIXWebApp.DAO.Map;

namespace ViniciusMaiaITIXWebApp.DAO.Factory
{
    public class SessionFactoryProvider
    {
        public ISessionFactory CriaSessionFactory()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS; Server=localhost; Initial Catalog=VINICIUSMAIAITIXDB; 
                                        Persist Security Info=True; Integrated Security=SSPI;";
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql()).
                    Mappings(m => m.FluentMappings.AddFromAssemblyOf<PacienteMap>()).
                    ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false)).BuildSessionFactory();
        }
    }
}