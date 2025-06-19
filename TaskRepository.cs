using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace ToDoApp
{
    class TaskRepository
    {
        User user = new User();
        Tasks tasks = new Tasks();
        TaskUpdater taskUpdater = new TaskUpdater();
        public async Task Newtask()
        {
            do
            {
                Console.WriteLine("Enter you name: ");
                user.Name = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    Console.WriteLine("Enter a valid name.");
                }
            } while (string.IsNullOrWhiteSpace(user.Name));

            do
            {
                Console.WriteLine("Enter your task title: ");
                tasks.Title = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(tasks.Title))
                {
                    Console.WriteLine("Enter a valid title.");
                }
            } while (string.IsNullOrWhiteSpace(tasks.Title));

            do
            {
                Console.WriteLine("Enter your task description: ");
                tasks.Description = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(tasks.Description))
                {
                    Console.WriteLine("Enter a valid description.");
                }
            } while (string.IsNullOrWhiteSpace(tasks.Description));

            int input;
            do
            {
                Console.WriteLine("Enter the priority of the task: (Min 1 - 5 Max)");

                if (!int.TryParse(Console.ReadLine(), out input) || input < 0)
                {
                    Console.WriteLine("Enter a valid priority.");
                }

            } while (input < 0);

            tasks.Priority = input;

            DateTime due;
            do
            {
                Console.Write("Enter the due date (yyyy-MM-dd): ");
                var raw = Console.ReadLine() ?? "";

                if (!DateTime.TryParse(raw, out due))
                {
                    Console.WriteLine("Enter a valid date.");
                }
            } while (due == default);

            tasks.DueDate = due;

            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlInsert = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Inserttask.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlInsert, connection);

            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@title", tasks.Title);
            command.Parameters.AddWithValue("@description", tasks.Description);
            command.Parameters.AddWithValue("@iscompleted", false);
            command.Parameters.AddWithValue("@priority", tasks.Priority);
            command.Parameters.AddWithValue("@datecreated", tasks.DateCreated);
            command.Parameters.AddWithValue("@duedate", tasks.DueDate);

            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Task entered successfully!");
            }
            else
            {
                Console.WriteLine("Something went wrong, try it again.");
            }
        }

        public async Task SeeTask()
        {
            // 1) Prompt for a non‐empty title
            do
            {
                Console.Write("Enter the task title: ");
                tasks.Title = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(tasks.Title))
                    Console.WriteLine("Enter a valid title.");
            }
            while (string.IsNullOrWhiteSpace(tasks.Title));

            // 2) Prepare your connection and SELECT statement
            var DBPath =
                "Server=DESKTOP-6UQP3HM;" +
                "Database=ToDo;" +
                "Trusted_Connection=True;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;";

            string sqlSelect = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Selecttask.sql")
            );

            using var conn = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlSelect, conn);
            command.Parameters.AddWithValue("@title", tasks.Title);

            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            // 4) If there are no rows, tell the user
            if (!reader.Read())
            {
                Console.WriteLine("Task not found.");
                return;
            }

            // 5) Read each column
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string title = reader.GetString(reader.GetOrdinal("Title"));
            string description = reader.GetString(reader.GetOrdinal("Description"));
            bool done = reader.GetBoolean(reader.GetOrdinal("IsCompleted"));
            int priority = reader.GetByte(reader.GetOrdinal("Priority"));
            DateTime created = reader.GetDateTime(reader.GetOrdinal("DateCreated"));

            // DueDate might be NULL in the DB, so guard against DBNull:
            DateTime? dueDate = reader.IsDBNull(reader.GetOrdinal("DueDate"))
                ? (DateTime?)null
                : reader.GetDateTime(reader.GetOrdinal("DueDate"));

            // 6) Print results
            Console.WriteLine("---- Task Details ----");
            Console.WriteLine($"Name:        {name}");
            Console.WriteLine($"Title:       {title}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Completed:   {done}");
            Console.WriteLine($"Priority:    {priority}");
            Console.WriteLine($"Created at:  {created:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Due on:      {(dueDate.HasValue ? dueDate.Value.ToString("yyyy-MM-dd") : "n/a")}");
        }

        public async Task UpdateTask()
        {
            int input;
            do
            {
                Console.WriteLine("What would like to update?\n\n1)Name\n2)Title\n3)Description\n4)Is Completed?\n5)Priority\n6)Date Created\n7)Due Date\n");

                if (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
                {
                    Console.WriteLine("Enter a valid option.");
                }
            } while (input <= 0);

            UpdateOption updateOption = (UpdateOption)input;

            switch (updateOption)
            {
                case UpdateOption.Name:
                    await taskUpdater.UpdateName();
                    break;
                case UpdateOption.Title:
                    await taskUpdater.UpdateTitle();
                    break;
                case UpdateOption.Description:
                    await taskUpdater.UpdateDesc();
                    break;
                case UpdateOption.IsCompleted:
                    await taskUpdater.UpdateCompleteCheckBox();
                    break;
                case UpdateOption.Priority:
                    await taskUpdater.UpdatePriority();
                    break;
                case UpdateOption.DueDate:
                    await taskUpdater.UpdateDueDate();
                    break;
            }
        }

        public async Task RemoveTask()
        {
            var titles = new List<string>();

            var DBPath =
                "Server=DESKTOP-6UQP3HM;" +
                "Database=ToDo;" +
                "Trusted_Connection=True;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;";

            var sqlSelect = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Selecttitle.sql")
            );

            using (var connection = new SqlConnection(DBPath))
            using (var selectCmd = new SqlCommand(sqlSelect, connection))
            {
                await connection.OpenAsync();
                using (var reader = await selectCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        titles.Add(reader.GetString(0));
                    }
                }
            }

            if (titles.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("Current tasks:");
            for (int i = 0; i < titles.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {titles[i]}");
            }

            string titleToDelete;
            do
            {
                Console.Write("Enter the exact task title you want to delete: ");
                titleToDelete = Console.ReadLine()?? "";
                if (string.IsNullOrWhiteSpace(titleToDelete))
                    Console.WriteLine("Please enter a valid title.");
            }
            while (string.IsNullOrWhiteSpace(titleToDelete));

            var sqlDelete = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Deletetask.sql")
            );

            using (var connection = new SqlConnection(DBPath))
            using (var deleteCmd = new SqlCommand(sqlDelete, connection))
            {
                deleteCmd.Parameters.AddWithValue("@title", titleToDelete);

                await connection.OpenAsync();
                int rowsAffected = await deleteCmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                    Console.WriteLine($"No task found with title “{titleToDelete}”.");
                else
                    Console.WriteLine($"Task “{titleToDelete}” deleted successfully.");
            }

            titles.Clear();
            using (var connection = new SqlConnection(DBPath))
            using (var selectCmd = new SqlCommand(sqlSelect, connection))
            {
                await connection.OpenAsync();
                using (var reader = await selectCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())

                        titles.Add(reader.GetString(0));
                }
            }

            Console.WriteLine("\nRemaining tasks:");
            if (titles.Count == 0)
            {
                Console.WriteLine("None.");
            }
            else
            {
                foreach (var t in titles) 
                    Console.WriteLine($"- {t}");
            }
        }


           
    }
}