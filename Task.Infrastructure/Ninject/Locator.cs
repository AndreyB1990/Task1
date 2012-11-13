using Ninject;
using Ninject.Modules;

namespace Task.Infrastructure.Ninject
{
    public static class Locator
    {
        /// <summary>
        /// IoC container
        /// </summary>
        public static IKernel Container { get; private set; }

        /// <summary>
        /// Initializes the IoC container specified modules
        /// </summary>
        /// <param name="modules"></param>
        public static void Init(params INinjectModule[] modules)
        {
            Container = new StandardKernel(modules);
        }

        /// <summary>
        /// Gets the implementation or instant type using IoC container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return Container.Get<T>();
        }
    }
}