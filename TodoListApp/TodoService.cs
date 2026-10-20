using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListApp
{
    public class TodoService
    {
        private List<TodoModel> _tasks;
        private ITaskRepository _repository;

        public TodoService(ITaskRepository repository)
        {
            _repository = repository;
            _tasks = _repository.LoadTasks();
        }

        public List<TodoModel> GetAllTasks()
        {
            return _tasks;
        }

        public void add_task(TodoModel task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "!!! задача не может быть пустой !!!");
            }
            
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new ArgumentException("!!! название задачи обязательно для заполнения !!!");
            }

            _tasks.Add(task);
            _repository.SaveTasks(_tasks);
        }

        public void delete_task(TodoModel task)
        {
            if (task != null && _tasks.Contains(task))
            {
                _tasks.Remove(task);
                _repository.SaveTasks(_tasks);
            }
        }

        public void upd_task()
        {
            _repository.SaveTasks(_tasks);
        }
    }
}
