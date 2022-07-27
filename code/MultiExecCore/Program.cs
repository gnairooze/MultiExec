// See https://aka.ms/new-console-template for more information
using MultiExec.Business;
using MultiExecCore;

try
{
    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - tool started");

    if (File.Exists("Log.txt"))
    {
        File.Delete("Log.txt");
    }

    ConfigurationHelper helper = new ();

    Manager manager = new Manager(helper.Configuration);

    manager.Events.TimeMessageReceived += Events_TimeMessageReceived;
    manager.Events.MessageReceived += Events_MessageReceived;
    manager.Events.ExceptionReceived += Events_ExceptionReceived;

    manager.Run();

    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - tool completed");
}
catch (Exception ex)
{
    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - tool aborted");

    Console.WriteLine(ex.ToString());

    File.AppendAllText("Log.txt", ex.ToString());
}

void Events_ExceptionReceived(object sender, BasicEventsCore.ExceptionEventArgs e)
{
    Console.WriteLine($"{e.Time.ToString("HH:mm:ss.fff")} - {e.Error.ToString()}");
    File.AppendAllText("Log.txt", e.Error.ToString());
}

void Events_MessageReceived(object sender, BasicEventsCore.MessageEventArgs e)
{
    Console.WriteLine($"{e.Message}");
}

void Events_TimeMessageReceived(object sender, BasicEventsCore.TimeMessageEventArgs e)
{
    Console.WriteLine($"{e.Time.ToString("HH:mm:ss.fff")} - {e.Message}");
}