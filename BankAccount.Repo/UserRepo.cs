using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankAccount.DTOS.User;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAccount.Repo
{
    public class UserRepo : BaseRepo<UserEntity>, IUserRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseRepo<UserEntity>> _logger;

        public UserRepo(IUnitOfWork unitOfWork, ILogger<BaseRepo<UserEntity>> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task CreateUserByDTOAsync(AddUserDto addUserDto)
        {
            throw new NotImplementedException();
        }

        public Task EditUserByDTOAsync(EditUserDto editUserDto)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserEntity> GetUsersWithAccounts(Expression<Func<UserEntity, bool>> whereLambda)
        {
            return _unitOfWork.GetDbContext().UserEntities.Include(u => u.AccountEntities).Where(whereLambda);
        }
    }
}
