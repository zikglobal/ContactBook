using ContactBook.API.Model.Domain;
using System.Runtime.InteropServices;

namespace ContactBook.API.Repository
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAllAsync(); 
        Task <AppUser?>GetByIdAsync(string id);
         Task<AppUser> CreateAsync(AppUser appUser);
        Task<AppUser?> UpdateAsync(string id, AppUser appUser); 

        Task <AppUser?> DeleteAsync(string id);

    }
}
