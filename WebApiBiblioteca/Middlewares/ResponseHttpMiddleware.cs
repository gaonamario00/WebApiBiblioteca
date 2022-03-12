namespace WebApiBiblioteca.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHttpMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseHttpMiddleware>();
        }
    }

    public class ResponseHttpMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<ResponseHttpMiddleware> logger;

        public ResponseHttpMiddleware(RequestDelegate siguiente, ILogger<ResponseHttpMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var as = new MemoryStream())
            {
                //se asinga el body del responde en una variable y se le da el valor de memorystream
                var bodyoriginal = context.response.body;
                context.response.body = as;

                //permite continuar con la linea
                await siguiente.invoke();

                //guardamos lo que le respondemos al cliente en el string
                as.seek(0, seekorigin.begin);
                string response = new streamreader(as).readtoend();
                as.seek(0, seekorigin.begin);

                //leemos el stream y lo colocamos como estaba
                await as.copytoasync(bodyoriginal);
                context.response.body = bodyoriginal;

                logger.loginformation(response);

            }
        }

    }
}
