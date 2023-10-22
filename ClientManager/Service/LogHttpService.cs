namespace ClientManager.Service
{
    public class LogHttpService
    {
        readonly IWebHostEnvironment _env;
        private readonly string _fileName = "Log.txt";

        public LogHttpService(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public void WriteException(Exception ex)
        {
            var root = $@"{_env.ContentRootPath}\wwwroot\{_fileName}";
            using (StreamWriter writer = new StreamWriter(root, append: true))
            {
                writer.WriteLine("ERROR: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                writer.WriteLine($"Exception: {ex.Message}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        public void WriteHttp(string context)
        {
            var root = $@"{_env.ContentRootPath}\wwwroot\{_fileName}";
            using (StreamWriter writer = new StreamWriter(root, append: true))
            {
                writer.WriteLine("HTTP: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                writer.WriteLine($"REQUEST RESPONSE: {context}");
            }
        }
    }
}
