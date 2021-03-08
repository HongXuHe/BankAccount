using System;

namespace BankAccount.DTOS
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
