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
            tasks.Add(new Task(title) { Id = nextId++ });
        }

        public void RemoveTask(int taskId)
        {
            var task = tasks.Find(i => i.Id == taskId);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Задача удалена!");
            }
            else
            {
                Console.WriteLine($"Задача с ID {taskId} не найдена");
            }
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
                Console.WriteLine("Неверно указан номер задачи");
            }
        }

        public void ViewTasks()
        {
            if (tasks == null)
            {
                Console.WriteLine("Тут ничего нет :(");
                return;
            }
            else
            {
                Console.WriteLine("Список задач:");
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title} {(task.IsDone ? " (Х)" : "")}");
                }
            }
        }

        public void ViewCompletedTasks() {
            var completedTasks = tasks.FindAll(i => i.IsDone);
            if (completedTasks.Count == 0)
            {
                Console.WriteLine("Нет выполненных задач");
                return;
            }
            else
            {
                Console.WriteLine("Выполненные задачи:");
                foreach (var task in completedTasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title}");
                }
            }
        }

        public void ViewUncompletedTasks()
        {
            var completedTasks = tasks.FindAll(i => !i.IsDone);
            if (completedTasks.Count == 0)
            {
                Console.WriteLine("Нет невыполненных задач");
                return;
            }
            else
            {
                Console.WriteLine("Невыполненные задачи:");
                foreach (var task in completedTasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title}");
                }
            }
        }

        public void SaveToFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach(var task in tasks)
                    {
                        writer.WriteLine($"{task.Id}. {task.Title} {(task.IsDone ? " (X)" : "")}");
                    }
                }
                Console.WriteLine("Список задач сохранен в файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении списка задач в файл: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                using (StreamReader reader=new StreamReader(filePath))
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
                            tasks.Add(new Task(title) { Id = id, IsDone = isDone });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке списка задач: {ex.Message} ");
            }
        }
    }
}
