using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class TaskService
    {
        private readonly TaskRepository _taskRepository;

        public TaskService(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AddTaskAsync(ProjectTask task)
        {
            await _taskRepository.AddTaskAsync(task);
        }

        public async Task<ProjectTaskDTO> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<List<ProjectTaskDTO>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task UpdateTaskAsync(ProjectTask task)
        {
            await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
