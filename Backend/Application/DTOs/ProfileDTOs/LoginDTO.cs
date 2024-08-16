using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profile
{
    public class LoginDTO : BaseDTOValidation
    {
        public LoginDTO(string username, string password)
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
