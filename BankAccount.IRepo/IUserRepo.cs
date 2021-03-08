using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankAccount.DTOS.User;
using BankAccount.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace BankAccount.IRepo
{
    public interface IUserRepo:IBaseRepo<UserEntity>
    {
        Task CreateUserByDTOAsync(AddUserDto addUserDto);
        Task EditUserByDTOAsync(EditUserDto editUserDto);
        IQueryable<UserEntity> GetUsersWithAccounts(Expression<Func<UserEntity, bool>> whereLambda);
    }
}
