﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentApplyProject.Persistence.Context;

#nullable disable

namespace PaymentApplyProject.Persistence.Migrations
{
    [DbContext(typeof(PaymentContext))]
    partial class PaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Bank", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<short>("BankId")
                        .HasColumnType("smallint");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<decimal>("LowerLimit")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("UpperLimit")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Company", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<short>("CompanyId")
                        .HasColumnType("smallint");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("ExternalCustomerId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("BankAccountId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("DepositRequestId")
                        .HasColumnType("integer");

                    b.Property<short>("DepositStatusId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DepositRequestId")
                        .IsUnique();

                    b.HasIndex("DepositStatusId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.DepositRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("CallbackUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("CompanyId")
                        .HasColumnType("smallint");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("FailedUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MethodType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SuccessUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UniqueTransactionId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UniqueTransactionIdHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("DepositRequests");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Role", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Status", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Status");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.UserCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<short>("CompanyId")
                        .HasColumnType("smallint");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompanies");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<short>("RoleId")
                        .HasColumnType("smallint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Withdraw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AddedUserId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<short>("BankId")
                        .HasColumnType("smallint");

                    b.Property<string>("CallbackUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EditedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("ExternalTransactionId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("MethodType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<short>("WithdrawStatusId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("WithdrawStatusId");

                    b.ToTable("Withdraws");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.DepositStatus", b =>
                {
                    b.HasBaseType("PaymentApplyProject.Domain.Entities.Status");

                    b.HasDiscriminator().HasValue("DepositStatus");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.WithdrawStatus", b =>
                {
                    b.HasBaseType("PaymentApplyProject.Domain.Entities.Status");

                    b.HasDiscriminator().HasValue("WithdrawStatus");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.BankAccount", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Bank", "Bank")
                        .WithMany("BankaAccounts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Customer", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Company", "Company")
                        .WithMany("Customers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Deposit", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.DepositRequest", "DepositRequest")
                        .WithOne("Deposit")
                        .HasForeignKey("PaymentApplyProject.Domain.Entities.Deposit", "DepositRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.DepositStatus", "DepositStatus")
                        .WithMany("Deposits")
                        .HasForeignKey("DepositStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("Customer");

                    b.Navigation("DepositRequest");

                    b.Navigation("DepositStatus");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.DepositRequest", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.UserCompany", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.User", "User")
                        .WithMany("UserCompanies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Withdraw", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.WithdrawStatus", "WithdrawStatus")
                        .WithMany("Withdraws")
                        .HasForeignKey("WithdrawStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Customer");

                    b.Navigation("WithdrawStatus");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Bank", b =>
                {
                    b.Navigation("BankaAccounts");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Company", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.DepositRequest", b =>
                {
                    b.Navigation("Deposit")
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.User", b =>
                {
                    b.Navigation("UserCompanies");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.DepositStatus", b =>
                {
                    b.Navigation("Deposits");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.WithdrawStatus", b =>
                {
                    b.Navigation("Withdraws");
                });
#pragma warning restore 612, 618
        }
    }
}
