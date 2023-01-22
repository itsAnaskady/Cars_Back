using Back1.Models;
using Back1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back1.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserGetViewModel> LoginUserAsync(LoginViewModel model);
       

    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager , IConfiguration configuration) 
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //************************Configuration du formulaire d'enregistrement

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new UserManagerResponse { Message = "Email is already registered !", IsSuccess=false };
            

            if (model.Password != model.ConfirmPassword) 
                return new UserManagerResponse { Message = "Confirm password doesn't match", IsSuccess = false };

            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Telephone,
                Adress = model.Adress,
                Name = model.Name,
                FirstName= model.FirstName,
                ListeFavori = false,
                ListeNoir= false
            };

            var result = await _userManager.CreateAsync(user,model.Password);
            var result1 = await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(model.Email), model.Role);
            
            if (result.Succeeded && result1.Succeeded)
            {
                return new UserManagerResponse
                  {
                    Message =  model.Role +" "+ model.Name + " is created.",
                    IsSuccess= true,
                    role = model.Role
                  };  
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    
        //****************************Configuration de Login formulaire 
        public async Task<UserGetViewModel> LoginUserAsync(LoginViewModel model)
        { 
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            //Check Email
            if (user == null)
                return null;

            //Chek Password
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if(!result)
                return null;
            
            //Generate token
            User u = (User)user;
           

            var userRole = await _userManager.GetRolesAsync(user);
            //return token
            UserGetViewModel modelShow = new UserGetViewModel()
            {
                Name = u.Name,
                FirstName = u.FirstName,
                PhoneNumber = u.PhoneNumber,
                IdUser = u.Id,
                Role = userRole.First(),
                Adress = u.Adress,
                Email = u.Email
            };
            return modelShow;

        }

        
    }
}
