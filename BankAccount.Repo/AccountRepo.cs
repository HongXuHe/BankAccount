using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BankAccount.DTOS.Account;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.Extensions.Logging;

namespace BankAccount.Repo
{
   public class AccountRepo:BaseRepo<AccountEntity>, IAccountRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseRepo<AccountEntity>> _logger;

        public AccountRepo(IUnitOfWork unitOfWork, ILogger<BaseRepo<AccountEntity>> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork ?? throw  new  ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task CreateAccountByDTOAsync(AddAccountDto addAccountDto)
        {
            throw new NotImplementedException();
        }

        public Task EditAccountByDTOAsync(EditAccountDto editAccountDto)
        {
            throw new NotImplementedException();
        }
    }
}
