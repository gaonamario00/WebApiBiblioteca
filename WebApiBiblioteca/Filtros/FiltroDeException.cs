using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiBiblioteca.Filtros
{
    public class FiltroDeException: ExceptionFilterAttribute
    {

        private readonly ILogger<FiltroDeException> log;

        public FiltroDeException(ILogger<FiltroDeException> log)
        {
            this.log = log;
        }
        
        public override void OnException(ExceptionContext context)
        {
            log.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
       
    }
}
