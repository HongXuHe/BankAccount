using BankAccount.DTOS.User;
using BankAccount.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankAccount.IRepo
{
    public interface IUserRepo:IBaseDTORepo<UserEntity,AddUserDto,EditUserDto>
    {
        IQueryable<UserEntity> GetUsersWithAccounts(Expression<Func<UserEntity, bool>> whereLambda);
        bool UserExists(string userName);
        bool UserExists(Guid guid);
    }
}
