using BankAccount.DTOS.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DTOS.Account
{
    public class EditAccountDto:BaseDTO
    {
        [Required(ErrorMessage ="AccountName cannot be empty")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "AccountNumber cannot be empty")]
        public Guid AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }

        [Required(ErrorMessage = "UserId cannot be empty")]
        public Guid UserId { get; set; }
        public EditUserDto UserEntity { get; set; }
    }
}
