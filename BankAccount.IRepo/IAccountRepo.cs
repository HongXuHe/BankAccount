using BankAccount.DTOS.Account;
using BankAccount.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAccount.IRepo
{
    public interface IAccountRepo:IBaseDTORepo<AccountEntity, AddAccountDto, EditAccountDto>
    {
        IQueryable<AccountEntity> GetUserWithAccounts(Expression<Func<AccountEntity, bool>> whereLambda);
        Task<bool> AccountExists(Guid userId, Guid accountId);
    }
}
