using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Entities.EntityConfig
{
    public class AccountEntityConfig : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AccountName).IsRequired();
            builder.Property(a => a.AccountNumber).IsRequired();
            builder.HasOne(a => a.UserEntity).WithMany(u => u.AccountEntities).HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
