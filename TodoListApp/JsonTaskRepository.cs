using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace TodoListApp
{
    public class JsonTaskRepository : ITaskRepository
    {
        private string _filePath = "tasks.json";

        public List<TodoModel> LoadTasks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<TodoModel>();
            }

            string json_string = File.ReadAllText(_filePath);

            List<TodoModel> deserialized_tasks = JsonSerializer.Deserialize<List<TodoModel>>(json_string);

            if (deserialized_tasks == null)
            {
                return new List<TodoModel>();
            }
            else
            {
                return deserialized_tasks;
            }
        }

        public void SaveTasks(List<TodoModel> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json_string = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(_filePath, json_string);
        }   
    }
}
