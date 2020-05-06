using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Infrastructure.Interfaces.Core;
using Framework.Infrastructure.Interfaces.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Qwerty.DDD.Application.Interfaces.UserServiceInterfaces;
using Qwerty.DDD.Domain;
using Qwerty.DDD.Domain.Events;
using Qwerty.DDD.Domain.Repository.Interfaces.UserRepositoryInterfaces;

namespace Qwerty.DDD.Application.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<bool> Add(User identity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.RegisterNew(identity);
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> Delete(User identity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.RegisterDeleted(identity);
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Task<List<User>> GetIdentituByIds(List<long> ids)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetIdentityById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(User identity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.RegisterDirty(identity);
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> ValiedUser(string userName, string password)
        {
            return await _userRepository.ValidUser(userName, password).AnyAsync();
        }

        public async Task<User> Login(string userName, string password)
        {
            return await _userRepository.ValidUser(userName, password).FirstOrDefaultAsync();
        }

        public async Task<bool> Identity(long userId, string realName, string identityNo)
        {
            var user = await _userRepository.GetIdentityById(userId).FirstOrDefaultAsync();
            user.UserIdentity = new UserIdentity() { UserId = userId, RealName = realName, IdentityNo = identityNo };
            await user.Identity();
            return true;
        }
    }
}
