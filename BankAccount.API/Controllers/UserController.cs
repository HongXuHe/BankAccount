using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccount.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepo userRepo, ILogger<UserController> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
        //    _logger.LogInformation("ss");
            _logger.LogError("this is error");
           var userList = await _userRepo.GetUsersWithAccounts(u => true).ToListAsync();
            return Ok(userList);
        }
    }
}
