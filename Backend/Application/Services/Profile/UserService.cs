using Application.DTOs.Commom;
using Application.DTOs.Profile;
using Application.DTOs.Profile.UserDTOs;
using Application.Interfaces.Profile;
using Domain.Entities.Profile;
using Domain.Interfaces.Profile;
using Domain.Tools;
using System.Net;


namespace Application.Services.Profile
{
    public class UserService(IUserRepository userRepository, ICollaboratorService collaboratorService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICollaboratorService _collaboratorService = collaboratorService;

        public async Task<ResultModel<dynamic>> Add(CreateUserDTO obj)
        {


            User user = new(username: obj.Username, password: BCrypt.Net.BCrypt.HashPassword(obj.Password));

            if (obj?.IsValid != true)
                return new(HttpStatusCode.BadRequest, obj?.Notifications.First().Message ?? "Erro");

            if (await _userRepository.AlredyExist(x => x.Username == obj.Username))
                return new(HttpStatusCode.Conflict, "There is already a user with this username");

            await _userRepository.Create(user);
            await _collaboratorService.Add(user);

            return new();

        }

        public async Task<ResultModel<dynamic>> Get(string username)
        {
            User user = await _userRepository.Get(x => x.Username == username);


            if (user == null)
                return new(HttpStatusCode.NotFound, "User not found");

            UserDTO usuarioDTO = new(user);

            return new(usuarioDTO);
        }

        public async Task<ResultModel<IList<UserDTO>>> GetItens()
        {
            IList<User> users = await _userRepository.GetItens();

            if (!users.Any()) return new(HttpStatusCode.NotFound, "No users found");

            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (User user in users)
            {
                UserDTO usuarioDTO = new(user);

                usersDTO.Add(usuarioDTO);

            }

            return new(usersDTO);

        }

        public async Task<ResultModel<dynamic>> Login(LoginDTO usuarioLoginDTO)
        {
            User usuario = await _userRepository.Get(x => x.Username == usuarioLoginDTO.Username);

            if (usuario?.Password != null && BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.Password, usuario.Password))
                return new ResultModel<dynamic>(new
                {
                    token = TokenService.GenerateToken(usuario)
                });

            return new(HttpStatusCode.BadRequest, "Invalid name or password");
        }

        public async Task<ResultModel<dynamic>> Remove(string username)
        {
            User user = await _userRepository.Get(x => x.Username == username);

            if (user == null) return new(HttpStatusCode.NotFound, "User not found");

            user.DeletedAt = Tool.ConvertTimeZones(DateTime.UtcNow);

            await _userRepository.Remove(user);
            await _collaboratorService.Remove(user);

            return new();
        }

        public async Task<ResultModel<dynamic>> Update(UserDTO userDTO, string username)
        {
            User user = await _userRepository.Get(x => x.Username == username);

            if (user == null) return new(HttpStatusCode.NotFound, "User not found");

            user.Username = userDTO.Username;
            user.UpdatedAt = Tool.ConvertTimeZones(DateTime.UtcNow);

            await _collaboratorService.Update(user);
            await _userRepository.Update(user);


            return new();
        }
    }
}
