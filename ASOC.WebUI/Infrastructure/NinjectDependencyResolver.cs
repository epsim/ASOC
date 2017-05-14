using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IRepository<COMPONENT>>().To<Infrastructure.Repositories.ComponentRepository>();
            kernel.Bind<IRepository<CURRENT_STATUS>>().To<Infrastructure.Repositories.CurrentStatusRepository>();
            kernel.Bind<IRepository<MODEL>>().To<Infrastructure.Repositories.ModelRepository>();
            kernel.Bind<IRepository<PRICE>>().To<Infrastructure.Repositories.PriceRepository>();
            kernel.Bind<IRepository<STATUS>>().To<Infrastructure.Repositories.StatusRepository>();
            kernel.Bind<IRepository<TYPE>>().To<Infrastructure.Repositories.TypeRepository>();
            kernel.Bind<IGetList>().To<Infrastructure.Concrete.GetList>();
        }
    }
}