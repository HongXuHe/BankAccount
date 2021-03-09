using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankAccount.DTOS.Account;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.Extensions.Logging;

namespace BankAccount.Repo
{
   public class AccountRepo: BaseDTORepo<AccountEntity, AddAccountDto, EditAccountDto>, IAccountRepo
    {
        #region ctor and props
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseDTORepo<AccountEntity>> _logger;
        private readonly IMapper _mapper;

        public AccountRepo(IUnitOfWork unitOfWork, ILogger<BaseDTORepo<AccountEntity>> logger, IMapper mapper)
            : base(unitOfWork, logger, mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        } 
        #endregion

    }
}
