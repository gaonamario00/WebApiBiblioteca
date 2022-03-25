namespace WebApiBiblioteca.Services
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Tarea 1.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando la cargamos la aplicacion una vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(120));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando detenemos la aplicacion aunque puede no ejecutarse por algun error.
            timer.Dispose();
            Escribir("Proceso finalizado");
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            //Escribir("Proceso en ejecucion " + DateTime.Now.ToString("G"));
            Escribir("El Profe Gustavo Rodriguez es el mejor " + DateTime.Now.ToString("G"));
        }

        public void Escribir(string msg)
        {
            //timer.Dispose();
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(msg);
            }
        }
    }
}
