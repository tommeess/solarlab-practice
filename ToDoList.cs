using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ToDoList
    {
        private List<Task> tasks;
        private int nextId;
        public ToDoList()
        {
            tasks = new List<Task>();
            nextId = 1;
        }

        public void AddTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Задача не может быть без названия");
            }
            else
            {
                tasks.Add(new Task(title) { Id = nextId++ });
            }

        }

        public void RemoveTask(int taskId)
        {
            var task = tasks.Find(i => i.Id == taskId);
            if (task != null)
            {
                tasks.Remove(task);
            }
            else
            {
                throw new InvalidOperationException($"Задача с ID {taskId} не найдена");
            }
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        public List<Task> GetCompletedTasks()
        {
            return tasks.Where(t => t.IsDone).ToList();
        }

        public List<Task> GetIncompleteTasks()
        {
            return tasks.Where(t => !t.IsDone).ToList();
        }

        public void MarkTaskAsCompleted(int taskId)
        {
            var task = tasks.Find(i => i.Id == taskId);
            if (task != null)
            {
                task.IsDone = true;
            }
            else
            {
                throw new InvalidOperationException($"Задача с ID {taskId} не найдена");
            }
        }

        public void SaveToFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("Файл не существует");
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Id}. {task.Title} {(task.IsDone ? " (X)" : "")}");
                }
            }
        }


        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("Файл не существует");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('.');
                    if (parts.Length == 3)
                    {
                        int id = int.Parse(parts[0].Trim());
                        string title = parts[1].Trim();
                        bool isDone = bool.Parse(parts[2].Trim());
                        Task task = new Task(title)
                        {
                            Id = id,
                            IsDone = isDone
                        };
                            tasks.Add(task);    
                    }
                }
            }
        }
    }
}

