SELECT 
                    Name,
                    Title,
                    Description,
                    IsCompleted,
                    Priority,
                    DateCreated,
                    DueDate
                FROM ToDoList
                WHERE Title = @title