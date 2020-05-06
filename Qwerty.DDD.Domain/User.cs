using System;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Domain.Core.Interface;
using Framework.Infrastructure.Interfaces.Core.Event;
using Framework.Infrastructure.Ioc.Core;
using Microsoft.Extensions.DependencyInjection;
using Qwerty.DDD.Domain.Events;

namespace Qwerty.DDD.Domain
{
    public class User : IAggregateRoot
    {
        private IEventBus _eventBus;

        public User() { }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public long Id { get; set; }

        public string Name { get;private set; }

        public string Password { get;private set; }

        public virtual UserIdentity UserIdentity { get; set; }

        public async Task Identity()
        {
            try
            {
                var userIdentity = Mapper.Map<UserIdentity>(this);
                _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
                await _eventBus.Publish(userIdentity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
