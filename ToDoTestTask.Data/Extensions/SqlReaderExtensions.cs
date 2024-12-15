using Microsoft.Data.SqlClient;
using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Data.Extensions;

public static class SqlReaderExtensions
{
    public static ToDoTask GetTask(this SqlDataReader reader)
    {
        return ToDoTask.Create(
            reader.GetString(reader.GetOrdinal("Title")),
            reader.GetString(reader.GetOrdinal("Description")),
            reader.GetString(reader.GetOrdinal("DueDate")),
            reader.GetString(reader.GetOrdinal("Priority")),
            reader.GetString(reader.GetOrdinal("Status")),
            reader.GetInt32(reader.GetOrdinal("Id")));
    }
}