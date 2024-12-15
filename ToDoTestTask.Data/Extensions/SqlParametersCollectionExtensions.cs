using Microsoft.Data.SqlClient;
using ToDoTestTask.Core.Extensions;
using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Data.Extensions;

public static class SqlParametersCollectionExtensions
{
    public static SqlParameterCollection AddTaskParameters(this SqlParameterCollection collection, ToDoTask task)
    {
        collection.AddWithValue("@Title", task.Title);
        collection.AddWithValue("@Description", task.Description);
        collection.AddWithValue("@DueDate", task.DueTime.DateToString());
        collection.AddWithValue("@Priority", task.Priority.ToString());
        collection.AddWithValue("@Status", task.Status.ToString());

        return collection;
    }
    
    public static SqlParameterCollection AddTaskParametersWithId(this SqlParameterCollection collection, ToDoTask task)
    {
        collection.AddTaskParameters(task);
        collection.AddWithValue("@Id", task.Id);

        return collection;
    }
}