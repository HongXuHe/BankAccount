using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.DTOS.User
{
    public class EditUserDto:BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
