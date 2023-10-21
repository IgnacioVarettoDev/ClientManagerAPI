namespace ClientManager.Service;

public class LogService: IHostedService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _fileName = "Log.txt";
    private Timer timer;

    public LogService(IWebHostEnvironment env)
    {
        this._env = env;
    }

    public Task StartAsync(CancellationToken cancelToken)
    {
        timer = new Timer(LogWrite, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        Write("Proceso Iniciado.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancelToken)
    {
        timer.Dispose();
        Write("Proceso Terminado.");
        return Task.CompletedTask;
    }

    private void LogWrite(object value)
    {
        Write("Proceso en ejecucion: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
    }

    private void Write(string message)
    {
        var root = $@"{_env.ContentRootPath}\wwwroot\{_fileName}";
        using (StreamWriter writer = new StreamWriter(root, append: true))
        {
            writer.WriteLine(message);
        }
    }
}