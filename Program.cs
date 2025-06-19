using System;

namespace ToDoApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TaskRunner taskRunner = new TaskRunner();
            //await taskRepository.Newtask();
            //await taskRepository.SeeTask();
            await taskRunner.Welcome();
            bool exit = false;

            while (!exit)
            {
                 exit = await taskRunner.Runner();
            }
        }
    }
}