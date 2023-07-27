using AutoMapper;
using ContactBook.API.Data;
using ContactBook.API.Model.Domain;
using ContactBook.API.Model.DTO;
using ContactBook.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly ContactBookDbContext dbContext;
        private readonly IAppUserRepository appUserRepository;

        public AppUserController(ContactBookDbContext dbContext, IAppUserRepository appUserRepository,
          IMapper mapper  )
        {
            this.dbContext = dbContext;
            this.appUserRepository = appUserRepository;
        }

        // GET ALL CONTACT
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            // get data from database - domain model
            var appUsersDomain = await appUserRepository.GetAllAsync();

            // map domain model to Dto
            var appUsersDto = new List<AppUserDto>();
            foreach (var appUserDomain in appUsersDomain)
            {
                appUsersDto.Add(new AppUserDto()
                {
                    FirstName = appUserDomain.FirstName,
                    LastName = appUserDomain.LastName,
                    Email = appUserDomain.Email,
                    Username = appUserDomain.UserName,
                    FacebookUrl = appUserDomain.FacebookUrl,
                    TwitterUrl = appUserDomain.TwitterUrl,
                    ImageUrl = appUserDomain.ImageUrl,
                    City = appUserDomain.City,
                    Country = appUserDomain.Country,
                    State = appUserDomain.State,
                    Password = appUserDomain.PasswordHash
                });

            }

            //return Dto
            return Ok(appUsersDto);

        }

        // GET CONTACT BY ID

        [HttpGet]
        [Route("{id}")]
        public async Task  <IActionResult> GetById([FromRoute]string id)
        {
            //get appuser domain model from database 
           // var appUserDomain = dbContext.AppUsers.Find(id);

            var appUserDomain = await appUserRepository.GetByIdAsync(id);
            if (appUserDomain == null)
            {
                return NotFound();
            }
            //map/convert appusermodel to appuser Dto

            var appUserDto = new AppUserDto()
            {
                FirstName = appUserDomain.FirstName,
                LastName = appUserDomain.LastName,
                Email = appUserDomain.Email,
                Username = appUserDomain.UserName,
                FacebookUrl = appUserDomain.FacebookUrl,
                TwitterUrl = appUserDomain.TwitterUrl,
                ImageUrl = appUserDomain.ImageUrl,
                City = appUserDomain.City,
                Country = appUserDomain.Country,
                State = appUserDomain.State,
                Password = appUserDomain.PasswordHash
                
                
            }; 
            return Ok(appUserDomain);

        }

        //POST to create new AppUser
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] AddAppUserDto addAppUserDto)
        {
            // map Dto to Domain Model
            var appUserDomainModel = new AppUser()
            {
                FirstName = addAppUserDto.FirstName,
                LastName = addAppUserDto.LastName,
                Email = addAppUserDto.Email,
                ImageUrl = addAppUserDto.ImageUrl,
                City = addAppUserDto.City,
                Country = addAppUserDto.Country,
                State = addAppUserDto.State,
                UserName = addAppUserDto.Username,
                TwitterUrl = addAppUserDto.TwitterUrl,
                FacebookUrl = addAppUserDto.FacebookUrl,
                PasswordHash = addAppUserDto.Password,
            };
            //use Domain Model to Create AppUser
              appUserDomainModel =  await appUserRepository.CreateAsync(appUserDomainModel);

            //map domain model back to Dto

            var appUserDto = new AppUser()
            {
                FacebookUrl = addAppUserDto.FacebookUrl,
                ImageUrl = addAppUserDto.ImageUrl,
                City = addAppUserDto.City,
                Country = addAppUserDto.Country,
                State = addAppUserDto.State,
                UserName = addAppUserDto.Username,
                FirstName = addAppUserDto.FirstName,
                LastName = addAppUserDto.LastName,
                Email = addAppUserDto.Email,
                PasswordHash = addAppUserDto.Password,
                TwitterUrl = addAppUserDto.TwitterUrl,
                
            };

            return CreatedAtAction(nameof(GetById),new {id = appUserDomainModel.Id}, appUserDto);

        }

       // update Appuser
       [HttpPut]
        [Route("{id:}")]
        public async  Task <IActionResult> Update([FromRoute] string id, [FromBody] UpdateAppUserDto updateAppUserDto)
        {
            // map Dto to Domain Model
            var appUserDomainModel = new AppUser
            {
                FirstName = updateAppUserDto.FirstName,
                LastName = updateAppUserDto.LastName,
                Email = updateAppUserDto.Email,
                FacebookUrl = updateAppUserDto.FacebookUrl,
                ImageUrl = updateAppUserDto.ImageUrl,
                City = updateAppUserDto.City,
                Country = updateAppUserDto.Country,
                State = updateAppUserDto.State,
                TwitterUrl = updateAppUserDto.TwitterUrl,
                UserName = updateAppUserDto.Username,
                PasswordHash = updateAppUserDto.Password,
            };

            // check if contact exist
            appUserDomainModel = await appUserRepository.UpdateAsync(id, appUserDomainModel);

            if (appUserDomainModel == null)
            {
                return NotFound();
            }
            // convert Domain Model to Dto
            var appUserDto = new AppUserDto
            {
                Email = appUserDomainModel.Email,
                ImageUrl = appUserDomainModel.ImageUrl,
                FacebookUrl = appUserDomainModel.FacebookUrl,
                LastName = appUserDomainModel.LastName,
                FirstName = appUserDomainModel.FirstName,
                TwitterUrl = appUserDomainModel.TwitterUrl,
                City = appUserDomainModel.City,
                State = appUserDomainModel.State,
                Country = appUserDomainModel.Country,
                Username = appUserDomainModel.UserName,
                Password = appUserDomainModel.PasswordHash,
            };


            return Ok(appUserDto);
        }

        //Delete Appuser
        [HttpDelete("{id}")]
        public async Task  <IActionResult> Delete([FromRoute] string id)
        {
            var appUserDomainModel = await appUserRepository.DeleteAsync(id);

            if(appUserDomainModel == null)
            {
                return NotFound();
            }

            //return deleted contact back
            // map Domain Model to Dto
            var appUserDto = new AppUserDto
            {
                Email = appUserDomainModel.Email,
                ImageUrl = appUserDomainModel.ImageUrl,
                FacebookUrl = appUserDomainModel.FacebookUrl,
                LastName = appUserDomainModel.LastName,
                FirstName = appUserDomainModel.FirstName,
                TwitterUrl = appUserDomainModel.TwitterUrl,
                City = appUserDomainModel.City,
                State = appUserDomainModel.State,
                Country = appUserDomainModel.Country,
                Username = appUserDomainModel.UserName,
                Password = appUserDomainModel.PasswordHash,
            };

            return Ok(appUserDto);
        }
    }
}
