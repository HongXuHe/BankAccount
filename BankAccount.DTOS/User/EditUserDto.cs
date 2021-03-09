using BankAccount.DTOS.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankAccount.DTOS.User
{
    public class EditUserDto:BaseDTO
    {

        public string FirstName { get;  }


        public string LastName { get;  }
        public string UserName => FirstName + " " + LastName;
        public string State { get; set; }
        public string PostCode { get; set; }
        public List<EditAccountDto> AccountEntities { get; } = new List<EditAccountDto>();
    }
}
