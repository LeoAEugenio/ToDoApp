using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace ToDoApp
{
    class Tasks
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Iscompleted { get; set; }
        public int Priority { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; }
    }
}