using ContactBook.API.Data;
using ContactBook.API.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ContactBook.API.Repository
{
    public class SQLAppUserRepository : IAppUserRepository
    {
        private readonly ContactBookDbContext dbContext;

        public SQLAppUserRepository(ContactBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AppUser> CreateAsync(AppUser appUser)
        {
            await dbContext.AppUsers.AddAsync(appUser);
            await dbContext.SaveChangesAsync();
            return appUser;
        }

        public async Task<AppUser?> DeleteAsync(string id)
        {
            var existingAppuser = await dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAppuser == null)
            {
                return null;
            }
            dbContext.AppUsers.Remove(existingAppuser);
            await dbContext.SaveChangesAsync();
            return existingAppuser;
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
           return await dbContext.AppUsers.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
           return await dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser?> UpdateAsync(string id, AppUser appUser)
        {
            var existingAppuser = dbContext.AppUsers.FirstOrDefault(x=> x.Id == id);    
            if (existingAppuser == null) 
            {
                return null;
            }
            existingAppuser.FacebookUrl = appUser.FacebookUrl;
            existingAppuser.ImageUrl = appUser.ImageUrl;
            existingAppuser.UserName = appUser.UserName;
            existingAppuser.LastName = appUser.LastName;
            existingAppuser.City = appUser.City;
            existingAppuser.Country = appUser.Country;
            existingAppuser.Email = appUser.Email;
            existingAppuser.FirstName = appUser.FirstName;
            existingAppuser.State = appUser.State;
            existingAppuser.TwitterUrl = appUser.TwitterUrl;
            existingAppuser.PasswordHash = appUser.PasswordHash;

            await dbContext.SaveChangesAsync();
            return existingAppuser;
        }
    }
}
