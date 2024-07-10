using System.Runtime.InteropServices;
using System.Text;
using System;

namespace ToDoListApp
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Поддержка кириллицы
            string filePath = "todo.txt"; // Путь к файлу, где будет сохраняться список задач
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
                            Console.WriteLine("\nВведите задачу: ");
                            string title = Console.ReadLine();
                            todoList.AddTask(title);
                            break;
                        }
                    case '2':
                        {
                            todoList.ViewTasks();
                            break;
                        }
                    case '3':
                        {
                            Console.Write("\nВведите ID задачи: ");
                            if (int.TryParse(Console.ReadLine(), out int taskId))
                            {
                                todoList.MarkTaskAsCompleted(taskId);
                            }
                            else
                            {
                                Console.WriteLine("\nНеверный формат ID");
                            }
                            break;
                        }
                    case '4':
                        {
                            Console.Write("\nВведите ID задачи: ");
                            if (int.TryParse(Console.ReadLine(), out int taskId))
                            {
                                todoList.RemoveTask(taskId);
                            }
                            else
                            {
                                Console.WriteLine("\nНеверный формат ID");
                            }
                            break;
                        }
                    case '5':
                        {
                            todoList.ViewCompletedTasks();
                            break;
                        }
                    case '6':
                        {
                            todoList.ViewUncompletedTasks();
                            break;
                        }
                    case '7':
                        {
                            todoList.SaveToFile(filePath);
                            break;
                        }
                    case '8':
                        {
                            todoList.LoadFromFile(filePath);
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
    }
}