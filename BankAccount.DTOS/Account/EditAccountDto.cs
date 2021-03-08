using System;
using System.Collections.Generic;
using System.Text;
using BankAccount.DTOS.User;

namespace BankAccount.DTOS.Account
{
    public class EditAccountDto:BaseDTO
    {
        public string AccountName { get; set; }
        public Guid AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public Guid UserId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
