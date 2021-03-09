using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAccount.Repo
{
    public abstract class BaseDTORepo<TEntity> : IBaseDTORepo<TEntity> where TEntity : BaseEntity
    {
        #region ctor and props, fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseDTORepo<TEntity>> _logger;
        private readonly AccountDbContext _context;

        public BaseDTORepo(IUnitOfWork unitOfWork, ILogger<BaseDTORepo<TEntity>> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = unitOfWork?.GetDbContext();
        } 
        #endregion

        /// <summary>
        /// create entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

        }

        /// <summary>
        /// update entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task EditAsync(TEntity entity)
        {
            await Task.FromResult(_context.Set<TEntity>().Update(entity));

        }

        /// <summary>
        /// get entities
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> whereLambda)
        {
            return _context.Set<TEntity>().Where(whereLambda);
        }
    }
}
