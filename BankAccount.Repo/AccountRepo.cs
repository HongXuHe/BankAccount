using AutoMapper;
using BankAccount.DTOS.Account;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAccount.Repo
{
    public class AccountRepo : BaseDTORepo<AccountEntity, AddAccountDto, EditAccountDto>, IAccountRepo
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

        /// <summary>
        /// return querable users with accounts
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<AccountEntity> GetUserWithAccounts(Expression<Func<AccountEntity, bool>> whereLambda)
        {
            return _unitOfWork.GetDbContext().AccountEntities.Include(a => a.UserEntity).Where(whereLambda);
        }

        /// <summary>
        /// check accound exists 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> AccountExists(Guid userId, Guid accountId)
        {
            return await _unitOfWork.GetDbContext().UserEntities.Include(x => x.AccountEntities).AnyAsync(x =>
                 x.Id == userId && x.AccountEntities.Any(y => y.Id == accountId));
        }

        public override async Task<bool> CreateByDTOAsync(AddAccountDto addDto)
        {
            var user = await _unitOfWork.GetDbContext().UserEntities.Include(u => u.AccountEntities).SingleOrDefaultAsync(x => x.Id == addDto.UserEntity.Id);
            var accountEntity = _mapper.Map<AccountEntity>(addDto);
            user.AccountEntities.Add(accountEntity);
            return await _unitOfWork.CommitAsync();

        }
    }
}
