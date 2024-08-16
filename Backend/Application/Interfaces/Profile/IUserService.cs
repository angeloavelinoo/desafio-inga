using Application.DTOs.Commom;
using Application.DTOs.Profile;
using Application.DTOs.Profile.UserDTOs;
using Domain.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Profile
{
    public interface IUserService
    {
        Task<ResultModel<IList<UserDTO>>> GetItens();
        Task<ResultModel<dynamic>> Get(string username);
        Task<ResultModel<dynamic>> Add(CreateUserDTO obj);
        Task<ResultModel<dynamic>> Remove(string username);
        Task<ResultModel<dynamic>> Update(UserDTO userDTO, string username);
        Task<ResultModel<dynamic>> Login(LoginDTO usuarioLoginDTO);
    }
}
