using System;

namespace BankAccount.Entities
{
    public class AccountEntity:BaseEntity
    {
        #region props
        public string AccountName { get; set; }
        public Guid AccountNumber { get; set; } = Guid.NewGuid();
        public decimal CurrentBalance { get; set; }
        #endregion

        #region Nav props
        public Guid UserId { get; set; }
        public UserEntity UserEntity { get; set; } 
        #endregion
    }
}
