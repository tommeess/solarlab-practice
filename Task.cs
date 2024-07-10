using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoListApp
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public Task(string title)
        {
            Title = title;
            IsDone = false;
        }
    }
}