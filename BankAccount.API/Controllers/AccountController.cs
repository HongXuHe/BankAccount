using AutoMapper;
using BankAccount.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/{userId}")]
    public class AccountController : ControllerBase
    {
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
    }
}
