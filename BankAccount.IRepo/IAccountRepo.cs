using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BankAccount.DTOS.Account;
using BankAccount.DTOS.User;
using BankAccount.Entities;

namespace BankAccount.IRepo
{
    public interface IAccountRepo:IBaseDTORepo<AccountEntity, AddAccountDto, EditAccountDto>
    {
    }
}
