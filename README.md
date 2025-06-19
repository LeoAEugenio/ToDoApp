# ToDoApp

Simple task manager with full CRUD operations and persistence in SQL Server. Create as many tasks as you want, view them, update any field, and delete when done.

## Features

- **Create**: add new tasks  
- **Read**: list all stored tasks  
- **Update**: edit title, description or status of any task  
- **Delete**: remove completed or unwanted tasks  
- **Persistence**: all data saved in SQL Server database  

## Technologies

- .NET 9 (C#)  
- Microsoft.Data.SqlClient  
- SQL Server  

---

## Getting Started

1. **Clone the repository**  
   ```bash
   git clone https://github.com/YourUsername/ToDoApp.git

2.Open in VS Code.

3.Adjust the connection string

-Locate the connectionString setting in the code
-Change the Data Source and Database values to point to your SQL Server instance

4. Restore and Run

-dotnet restore
-dotnet build
-dotnet run

