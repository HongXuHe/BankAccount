using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankAccount.API.CustomException;
using BankAccount.DTOS.User;
using BankAccount.IRepo;
using BankAccount.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region ctor and props
        private static object _lock = new object();
        private readonly IUserRepo _userRepo;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserRepo userRepo, ILogger<UserController> logger, IMapper mapper)
        {
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            var userList = await _userRepo.GetUsersWithAccounts(u => true).ToListAsync();
            if (userList.Count > 0)
            {
                var userListDto = _mapper.Map<List<UserDto>>(userList);
                return Ok(userListDto);
            }
            return Ok("no users in the system yet");
        }

        [HttpPost("")]
        public IActionResult AddUser(AddUserDto addUserDto)
        {


            try
            {
                if (_userRepo.UserExists(addUserDto.UserName.Trim()))
                {
                    return BadRequest("User already exists");
                }
                lock (_lock) //use lock to avoid parallel problem
                {
                    var result = _userRepo.CreateByDTOAsync(addUserDto).GetAwaiter().GetResult();
                    if (result)
                    {
                        _logger.LogInformation($"Create user {addUserDto.UserName} successfully");
                        return Ok("Create successfully");
                    }
                }
                return StatusCode(500);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> EditAddress(Guid userId, [FromBody] JsonPatchDocument<PatchUserDto> patchDocument)
        {
            if (!_userRepo.UserExists(userId))
            {
                return BadRequest("user not exists");
            }
            var userFromDb = await _userRepo.GetEntities(x => true).SingleOrDefaultAsync(x => x.Id == userId);
            var editUserDto = _mapper.Map<EditUserDto>(userFromDb);
            var patchUser = new PatchUserDto();
            patchDocument.ApplyTo(patchUser, ModelState);
           var appliedUser = _mapper.Map<EditUserDto>(patchUser);
            if (!TryValidateModel(appliedUser))
            {
                return ValidationProblem(ModelState);
            }
            //check if state and postcode match
            var resultMatch = Utility.CheckAddressMatch((state, postcode) =>
             {
                 return true;
             });
            if (!resultMatch)
            {
                return BadRequest("Postcode and state are not match");
            }
            lock (_lock)
            {
                var result = _userRepo.EditByDTOAsync(editUserDto).GetAwaiter().GetResult();
                if (result)
                {
                    return Ok("Update success");
                }
            }

            return new JsonResult(new CustomeServerErrorMsg(DateTime.UtcNow,"Server error"));
        }
    }
}
