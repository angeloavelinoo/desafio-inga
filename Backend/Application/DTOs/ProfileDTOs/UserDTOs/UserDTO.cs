using Domain.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profile.UserDTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            
        }
        public UserDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
