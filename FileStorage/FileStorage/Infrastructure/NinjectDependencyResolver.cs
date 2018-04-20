namespace FileStorage.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DAL;
    using DAL.Interfaces;
    using Ninject;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
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
            kernel.Bind<IFileRepository>().To<FileRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<IAccessRepository>().To<AccessRepository>();
        }
    }
}