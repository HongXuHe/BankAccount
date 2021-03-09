using BankAccount.DTOS.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DTOS.User
{
    public class AddUserDto:BaseDTO
    {
        [Required(ErrorMessage = "FirstName cannot be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cannot be empty")]
        public string LastName { get; set; }
        public string UserName => FirstName + " " + LastName;
        public string State { get; set; }
        public string PostCode { get; set; }

        public List<AddAccountDto> AccountEntities { get; set; } = new List<AddAccountDto>();
    }
}
