using System;
using System.Runtime.CompilerServices;

namespace ToDoApp
{
    class TaskRunner
    {
        TaskRepository taskRepository = new TaskRepository();
        string banner = @"
            ╔═════════════════════════════╗
            ║          TO DO LIST         ║
            ╚═════════════════════════════╝
            ";
        public async Task Welcome()
        {
            Console.WriteLine(banner);

            Console.WriteLine(@"
            Welcome to your To Do List.
            
            Here, you will be able to organize and track tasks you need to complete.
            You can prioritize your work, set deadlines, and make sure nothing important gets forgotten.
            ");

        }
        
        public async Task<bool> Runner()
        {

            int input;
            do
            {
                Console.WriteLine("What would you like to do now:\n\n1)Create a new task\n2)See my tasks\n3)Update a task\n4)Delete a task\n5)Exit");

                if (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
                {
                    Console.WriteLine("Enter a valid option.");
                }
            } while (input <= 0);

            ToDoOption toDoOption = (ToDoOption)input;

            switch (toDoOption)
            {
                case ToDoOption.CreateTask:
                    await taskRepository.Newtask();
                    break;
                case ToDoOption.ListTasks:
                    await taskRepository.SeeTask();
                    break;
                case ToDoOption.UpdateTasks:
                    await taskRepository.UpdateTask();
                    break;
                case ToDoOption.DeleteTask:
                    await taskRepository.RemoveTask();
                    break;
                case ToDoOption.Exit:
                    return true;
            }
            return false;

        }

    }
}