using Microsoft.Practices.Unity;
using SpaUserControl.Business.Services;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Domain.Services;
using SpaUserControl.Infraestructure.Data;
using SpaUserControl.Infraestructure.Repositories;

namespace SpaUserControl.Startup
{
    public class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<DbDataContext, DbDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            container.RegisterType<User, User>(new HierarchicalLifetimeManager());
        }
    }
}
