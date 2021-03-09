using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.API
{
    [Serializable]
    public class CustomeServerErrorMsg 
    {
        public DateTime ReferenceNumber { get; set; }
        public string Message { get; set; }
        public CustomeServerErrorMsg(DateTime referenceNumber,string message)
            
        {
            this.ReferenceNumber = referenceNumber;
            this.Message = message;
        }
    }
}
