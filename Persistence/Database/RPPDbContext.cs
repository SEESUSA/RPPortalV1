using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public partial class RPPDbContext : DbContext, IRppDbContext
    {
        public RPPDbContext(DbContextOptions<RPPDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }  
}