﻿using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Interfaces
{
    public interface IPaymentContext : IDbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<WithdrawStatus> WithdrawStatuses { get; set; }
        public DbSet<DepositStatus> DepositStatuses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<DepositRequest> DepositRequests { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
