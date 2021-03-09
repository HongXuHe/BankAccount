using AutoMapper;
using BankAccount.DTOS.User;
using BankAccount.IRepo;
using BankAccount.Shared;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region ctor and props
        private static readonly object _lock = new object();
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

        /// <summary>
        /// get user list include accounts
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// get all the accounts of single user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet("{userId}")]
        public async Task<IActionResult> ViewAllUserAccounts(Guid userId)
        {
            var accountList = await _userRepo.GetUsersWithAccounts(a => a.Id == userId).ToListAsync();
            if (accountList.Count > 0)
            {
                var list = _mapper.Map<List<UserDto>>(accountList);
                return Ok(list);
            }
            return Ok("No account under this user or user not exist");
        }

        /// <summary>
        /// create user, only Username Admin can do this
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult AddUser(AddUserDto addUserDto)
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

        /// <summary>
        /// user to edit address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{userId}")]
        public async Task<IActionResult> EditAddress(Guid userId, [FromBody] JsonPatchDocument<PatchUserDto> patchDocument)
        {
            if (!_userRepo.UserExists(userId))
            {
                return BadRequest("user not exists");
            }
            var userFromDb = await _userRepo.GetEntities(x => true).SingleOrDefaultAsync(x => x.Id == userId);
            var editUserDto = _mapper.Map<EditUserDto>(userFromDb);
            var patchDto = new PatchUserDto();
            patchDocument.ApplyTo(patchDto, ModelState);
           var  editNewDto = _mapper.Map<PatchUserDto, EditUserDto>(patchDto, editUserDto);
            if (!TryValidateModel(editNewDto))
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
                var result = _userRepo.EditByDTOAsync(editNewDto).GetAwaiter().GetResult();
                if (result)
                {
                    _logger.LogInformation($"User {userFromDb.UserName} have update postcode and state");
                    return Ok("Update success");
                }
            }

            throw new Exception();
        }
    }
}
