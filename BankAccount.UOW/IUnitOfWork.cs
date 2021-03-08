using BankAccount.Entities;
using System;
using System.Threading.Tasks;

namespace BankAccount.UOW
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        Task<bool> BeginTransactionAsync(Action action);
        AccountDbContext GetDbContext();
    }
}
