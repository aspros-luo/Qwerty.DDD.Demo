using System.Collections.Generic;
using System.Threading.Tasks;
using Qwerty.DDD.Domain;

namespace Qwerty.DDD.Application.Interfaces.UserServiceInterfaces
{
    public interface IUserService
    {
        Task<bool> Add(User identity);
        Task<bool> Update(User identity);
        Task<bool> Delete(User identity);

        Task<User> GetIdentityById(long id);
        Task<List<User>> GetIdentituByIds(List<long> ids);
        Task<bool> ValiedUser(string userName, string password);
        Task<User> Login(string userName, string password);
        Task<bool> Identity(long userId, string realName, string identityNo);
    }
}
