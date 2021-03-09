using BankAccount.DTOS;
using BankAccount.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAccount.IRepo
{
    public interface IBaseDTORepo<TEntity> where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task EditAsync(TEntity entity);
        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> whereLambda);
    }
}
