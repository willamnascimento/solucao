﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solucao.Application.Data;

namespace Solucao.Application.Migrations
{
    [DbContext(typeof(SolucaoContext))]
    partial class SolucaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Solucao.Application.Data.Entities.Calendar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("EquipamentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("NoCadastre")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("TechniqueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TemporaryName")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TravelOn")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DriverId");

                    b.HasIndex("EquipamentId");

                    b.HasIndex("TechniqueId");

                    b.HasIndex("UserId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.CalendarSpecifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecificationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.HasIndex("SpecificationId");

                    b.ToTable("CalendarSpecifications");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("CellPhone")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(10);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("ClinicCellPhone")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("ClinicName")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(50);

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(18)")
                        .HasMaxLength(18);

                    b.Property<string>("Complement")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(30);

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("EquipamentValues")
                        .HasColumnType("varchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<bool?>("Has220V")
                        .HasColumnType("bit");

                    b.Property<bool?>("HasAirConditioning")
                        .HasColumnType("bit");

                    b.Property<bool?>("HasStairs")
                        .HasColumnType("bit");

                    b.Property<bool?>("HasTechnique")
                        .HasColumnType("bit");

                    b.Property<string>("Ie")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<bool?>("IsAnnualContract")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPhysicalPerson")
                        .HasColumnType("bit");

                    b.Property<int?>("IsReceipt")
                        .HasColumnType("int");

                    b.Property<string>("LandMark")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(50);

                    b.Property<string>("NameForReceipt")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Neighborhood")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(30);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Responsible")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(70);

                    b.Property<string>("Rg")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Secretary")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Specialty")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<bool?>("TakeTransformer")
                        .HasColumnType("bit");

                    b.Property<string>("TechniqueOption1")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("TechniqueOption2")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("ZipCode")
                        .HasColumnType("varchar(9)")
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Equipament", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Equipaments");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.EquipamentSpecifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("EquipamentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpecificationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EquipamentId");

                    b.HasIndex("SpecificationId");

                    b.ToTable("EquipamentSpecifications");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Specification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Single")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Specifications");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("varchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.StickyNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Resolved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StickyNotes");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("varchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Calendar", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solucao.Application.Data.Entities.Person", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("Solucao.Application.Data.Entities.Equipament", "Equipament")
                        .WithMany()
                        .HasForeignKey("EquipamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solucao.Application.Data.Entities.Person", "Technique")
                        .WithMany()
                        .HasForeignKey("TechniqueId");

                    b.HasOne("Solucao.Application.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.CalendarSpecifications", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.Calendar", null)
                        .WithMany("CalendarSpecifications")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solucao.Application.Data.Entities.Specification", "Specification")
                        .WithMany("CalendarSpecifications")
                        .HasForeignKey("SpecificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.City", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.Client", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solucao.Application.Data.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.EquipamentSpecifications", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.Equipament", "Equipament")
                        .WithMany("EquipamentSpecifications")
                        .HasForeignKey("EquipamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solucao.Application.Data.Entities.Specification", "Specification")
                        .WithMany("EquipamentSpecifications")
                        .HasForeignKey("SpecificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Solucao.Application.Data.Entities.StickyNote", b =>
                {
                    b.HasOne("Solucao.Application.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
