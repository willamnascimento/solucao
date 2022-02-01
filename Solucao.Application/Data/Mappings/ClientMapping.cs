using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CellPhone)
                .HasColumnType("varchar(15)")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime");


            builder.Property(c => c.Active)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasColumnType("varchar(14)")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnType("varchar(70)")
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(c => c.Number)
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.Complement)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            builder.Property(c => c.CityId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.StateId)
                .HasColumnType("int")
                .IsRequired();


        }
    }
}
