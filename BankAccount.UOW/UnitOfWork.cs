using BankAccount.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        #region ctor and props
        private readonly AccountDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(AccountDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// use transaction
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<bool> BeginTransactionAsync(Action action)
        {
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    action?.Invoke();
                    await tran.CommitAsync();
                    return await Task.FromResult(true);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    _logger.LogError(e.Message, e);
                    return await Task.FromResult(false);
                }

            }
        }

        /// <summary>
        /// commit database changes
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// return dbcontext
        /// </summary>
        /// <returns></returns>
        public AccountDbContext GetDbContext()
        {
            return _context;
        }
    }
}
