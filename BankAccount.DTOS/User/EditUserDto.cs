using BankAccount.DTOS.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
