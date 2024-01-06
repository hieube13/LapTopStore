using Microsoft.Extensions.DependencyInjection;

namespace LapTopStore_Computer.MyDapper
{
    public abstract class BaseApplicationService
    {
        protected IApplicationDbConnection DbConnectionHelper { get; }
        public BaseApplicationService(IServiceProvider serviceProvider)
        {
            DbConnectionHelper = serviceProvider.GetRequiredService<IApplicationDbConnection>(); ;
        }

    }
}
