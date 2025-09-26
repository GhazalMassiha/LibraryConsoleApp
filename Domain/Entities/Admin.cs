

using HW12_Issue1.Enums;

namespace HW12_Issue1.Domain.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }

        public Admin()
        {
            Role = RoleEnum.Admin;
        }
    }
}
