using NHibernate;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViniciusMaiaITIXWebApp.DAO;
using ViniciusMaiaITIXWebApp.DAO.Factory;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Service;

namespace ViniciusMaiaITIXWebApp.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<ISessionFactory>().ToMethod(context => new SessionFactoryProvider().CriaSessionFactory());
            kernel.Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession());
            kernel.Bind<IConsultaDAO>().To<ConsultaDAO>();
            kernel.Bind<IPacienteDAO>().To<PacienteDAO>();
            kernel.Bind<IPacienteService>().To<PacienteService>();
            kernel.Bind<IConsultaService>().To<ConsultaService>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}