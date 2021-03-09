using BankAccount.DTOS.Account;
using System.Collections.Generic;

namespace BankAccount.DTOS.User
{
    public class UserDto:BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public List<AccountDto> AccountEntities { get; set; } = new List<AccountDto>();
    }
}
