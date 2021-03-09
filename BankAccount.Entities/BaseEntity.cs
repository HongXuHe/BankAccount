using System;

namespace BankAccount.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// key for all entities
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}
