using Application.DTOs.Commom;
using Application.DTOs.WorkManagementDTOs.TasksDTOs;
using Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs;
using Application.Interfaces.WorkManagement;
using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using Domain.Interfaces.Profile;
using Domain.Interfaces.WorkManagement;
using Domain.Tools;
using Persistence.Repositories.Profile;
using Persistence.Repositories.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorkManagement
{
    public class TimeTrackersService(ITimeTrackersRepository timeTrackersRepository, ITasksRepository taskRepository, ICollaboratorRepository collaboratorRepository) : ITimeTrackersService
    {
        private readonly ITimeTrackersRepository _timeTrackersRepository = timeTrackersRepository;
        private readonly ITasksRepository _taskRepository = taskRepository;
        private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository;




        public async Task<ResultModel<dynamic>> Add(TimeTrackersCreateDTO obj, Guid projectId)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            if (await VerifyToAddTimeTracker(obj, projectId) == false)
            {
                Tasks taskRemoved = await _taskRepository.Get(x => x.Id == obj.TaskId);
                await _taskRepository.Delete(taskRemoved);
                return new(HttpStatusCode.Conflict, "Não é possivel criar a tarefa nesse horário");
            }

            TimeTrackers timeTracker = new(startDate: Tool.ConvertTimeZones(obj.StartTime), endDate: Tool.ConvertTimeZones(obj.EndTime), timeZoneId: localZone.Id.ToString(), taskId: obj.TaskId, collaboratorId: obj.CollaboratorId);

            await _timeTrackersRepository.Create(timeTracker);

            return new();


        }

        public async Task<ResultModel<dynamic>> Update(TimeTrackersCreateDTO obj, Guid projectId)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            TimeTrackers timeTracker = await _timeTrackersRepository.Get(x => x.TaskId == obj.TaskId);
            if (timeTracker == null)
                return new(HttpStatusCode.NotFound, "TimeTracker not found");

            if (await VerifyToAddTimeTracker(obj, projectId) == false)
                return new(HttpStatusCode.Conflict, "Não é possivel atualizar a tarefa nesse horário");

            timeTracker.StartDate = Tool.ConvertTimeZones(obj.StartTime);
            timeTracker.EndDate = Tool.ConvertTimeZones(obj.EndTime);
            timeTracker.TimeZoneId = localZone.Id.ToString();
            timeTracker.TaskId = obj.TaskId;
            timeTracker.CollaboratorId = obj.CollaboratorId;
            await _timeTrackersRepository.Update(timeTracker);


            return new();


        }

        public async Task<TimeTrackers> Get(Guid taskId)
        {
            TimeTrackers timeTracker = await _timeTrackersRepository.Get(x => x.TaskId == taskId);

            if (timeTracker == null)
                throw new Exception("404: Time Tracker não encontrado");

            return timeTracker;
        }


        public async Task<ResultModel<IList<TimeTrackersDTO>>> GetItens(Guid projectId)
        {
            IList<Tasks> tasks = await _taskRepository.GetItens(x => x.ProjectId == projectId);

            if (tasks == null)
                return new(HttpStatusCode.NotFound, "Tarefas não encontradas");

            IList<TimeTrackersDTO> timeTrackersDTOs = new List<TimeTrackersDTO>();

            foreach (Tasks task in tasks)
            {
                IList<TimeTrackers> existingTimeTrackers = await _timeTrackersRepository.GetItens(x => x.TaskId == task.Id);

                foreach (TimeTrackers timeTracker in existingTimeTrackers)
                {
                    Collaborator collaborator = await _collaboratorRepository.Get(x => x.Id == timeTracker.CollaboratorId);
                    TimeTrackersDTO timeTrackersDTO = new(task, collaborator, timeTracker);

                    timeTrackersDTOs.Add(timeTrackersDTO);
                }

            }

            return new(timeTrackersDTOs.OrderBy(x => x.StartTime).ToList());
        }

        public async Task<ResultModel<dynamic>> Remove(Guid id)
        {
            TimeTrackers timeTracker = await _timeTrackersRepository.Get(x => x.Id == id);

            if (timeTracker == null)
                return new(HttpStatusCode.NotFound, "Time Tracker não encontrado");

            await _timeTrackersRepository.Remove(timeTracker);

            return new();
        }

        public async Task<bool> VerifyToAddTimeTracker(TimeTrackersCreateDTO obj, Guid projectId)
        {
            IList<Tasks> tasks = await _taskRepository.GetItens(x => x.ProjectId == projectId && x.DeletedAt == null);
            var filtredTasks = tasks.Where(task => task.Id != obj.TaskId).ToList();
            foreach (Tasks task in filtredTasks)
            {
                var existingTimeTrackers = await _timeTrackersRepository.GetItens(x => x.TaskId == task.Id);

                foreach (var existingTimeTracker in existingTimeTrackers)
                {
                    if (existingTimeTracker.StartDate < Tool.ConvertTimeZones(obj.EndTime) && existingTimeTracker.EndDate > Tool.ConvertTimeZones(obj.StartTime))
                    {
                        return false;
                    }
                }

                var dayStart = obj.StartTime.Date;
                var dayEnd = dayStart.AddDays(1).AddTicks(-1);

                var timeTrackersForTheDay = existingTimeTrackers
                    .Where(x => x.StartDate >= dayStart && x.EndDate <= dayEnd)
                    .ToList();

                long totalTicksForTheDay = timeTrackersForTheDay
                .Sum(x => (x.EndDate - x.StartDate).Ticks);

                totalTicksForTheDay += (obj.EndTime - obj.StartTime).Ticks;

                TimeSpan totalHoursForTheDay = TimeSpan.FromTicks(totalTicksForTheDay);

                if (totalHoursForTheDay.TotalHours > 24)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
