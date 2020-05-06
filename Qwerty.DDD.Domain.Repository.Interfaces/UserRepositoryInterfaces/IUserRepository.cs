using System.Linq;
using Framework.Domain.Core;

namespace Qwerty.DDD.Domain.Repository.Interfaces.UserRepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> GetIdentityById(long id);
        IQueryable<User> ValidUser(string userName, string password);
    }
}
