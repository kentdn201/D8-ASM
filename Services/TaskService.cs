using Task = D8.Models.Task;

namespace D8.Services
{
    public class TaskService : ITaskService
    {
        private static readonly List<Task> _dataSource = new List<Task>();
        public List<Task> GetAll()
        {
            return _dataSource;
        }

        public Task? GetOne(Guid id)
        {
            return _dataSource.FirstOrDefault(o => o.Id == id);
        }
        public Task Add(Task task)
        {
            _dataSource.Add(task);
            return task;
        }

        public Task? Edit(Task task)
        {
            var current = _dataSource.FirstOrDefault(o => o.Id == task.Id);
            if (current == null) return null;

            current.Title = task.Title;
            current.Description = task.Description;
            current.Completed = task.Completed;

            return current;
        }


        public void Remove(Guid id)
        {
            var current = _dataSource.FirstOrDefault(o => o.Id == id);
            if (current != null)
            {
                _dataSource.Remove(current);
            }

        }

        public List<Task> Add(List<Task> tasks)
        {
            _dataSource.AddRange(tasks);
            return tasks;
        }

        public void Remove(List<Guid> ids)
        {
            _dataSource.RemoveAll(o => ids.Contains(o.Id));
        }

        public bool Exists(Guid id)
        {
            return _dataSource.Any(o => o.Id == id);
        }
    }
}