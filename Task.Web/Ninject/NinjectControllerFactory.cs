using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Task.Repositories.NinjectModules;
using Task.Services.NinjectModules;

namespace Task.Web.Ninject
{
    /// <summary>
    /// Controller factory that uses Ninject as IoC container
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Base constructor, which creates an IKernel instance, using Ninject modules
        /// </summary>
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel(new INinjectModule[]
                                             {
                                                 new ServiceModule(),
                                                 new RepositoryModule()
                                             });
        }

        /// <summary>
        /// Gets IController instance
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return (IController)_kernel.Get(controllerType);
        }
    }
}