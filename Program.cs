using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Поддержка кириллицы
            ToDoList todoList = new ToDoList();

            while (true)
            {
                Console.Clear();
                PrintMenu();
                Console.Write("Выберите пункт меню:  ");
                char choice = Console.ReadKey().KeyChar;
                switch (choice)
                {
                    case '1':
                        {
                            AddNewTask(todoList);
                            break;
                        }
                    case '2':
                        {
                            ViewTasks(todoList);
                            break;
                        }
                    case '3':
                        {
                            MarkTaskAsCompleted(todoList);
                            break;
                        }
                    case '4':
                        {
                            RemoveTask(todoList);
                            break;
                        }
                    case '5':
                        {
                            ViewCompletedTasks(todoList);
                            break;
                        }
                    case '6':
                        {
                            ViewIncompletedTasks(todoList);
                            break;
                        }
                    case '7':
                        {
                            SaveToFile(todoList);
                            break;
                        }
                    case '8':
                        {
                            LoadFromFile(todoList);
                            break;
                        }
                    case '9':
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("\nТакого пункта меню нет, попробуйте еще раз!");
                            break;
                        }

                }
                Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Просмотр задач");
            Console.WriteLine("3. Отметить задачу как выполненную");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Просмотр выполненных задач");
            Console.WriteLine("6. Просмотр невыполненных задач");
            Console.WriteLine("7. Сохранить в файл");
            Console.WriteLine("8. Загрузить из файла");
            Console.WriteLine("9. Выход");
            Console.WriteLine("-------------------------------------");
        }

        private static void AddNewTask(ToDoList todoList)
        {
            Console.WriteLine("\nВведите задачу: ");
            string title = Console.ReadLine();
            try
            {
                todoList.AddTask(title);
                Console.WriteLine("Задача успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении задачи: {ex.Message}");
            }
        }

        private static void RemoveTask(ToDoList todoList)
        {
            Console.Write("\nВведите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                try
                {
                    todoList.RemoveTask(taskId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении задачи: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nНеверный формат ID");
            }
        }

        public static void MarkTaskAsCompleted(ToDoList todoList)
        {
            Console.Write("\nВведите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                try
                {
                    todoList.MarkTaskAsCompleted(taskId);
                    Console.WriteLine("Готово!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nНеверный формат ID");
            }
        }

        public static void ViewTasks(ToDoList todoList)
        {
            List<Task> tasks = todoList.GetAllTasks();
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

        public static void ViewCompletedTasks(ToDoList todoList)
        {
            List<Task> completedTasks = todoList.GetCompletedTasks();
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

        public static void ViewIncompletedTasks(ToDoList todoList)
        {
            var incompletedTasks = todoList.GetIncompleteTasks();
            if (incompletedTasks.Count == 0)
            {
                Console.WriteLine("Нет невыполненных задач");
                return;
            }
            else
            {
                Console.WriteLine("Невыполненные задачи:");
                foreach (var task in incompletedTasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title}");
                }
            }
        }

        private static void SaveToFile(ToDoList todoList)
        {
            string filePath = "todo.txt";
            try
            {
                todoList.SaveToFile(filePath);
                Console.WriteLine("Файл успешно сохранен!");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void LoadFromFile(ToDoList todoList)
        {
            string filePath = "todo.txt";
            try
            {
                todoList.LoadFromFile(filePath);
                Console.WriteLine("Готово");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}