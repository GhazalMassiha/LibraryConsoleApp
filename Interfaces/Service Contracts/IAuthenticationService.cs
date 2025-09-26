
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;


namespace HW12_Issue1.Interfaces.Service_Contracts
{
    public interface IAuthenticationService
    {
        public (User? user, Admin? admin) Login(string username, string password);
        public void RegisterUser(string username, string password);
        public void RegisterAdmin(string username, string password);
    }
}
