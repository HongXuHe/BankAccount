using BankAccount.DTOS.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DTOS.Account
{
    public class AddAccountDto:BaseDTO
    {
        [Required(ErrorMessage = "AccountName cannot be empty")]
        public string AccountName { get; set; }
        public Guid AccountNumber { get; set; } = Guid.NewGuid();
        public decimal CurrentBalance { get; set; } = default;

        [Required(ErrorMessage = "UserId cannot be empty")]
        public Guid UserId { get; set; }
        public AddUserDto UserEntity { get; set; }
    }
}
