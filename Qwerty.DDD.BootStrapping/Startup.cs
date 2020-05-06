using AutoMapper;
using Qwerty.DDD.Infrastructure.Repository;
using Framework.Infrastructure.Interfaces.Core;
using Framework.Infrastructure.Interfaces.Core.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qwerty.DDD.Application.Interfaces.UserServiceInterfaces;
using Qwerty.DDD.Application.UserServices;
using Qwerty.DDD.Domain;
using Qwerty.DDD.Domain.EventHandlers;
using Qwerty.DDD.Domain.Events;
using Qwerty.DDD.Domain.Repository.Interfaces.UserRepositoryInterfaces;
using Qwerty.DDD.Repository.UserRepository;
using Framework.Infrastructure.Interfaces.Core.Interface;

namespace Qwerty.DDD.BootStrapping
{
    public static class Startup
    {
        public static void Configure(this IServiceCollection service, string connectionString)
        {
//            service.AddEntityFrameworkSqlServer().AddDbContext<CommodityDbContext>(options => options.UseSqlServer(connectionString));

            service.AddDbContext<UserDbContext>(options =>options.UseMySQL(connectionString));

            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IDbContext, UserDbContext>();
            service.AddTransient<IWorkContext, WorkContext>();

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IUserService, UserService>();


            service.AddTransient<IEventBus, EventBus>();
            service.AddTransient<IEventHandler<UserIdentity>, UserEventHanler>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserIdentity>()
                    .ForMember(dest => dest.IdentityNo, opt => opt.MapFrom(src => src.UserIdentity.IdentityNo))
                    .ForMember(dest => dest.RealName, opt => opt.MapFrom(src => src.UserIdentity.RealName))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserIdentity.UserId))
                    ;
            });
        }
    }
}
