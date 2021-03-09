using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.API.CustomException
{
    public class BankAccountException:Exception
    {
        public BankAccountException()
        {

        }
        public BankAccountException(CustomeServerErrorMsg errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        public CustomeServerErrorMsg ErrorMsg { get; }
    }
}
