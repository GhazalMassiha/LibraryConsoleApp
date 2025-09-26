
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Enums;
using HW12_Issue1.Infrastructures.Repositories;
using HW12_Issue1.Interfaces.Service_Contracts;
using System.Net;

namespace HW12_Issue1.Application_Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserRepository _userRepo = new UserRepository();
        private AdminRepository _adminRepo = new AdminRepository();

        public (User? user, Admin? admin) Login(string username, string password)
        {
            Admin? admin = _adminRepo.GetByUsername(username);
            if (admin != null)
            {
                if (admin.Password != password)
                {
                    throw new InvalidPasswordException("\nInvalid Password.");
                }
                return (null, admin);
            }

            User? user = _userRepo.GetByUsername(username);
            if (user != null)
            {
                if (user.Password != password)
                {
                    throw new InvalidPasswordException("\nInvalid Password.");
                }
                return (user, null);
            }

            throw new UsernameNotFoundException("\nUsername Not Found.");
        }

        public void RegisterUser(string username, string password)
        {
            IsUsernameAvailable(username);

            var user = new User
            {
                Username = username,
                Password = password,
                Role = RoleEnum.User
            };
            _userRepo.Add(user);
        }

        public void RegisterAdmin(string username, string password)
        {
            IsUsernameAvailable(username);

            var admin = new Admin
            {
                Username = username,
                Password = password,
                Role = RoleEnum.Admin
            };
            _adminRepo.Add(admin);
        }

        // --- Helper Methods ---
        private void IsUsernameAvailable (string username)
        {
            if (_userRepo.GetByUsername(username) != null)
            {
                throw new UsernameAlreadyExistsException("\nUsername Already Exists.");
            }
            if (_adminRepo.GetByUsername(username) != null)
            {
                throw new UsernameAlreadyExistsException("\nUsername Already Exists.");
            }
        }
    }
}
