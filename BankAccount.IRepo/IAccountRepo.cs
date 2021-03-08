using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BankAccount.DTOS.Account;
using BankAccount.DTOS.User;

namespace BankAccount.IRepo
{
    public interface IAccountRepo
    {
        Task CreateAccountByDTOAsync(AddAccountDto addAccountDto);
        Task EditAccountByDTOAsync(EditAccountDto editAccountDto);
    }
}
