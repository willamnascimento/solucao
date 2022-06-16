﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Mappings
{
    public class CalendarMapping : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.ClientId)
                .IsRequired();

            builder.Property(c => c.EquipamentId)
                .IsRequired();

            builder.Property(c => c.Note)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.DriverId);

            builder.Property(c => c.TechniqueId);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.Active)
                .IsRequired();

            builder.Property(c => c.ParentId);

            builder.Property(c => c.Date)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.StartTime)
                .HasColumnType("datetime");

            builder.Property(c => c.EndTime)
                .HasColumnType("datetime");

            builder.Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime");

            builder.Property(c => c.Status)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(c => c.NoCadastre)
                .HasColumnType("bit");

            builder.Property(c => c.TemporaryName)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.TravelOn)
                .HasColumnType("int");
        }
    }
}
