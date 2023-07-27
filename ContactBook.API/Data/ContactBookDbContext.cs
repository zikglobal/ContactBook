using ContactBook.API.Model.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.API.Data
{
    public class ContactBookDbContext:IdentityDbContext<AppUser>
    {
        public ContactBookDbContext( DbContextOptions<ContactBookDbContext>options):base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<AppUser>AppUsers { get; set; }
    }
}
