using Application.DTOs.Commom;
using Application.DTOs.WorkManagementDTOs.ProjectsDTOs;
using Application.DTOs.WorkManagementDTOs.TasksDTOs;
using Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs;
using Application.Interfaces.Profile;
using Application.Interfaces.WorkManagement;
using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using Domain.Interfaces.WorkManagement;
using Domain.Tools;
using Persistence.Repositories.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorkManagement
{
    public class TasksService(ITasksRepository tasksRepository, ITimeTrackersService timeTrackersService, ICollaboratorService collaboratorService) : ITasksService
    {
        private readonly ITasksRepository _tasksRepository = tasksRepository;
        private readonly ITimeTrackersService timeTrackersService = timeTrackersService;
        private readonly ICollaboratorService _collaboratorService = collaboratorService;
        public async Task<ResultModel<dynamic>> Add(TaskCreateDTO obj)
        {
            if(obj.StartTime > obj.EndTime)
                return new(HttpStatusCode.Conflict, "Data início não pode ser menor que data fim");

            TimeZoneInfo localZone = TimeZoneInfo.Local;
            Tasks task = new(projectId: obj.ProjectId, name: obj.Name, description: obj.Description);

            if (await _tasksRepository.AlredyExist(x => x.Name == obj.Name && x.DeletedAt == null))
                return new(HttpStatusCode.Conflict, "Já existe um usuário com esse nome");

            await _tasksRepository.Create(task);
            TimeTrackersCreateDTO timeTrackersCreateDTO = new TimeTrackersCreateDTO(taskId: task.Id, collaboratorId: obj.CollaboratorId, startTime: obj.StartTime, endTime: obj.EndTime);

            var timeTrackerResult = await timeTrackersService.Add(timeTrackersCreateDTO, task.ProjectId);

            if (timeTrackerResult.Status != 200)
                return timeTrackerResult;

            return new();
        }

        public async Task<ResultModel<dynamic>> Get(string name)
        {
            Tasks task = await _tasksRepository.Get(x => x.Name == name);

            if (task == null)
                return new(HttpStatusCode.NotFound, "Tarefa não encontrada");

            TimeTrackers timeTracker = await timeTrackersService.Get(task.Id);

            Collaborator collaborator = null;
            if (timeTracker.CollaboratorId.HasValue)
            {
                collaborator = await _collaboratorService.Get(timeTracker.CollaboratorId.Value);
            }

            TaskDTO taskDTO = new(task: task, collaborator: collaborator, timeTracker: timeTracker);

            return new(taskDTO);
        }

        public async Task<ResultModel<IList<TaskDTO>>> GetItens(Guid projectId)
        {
            IList<Tasks> tasks = await _tasksRepository.GetItensByProject(projectId);

            if (tasks == null || !tasks.Any())
                return new ResultModel<IList<TaskDTO>>(HttpStatusCode.NotFound, "Tarefas não encontradas");

            List<TaskDTO> tasksDTO = new List<TaskDTO>();

            foreach (Tasks task in tasks)
            {
                TimeTrackers timeTracker = await timeTrackersService.Get(task.Id);

                Collaborator collaborator = null;
                if (timeTracker?.CollaboratorId.HasValue == true)
                {
                    collaborator = await _collaboratorService.Get(timeTracker.CollaboratorId.Value);
                }

                TaskDTO taskDTO = new TaskDTO(task: task, collaborator: collaborator, timeTracker: timeTracker);

                tasksDTO.Add(taskDTO);
            }

            return new(tasksDTO);
        }

        public async Task<ResultModel<dynamic>> Remove(Guid id)
        {
            Tasks task = await _tasksRepository.Get(x => x.Id == id);

            if (task == null)
                return new(HttpStatusCode.NotFound, "Tarefa não encontrada");

            task.DeletedAt = Tool.ConvertTimeZones(DateTime.UtcNow);

            await _tasksRepository.Remove(task);

            TimeTrackers timeTracker = await timeTrackersService.Get(id);

            await timeTrackersService.Remove(timeTracker.Id);


            return new();
        }

        public async Task<ResultModel<dynamic>> RemoveAll(Guid projectId)
        {
            IList<Tasks> tasks = await _tasksRepository.GetItens(x => x.ProjectId == projectId && x.DeletedAt == null);

            if (tasks == null)
                return new(HttpStatusCode.NotFound, "Nenhuma tarefa não encontrada");

            foreach (Tasks task in tasks)
            {
                await _tasksRepository.Remove(task);
            }

            return new();

        }

        public async Task<ResultModel<dynamic>> Update(TaskUpdateDTO taskDTO, Guid id)
        {
            if (taskDTO.StartTime > taskDTO.EndTime)
                return new(HttpStatusCode.Conflict, "Data início não pode ser menor que data fim");
            Tasks task = await _tasksRepository.Get(x => x.Id == id);

            if (task == null)
                return new(HttpStatusCode.NotFound, "Tarefa não encontrada");

            task.Name = taskDTO.Name;
            task.Description = taskDTO.Description;
            task.UpdatedAt = Tool.ConvertTimeZones(DateTime.UtcNow);

            await _tasksRepository.Update(task);

            TimeTrackersCreateDTO timeTrackerDTO = new(taskId: taskDTO.Id, collaboratorId: taskDTO.CollaboratorId, startTime: taskDTO.StartTime, endTime: taskDTO.EndTime);

            var timeTrackerResult = await timeTrackersService.Update(timeTrackerDTO, task.ProjectId);

            if (timeTrackerResult.Status != 200)
                return timeTrackerResult;

            return new();
        }
    }
}
