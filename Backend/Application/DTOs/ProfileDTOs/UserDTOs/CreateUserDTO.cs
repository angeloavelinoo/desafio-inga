using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profile.UserDTOs
{
    public class CreateUserDTO : BaseDTOValidation
    {
        public CreateUserDTO(string username, string password)
        {
            AddNotifications(new ContractUsername(username),
                new ContractPassword(password));

            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
