using AutoMapper;
using BankAccount.DTOS.Account;
using BankAccount.DTOS.User;
using BankAccount.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.API.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            #region User Mapper
            CreateMap<AddUserDto, UserEntity>().ReverseMap();
            CreateMap<EditUserDto, UserEntity>().ReverseMap();
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<PatchUserDto, EditUserDto>();
            #endregion

            #region Account Mapper
            CreateMap<AddAccountDto, AccountEntity>().ReverseMap();
            CreateMap<EditAccountDto, AccountEntity>().ReverseMap();
            CreateMap<AccountDto, AccountEntity>().ReverseMap(); 
            #endregion
        }
    }
}
