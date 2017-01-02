using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantCore
{
    public class SqliteApplicationDbContext : DbContext
    {
        private readonly IHostingEnvironment env;
        private readonly AppTenant tenant;

        public SqliteApplicationDbContext(IHostingEnvironment env, AppTenant tenant)
        {
            this.env = env;
            this.tenant = tenant;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantDbName = tenant.AppName.Replace(" ", "-").ToLowerInvariant();
            var connectionString = $"FileName={tenantDbName}.db";
            optionsBuilder.UseSqlite(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
