using System.Globalization;

namespace ToDoTestTask.Application.DataValidators;

public sealed class TaskRequireDataValidator : ITaskRequireDataValidator
{
    public bool TryValidate(string title, string dueDate, string priority, string status = "New")
    {
        if (string.IsNullOrEmpty(title))
            return false;
        
        if(string.IsNullOrEmpty(dueDate))
            return false;

        if (!DateTime.TryParseExact(
                dueDate,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result))
            return false;
        
        if(string.IsNullOrEmpty(priority))
            return false;

        if (string.IsNullOrEmpty(status))
            return false;

        return true;
    }
}