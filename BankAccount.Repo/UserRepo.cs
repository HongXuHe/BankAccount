using AutoMapper;
using BankAccount.DTOS.User;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankAccount.Repo
{
    public class UserRepo : BaseDTORepo<UserEntity,AddUserDto,EditUserDto>, IUserRepo
    {
        #region props and ctor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseDTORepo<UserEntity>> _logger;
        private readonly IMapper _mapper;

        public UserRepo(IUnitOfWork unitOfWork, ILogger<BaseDTORepo<UserEntity>> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        } 
        #endregion

        //get user along with accounts
        public IQueryable<UserEntity> GetUsersWithAccounts(Expression<Func<UserEntity, bool>> whereLambda)
        {
            return _unitOfWork.GetDbContext().UserEntities.Include(u => u.AccountEntities).Where(whereLambda);
        }

        /// <summary>
        /// check user exists according to username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserExists(string userName)
        {
          return  _unitOfWork.GetDbContext().UserEntities.Any(x=>x.UserName==userName);
        }

        /// <summary>
        /// check user exist according to id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool UserExists(Guid guid)
        {
            return _unitOfWork.GetDbContext().UserEntities.Any(x => x.Id == guid);
        }
    }
}
