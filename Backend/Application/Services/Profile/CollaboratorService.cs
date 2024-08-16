using Application.DTOs.Commom;
using Application.DTOs.Profile;
using Application.DTOs.Profile.UserDTOs;
using Application.Interfaces.Profile;
using Domain.Entities.Profile;
using Domain.Interfaces.Profile;
using Persistence.Repositories.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Profile
{
    public class CollaboratorService(ICollaboratorRepository collaboratorRepository) : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository;
        public async Task<ResultModel<dynamic>> Add(User obj)
        {
            if(obj == null)
                return new(HttpStatusCode.BadRequest, "User is null");

            Collaborator collaborator =  new (userId: obj.Id, name: obj.Username);

            await _collaboratorRepository.Create(collaborator);

            return new();
        }

        public async Task<Collaborator> Get(Guid id)
        {
            Collaborator collaborator = await _collaboratorRepository.Get(x => x.Id == id);


            if (collaborator == null)
                throw new Exception("404: Collaborator not found");


            return collaborator;
        }

        public async Task<ResultModel<IList<CollaboratorDTO>>> GetItens()
        {
            IList<Collaborator> collaborators = await _collaboratorRepository.GetItens();

            if (!collaborators.Any()) return new(HttpStatusCode.NotFound, "No collaborator found");

            List<CollaboratorDTO> collaboratorsDTO = new List<CollaboratorDTO>();

            foreach (Collaborator collaborator in collaborators)
            {
                CollaboratorDTO collaboratorDTO = new(collaborator);

                collaboratorsDTO.Add(collaboratorDTO);

            }

            return new(collaboratorsDTO);

        }

        public async Task<ResultModel<dynamic>> Remove(User user)
        {
            Collaborator collaborator = await _collaboratorRepository.Get(x => x.UserId == user.Id);

            if (collaborator == null) return new(HttpStatusCode.NotFound, "Collaborator not found");

            collaborator.DeletedAt = user.DeletedAt;
            await _collaboratorRepository.Remove(collaborator);

            return new();
        }

        public async Task<ResultModel<dynamic>> Update(User user)
        {
            Collaborator collaborator = await _collaboratorRepository.Get(x => x.UserId == user.Id);

            if (user == null) return new(HttpStatusCode.NotFound, "Collaborator not found");

            collaborator.Name = user.Username;
            collaborator.UpdatedAt = user.UpdatedAt;
            await _collaboratorRepository.Update(collaborator);

            return new();
        }
    }  
    
}
