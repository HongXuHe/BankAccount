using System.ComponentModel.DataAnnotations;

namespace BankAccount.DTOS.Account
{
    public class UpdateAccountDto
    {
        [Required]
        public decimal CurrentBalance { get; set; }
    }
}
