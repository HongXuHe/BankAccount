using BankAccount.DTOS;
using BankAccount.Entities;
using System.Threading.Tasks;

namespace BankAccount.IRepo
{
    public interface IBaseDTORepo<TEntity, TAddDto, TEditDTo> : IBaseDTORepo<TEntity>
        where TEntity : BaseEntity
        where TAddDto : BaseDTO
        where TEditDTo:BaseDTO
    {
        Task<bool> CreateByDTOAsync(TAddDto addDto);
        Task<bool> EditByDTOAsync(TEditDTo editDto);
    }
}
