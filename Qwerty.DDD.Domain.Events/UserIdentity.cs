using Framework.Domain.Core.Interface;
using Framework.Infrastructure.Interfaces.Core.Event;

namespace Qwerty.DDD.Domain.Events
{
    public class UserIdentity : IAggregateRoot, IEvent
    {
        public long UserId { get; set; }
        public string RealName { get; set; }
        public string IdentityNo { get; set; }
    }
}
