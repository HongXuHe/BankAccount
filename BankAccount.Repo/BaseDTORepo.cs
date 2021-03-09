using AutoMapper;
using BankAccount.DTOS;
using BankAccount.Entities;
using BankAccount.IRepo;
using BankAccount.UOW;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Repo
{
    public class BaseDTORepo<TEntity, TAddDto, TEditDto> :BaseDTORepo<TEntity>,IBaseDTORepo<TEntity,TAddDto,TEditDto>
        where TEntity:BaseEntity
        where TAddDto : BaseDTO
        where TEditDto :BaseDTO
    {
        #region props and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseDTORepo<TEntity>> _logger;
        private readonly IMapper _mapper;

        public BaseDTORepo(IUnitOfWork unitOfWork, ILogger<BaseDTORepo<TEntity>> logger, IMapper mapper)
            : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        } 
        #endregion

        //create by dto
        public async Task<bool> CreateByDTOAsync(TAddDto addDto)
        {
            try
            {
                var entityToAdd = _mapper.Map<TEntity>(addDto);
                await CreateAsync(entityToAdd);
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //edit by dto

        public async Task<bool> EditByDTOAsync(TEditDto editDto)
        {
            try
            {
                //check if entity exists
                if(!_unitOfWork.GetDbContext().Set<TEntity>().Any(x=>x.Id==editDto.Id))
                {
                    return await Task.FromResult(false);
                }
                var entityToEdit = _mapper.Map<TEntity>(editDto);
                await EditAsync(entityToEdit);
                return await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
