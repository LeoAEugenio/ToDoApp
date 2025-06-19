using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace ToDoApp
{
    class TaskUpdater
    {
        User user = new User();
        Tasks tasks = new Tasks();
        public async Task UpdateName()
        {
            do
            {
                Console.WriteLine("Enter your current name: ");
                user.Name = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    Console.WriteLine("Enter a valid name.");
                }
            } while (string.IsNullOrWhiteSpace(user.Name));

            string Newname;
            do
            {
                Console.WriteLine("Enter your new name: ");
                Newname = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(Newname))
                {
                    Console.WriteLine("Enter a valid name.");
                }
            } while (string.IsNullOrWhiteSpace(Newname));

            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updatename.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@newname", Newname);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }

        public async Task UpdateTitle()
        {
            do
            {
                Console.WriteLine("Enter your current title: ");
                tasks.Title = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(tasks.Title))
                {
                    Console.WriteLine("Enter a valid title.");
                }
            } while (string.IsNullOrWhiteSpace(tasks.Title));

            string Newtitle;
            do
            {
                Console.WriteLine("Enter your new title: ");
                Newtitle = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(Newtitle))
                {
                    Console.WriteLine("Enter a valid title.");
                }
            } while (string.IsNullOrWhiteSpace(Newtitle));

            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updatetitle.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@title", tasks.Title);
            command.Parameters.AddWithValue("@newtitle", Newtitle);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }

        public async Task UpdateDesc()
        {
            do
            {
                Console.WriteLine("Enter your current description: ");
                tasks.Description = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(tasks.Description))
                {
                    Console.WriteLine("Enter a valid description.");
                }
            } while (string.IsNullOrWhiteSpace(tasks.Description));

            string Newdescription;
            do
            {
                Console.WriteLine("Enter your new description: ");
                Newdescription = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(Newdescription))
                {
                    Console.WriteLine("Enter a valid description.");
                }
            } while (string.IsNullOrWhiteSpace(Newdescription));

            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updatedesc.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@description", tasks.Description);
            command.Parameters.AddWithValue("@newdescription", Newdescription);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }

        public async Task UpdateCompleteCheckBox()
        {
            do
            {
                Console.WriteLine("Enter your name: ");
                user.Name = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    Console.WriteLine("Enter a valid name.");
                }
            } while (string.IsNullOrWhiteSpace(user.Name));

            bool checkbox;

            Console.WriteLine("Would you like to mark it as done or not?:\nFalse)In Progress - True)Done ");

            if (bool.TryParse(Console.ReadLine(), out checkbox))
            {
                tasks.Iscompleted = checkbox;
            }
            else
            {
                Console.WriteLine("Enter a valid value.");
            }


            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updatecompletecheckbox.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@name", user.Name);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }

        public async Task UpdatePriority()
        {
            int input;
            do
            {
                Console.WriteLine("Enter the priority of the task: ");

                if (!int.TryParse(Console.ReadLine(), out input) || input < 0)
                {
                    Console.WriteLine("Enter a valid priority.");
                }

            } while (input < 0);

            tasks.Priority = input;

            int Newpriority;
            do
            {
                Console.WriteLine("Enter the priority of the task: ");

                if (!int.TryParse(Console.ReadLine(), out Newpriority) || Newpriority < 0)
                {
                    Console.WriteLine("Enter a valid priority.");
                }

            } while (Newpriority < 0);

            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updatepriority.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@priority", tasks.Priority);
            command.Parameters.AddWithValue("@newpriority", Newpriority);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }
        
        public async Task UpdateDueDate()
        {
            DateTime due;
            do
            {
                Console.Write("Enter your current due date (yyyy-MM-dd): ");
                var raw = Console.ReadLine() ?? "";

                if (!DateTime.TryParse(raw, out due))
                {
                    Console.WriteLine("Enter a valid date.");
                }
            } while (due == default);

            tasks.DueDate = due;

            DateTime Newdue;
            do
            {
                Console.Write("Enter your current due date (yyyy-MM-dd): ");
                var raw = Console.ReadLine() ?? "";

                if (!DateTime.TryParse(raw, out Newdue))
                {
                    Console.WriteLine("Enter a valid date.");
                }
            } while (Newdue == default);

            


            var DBPath =
            "Server=DESKTOP-6UQP3HM;" +
            "Database=ToDo;" +
            "Trusted_Connection=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

            var sqlUpdate = File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "sql", "Updateduedate.sql")
            );

            using var connection = new SqlConnection(DBPath);
            using var command = new SqlCommand(sqlUpdate, connection);

            command.Parameters.AddWithValue("@duedate", tasks.Priority);
            command.Parameters.AddWithValue("@newduedate", Newdue);


            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                Console.WriteLine("Update executed successfully");

                
            }
            else
            {
                Console.WriteLine("Something went wrong, try again.");
            }
        }
    }
}