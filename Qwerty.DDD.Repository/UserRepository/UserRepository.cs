using System.Linq;
using Framework.Domain.Core;
using Framework.Infrastructure.Interfaces.Core.Interface;
using Qwerty.DDD.Domain;
using Qwerty.DDD.Domain.Repository.Interfaces.UserRepositoryInterfaces;

namespace Qwerty.DDD.Repository.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<User> GetIdentityById(long id)
        {
            return Entities.Where(i => i.Id == id);
        }

        public IQueryable<User> ValidUser(string userName, string password)
        {
            return Entities.Where(a => a.Name == userName && a.Password == password);
        }
    }
}
