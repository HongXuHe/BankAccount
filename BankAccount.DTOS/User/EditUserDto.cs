using BankAccount.DTOS.Account;
using System.Collections.Generic;

namespace BankAccount.DTOS.User
{
    public class EditUserDto:BaseDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public List<EditAccountDto> AccountEntities { get; set; } = new List<EditAccountDto>();
    }
}
