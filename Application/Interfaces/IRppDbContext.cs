using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IRppDbContext
    {
        DbSet<User> User { get; set; }
        Task<int> SaveChangesAsync();
    }
}
