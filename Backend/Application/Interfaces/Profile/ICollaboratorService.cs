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
    public interface ICollaboratorService
    {
        Task<ResultModel<IList<CollaboratorDTO>>> GetItens();
        Task<Collaborator> Get(Guid id);
        Task<ResultModel<dynamic>> Add(User obj);
        Task<ResultModel<dynamic>> Remove(User name);
        Task<ResultModel<dynamic>> Update(User userDTO);
    }
}
