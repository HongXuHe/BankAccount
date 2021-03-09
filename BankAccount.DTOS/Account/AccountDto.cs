using BankAccount.DTOS.User;
using System;

namespace BankAccount.DTOS.Account
{
    public class AccountDto:BaseDTO
    {
        public string AccountName { get; set; }
        public Guid AccountNumber { get; set; } 
        public decimal CurrentBalance { get; set; }
        public Guid UserId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
