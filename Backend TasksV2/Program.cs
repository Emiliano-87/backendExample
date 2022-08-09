using Autofac;
using Backend.Services;

namespace Backend
{
    class Program
    {
        //This is your app entry point
        static void Main(string[] args)
        {
            //Use sepparate class to configure the DI containers
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                //Get your application menu class
                var application = container.Resolve<IApplicationService>();

                //Run your application
                application.Run();
            }
        }
    }
}
