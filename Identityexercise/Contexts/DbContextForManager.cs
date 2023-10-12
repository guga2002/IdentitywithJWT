using Identityexercise.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identityexercise.Contexts
{
    public class DbContextForManager:IdentityDbContext<User,IdentityRole,string>
    {

        public DbContextForManager(DbContextOptions<DbContextForManager> ops):base(ops)
        {
                
        }
        public DbSet<User> _Users { get; set; }
    }
}
