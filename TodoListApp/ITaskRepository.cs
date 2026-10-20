using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListApp
{
    public interface ITaskRepository
    {
        void SaveTasks(List<TodoModel> tasks);
        List<TodoModel> LoadTasks();
    }
}
