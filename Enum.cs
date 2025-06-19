using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace ToDoApp
{
    public enum UpdateOption
    {
        Name = 1,
        Title,
        Description,
        IsCompleted,
        Priority,
        DueDate
    }

    public enum ToDoOption
    {
        CreateTask = 1,
        ListTasks,
        UpdateTasks,
        DeleteTask,
        Exit
    }
}