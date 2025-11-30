using TaskCLI.Models;

class Arguments
{
    public static void HandleArgs(string[] args, DatabaseController db)
    {
        switch (args[0])
        {
            case "-h":
            case "--help":
                PrintHelp();
                break;
            case "-l":
            case "--list":
                ListTasks(db);
                break;
            case "-n":
            case "--new":
                AddNewTask(args, db);
                break;
            
            default:
                Console.WriteLine("Unkown Option.");
                break;
        }
    }

    static void ListTasks(DatabaseController db)
    {
        foreach (var task in db.Items)
        {
            var mark = task.IsDone ? "[x]" : "[ ]";
            Console.WriteLine($"{mark}  {task.Title} - {task.Description}");
        }
    }

    static void AddNewTask(string[] args, DatabaseController db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Error: missing task title & description.");
            return;
        }
        string title = args[1];
        string description = args[2];

        db.Items.Add(new TodoItem
        {
            Title = title,
            Description = description
        });

        db.Save();
        Console.WriteLine("Task added");
    }

    static void PrintHelp()
    {
        Console.WriteLine("taskcli usage:");
        Console.WriteLine("  dotnet run             \t\t\tRun interactive mode (TUI)");
        Console.WriteLine("  dotnet run -- -l       \t\t\tList tasks");
        Console.WriteLine("  dotnet run -- -n \"title\" \"description\"  \tAdd new task");
    }
}