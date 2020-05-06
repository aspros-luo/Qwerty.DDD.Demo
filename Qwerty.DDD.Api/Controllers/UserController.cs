using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qwerty.DDD.Application.Interfaces.UserServiceInterfaces;

namespace Qwerty.DDD.Api.Controllers
{
    [Route(Program.ServiceName)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("user.valied")]
        [HttpGet]
        public async Task<IActionResult> ValiedUser(string name, string password)
        {
            var result = await _userService.ValiedUser(name, password);
            return Ok(result);
        }

        [Route("user.inside.login")]
        [HttpGet]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await _userService.Login(name, password);
            return Ok(new { Succeed = true, Data = user, Message = "" });
        }


        [Route("user.identity.valied")]
        [HttpGet]
        public async Task<IActionResult> Identity(long userId, string realName, string idetntityNo)
        {
            var result = await _userService.Identity(userId,realName, idetntityNo);
            return Ok(new { Succeed = result});
        }
    }
}
