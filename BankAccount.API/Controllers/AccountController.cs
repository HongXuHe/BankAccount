using AutoMapper;
using BankAccount.DTOS.Account;
using BankAccount.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BankAccount.DTOS.User;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/{userId}")]
    public class AccountController : ControllerBase
    {
        #region props and ctor
        private static readonly object _lockAccount = new object();
        private readonly IUserRepo _userRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public AccountController(IUserRepo userRepo,
            IAccountRepo accountRepo,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _userRepo = userRepo;
            _accountRepo = accountRepo;
            _mapper = mapper;
            _logger = logger;
        } 
        #endregion

        /// <summary>
        /// create account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addAccountDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        public  IActionResult CreateAccount(Guid userId, AddAccountDto addAccountDto)
        {
            if (!_userRepo.UserExists(userId)) //check if user exist
            {
                return BadRequest($"User with Id {userId} does not exist");
            }

            lock (_lockAccount)
            {
                var user = _userRepo.GetEntities(x => x.Id == userId).SingleOrDefaultAsync().Result;
               var userDto = _mapper.Map<AddUserDto>(user);
               addAccountDto.UserEntity = userDto;
               var result = _accountRepo.CreateByDTOAsync(addAccountDto).GetAwaiter().GetResult();
               if (result)
               {
                   _logger.LogInformation($"user {userId} Create account successfully");
                   return Ok("Create account successfully");
               }
            }

            throw new Exception();
        }

        /// <summary>
        /// deposit or widthdraw
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <param name="model"></param>
        /// <param name="deposit"></param>
        /// <returns></returns>
        [HttpPost("{accountId}")]
        public async Task<IActionResult> DepositAndWidthdraw(Guid userId, Guid accountId, [FromBody] UpdateAccountDto model, bool deposit =true)
        {
            if (! await _accountRepo.AccountExists(userId, accountId)) //check account exist
            {
                return BadRequest("Account does not exits");
            }
            
            lock (_lockAccount) //user lock or RabbitMQ
            {
                var accountFromDb =  _accountRepo.GetEntities(x => true).SingleOrDefaultAsync(x => x.Id == accountId).Result;
                var editAccountDto = _mapper.Map<EditAccountDto>(accountFromDb);
                if (deposit)
                {
                    editAccountDto.CurrentBalance += model.CurrentBalance;
                }
                else
                {
                    editAccountDto.CurrentBalance -= model.CurrentBalance;
                }

                if (editAccountDto.CurrentBalance > 0)
                {
                    var result = _accountRepo.EditByDTOAsync(editAccountDto).GetAwaiter().GetResult();
                    if (result)
                    {
                        _logger.LogInformation($"Deposit Success");
                        return Ok("Update success");
                    }
                }

                return BadRequest($"Cannot Withdraw or deposit");
            }

        }

       
    }
}
