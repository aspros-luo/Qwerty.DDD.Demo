using System;
using Framework.Infrastructure.Interfaces.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Qwerty.DDD.Domain.Events;
using System.Threading.Tasks;
using Framework.Infrastructure.Interfaces.Core.Event;
using Framework.Infrastructure.Ioc.Core;

namespace Qwerty.DDD.Domain.EventHandlers
{
    public class UserEventHanler : IEventHandler<UserIdentity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserEventHanler(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                _unitOfWork = ServiceLocator.Instance.GetService<IUnitOfWork>();
            }
            else
            {
                _unitOfWork = httpContextAccessor.HttpContext.RequestServices.GetService<IUnitOfWork>();
            }
        }

        public async Task Handle(UserIdentity @event)
        {
            await _unitOfWork.RegisterNew(@event);
            await _unitOfWork.CommitAsync();
        }
    }
}
