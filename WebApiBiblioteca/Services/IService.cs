namespace WebApiBiblioteca.Interfaces
{
    public interface IService
    {
        void ejecutarJob();
        Guid GetScoped();
        Guid GetSingleton();
        Guid GetTransient();

    }

    public class ServiceA : IService
    {

        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<ServiceA> logger;
        private readonly ServiceTransient serviceTransient;

        public ServiceA(ILogger<ServiceA> logger
            ,ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton)
        {
            this.logger = logger;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
        }

        public void ejecutarJob()
        {
        }

        public Guid GetScoped()
        {
            return serviceScoped.guid;
        }

        public Guid GetSingleton()
        {
            return serviceSingleton.guid;
        }

        public Guid GetTransient()
        {
            return serviceTransient.guid;
        }
    }

    public class serviceB : IService
    {
        public void ejecutarJob()
        {
            throw new NotImplementedException();
        }

        public Guid GetScoped()
        {
            throw new NotImplementedException();
        }

        public Guid GetSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid GetTransient()
        {
            throw new NotImplementedException();
        }
    }

    public class ServiceTransient
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServiceScoped
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServiceSingleton
    {
        public Guid guid = Guid.NewGuid();
    }
}
