using ToDoTestTask.Application.DataValidators;
using ToDoTestTask.Application.Services.TimeService;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Data.Repositories;

namespace ToDoTestTask.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddToDoList(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IToDoTasksRepository, ToDoTasksRepository>( provider =>
            {
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                string? connectionString = configuration.GetConnectionString("Database");
                return new ToDoTasksRepository(connectionString);
            })
            .AddScoped<ITaskRequireDataValidator, TaskRequireDataValidator>()
            .AddScoped<ITimeService, TimeService>()
            .AddScoped<IToDoTasksService, ToDoTasksService>();
    }
}