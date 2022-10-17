using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query.Identity;
using MozaeekCore.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Enum;
using MozaeekCore.ViewModel;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    [Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
    public class UserController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IUserQueryFacade userQuery;

        public UserController(ICommandBus commandBus,
                              IUserQueryFacade userQuery)
        {
            this.commandBus = commandBus;
            this.userQuery = userQuery;
        }
        [HttpGet("{id}")]
        public Task<UserDto> GetById(long id)
        {
            return userQuery.GetUserById(id);
        }
        [HttpGet]
        public Task<PagedListResult<UserDto>> GetAll([FromQuery]PagingContract pagingContract)
        {
            return userQuery.GetUserDtos(pagingContract);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var commandResult = await  commandBus.DispatchAsync<RegisterUserCommand, RegisterUserCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.UserId }, commandResult.UserId);
        }

        //DropDownDto
        [HttpGet]
        public async Task<InitUserDto> GetUserInitDto()
        {
            var res = new InitUserDto
            {
                Roles = new List<DropDownDto>()
                {
                    new DropDownDto(((int) CoreRole.Admin).ToString(), CoreRole.Admin.GetDisplayValue()),
                    new DropDownDto(((int) CoreRole.BasiInfo).ToString(), CoreRole.BasiInfo.GetDisplayValue()),
                    new DropDownDto(((int) CoreRole.Operation).ToString(), CoreRole.Operation.GetDisplayValue()),
                }
            };
            return res;
        }

        // [HttpPost("AddRole")]
        // public async Task<IActionResult> AssignRole(AssignRoleToUserCommand command)
        // {
        //     var commandResult = await commandBus.DispatchAsync<AssignRoleToUserCommand, AssignRoleToUserCommandResult>(command);
        //     return Ok(commandResult);
        // }
        // [HttpPost("RemoveRole")]
        // public async Task<IActionResult> UnAssignRole(UnAssignRoleToUserCommand command)
        // {
        //     var commandResult = await commandBus.DispatchAsync<UnAssignRoleToUserCommand, UnAssignRoleToUserCommandResult>(command);
        //     return Ok(commandResult);
        // }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserInfoCommand command)
        {
            var commandResult = await commandBus.DispatchAsync<UpdateUserInfoCommand, UpdateUserInfoCommandResult>(command);
            return Ok(commandResult);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] RemoveUserCommand command)
        {
            var commandResult = await commandBus.DispatchAsync<RemoveUserCommand, RemoveUserCommandResult>(command);
            return Ok(commandResult);
        }
    }
}
