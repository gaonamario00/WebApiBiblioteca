using System.IO;

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
            using (var ms = new MemoryStream())
            {
                //se asinga el body del responde en una variable y se le da el valor de memorystream
                var bodyoriginal = context.Response.Body;
                context.Response.Body = ms;

                //permite continuar con la linea
                await siguiente(context);

                //guardamos lo que le respondemos al cliente en el string
                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                //leemos el stream y lo colocamos como estaba
                await ms.CopyToAsync(bodyoriginal);
                context.Response.Body = bodyoriginal;

                logger.LogInformation(response);

            }
        }

    }
}
