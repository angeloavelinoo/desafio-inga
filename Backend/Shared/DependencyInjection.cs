using Application.Interfaces.Profile;
using Application.Interfaces.WorkManagement;
using Application.Services.Profile;
using Application.Services.WorkManagement;
using Domain.Interfaces.Profile;
using Domain.Interfaces.WorkManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataContext;
using Persistence.Repositories.Profile;
using Persistence.Repositories.WorkManagement;


namespace Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
               IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext)
            .Assembly.FullName)));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<ICollaboratorService, CollaboratorService>();

            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsService, ProjectsService>();

            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksService, TasksService>();

            services.AddScoped<ITimeTrackersRepository, TimeTrackersRepository>();
            services.AddScoped<ITimeTrackersService, TimeTrackersService>();


            return services;
        }
    }
}
