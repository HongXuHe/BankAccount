using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Entities
{
    public class UserEntity : BaseEntity
    {
        #region props

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    
        #endregion

        #region Nav props
        public List<AccountEntity> AccountEntities { get; set; } = new List<AccountEntity>();
        #endregion
    }
}
