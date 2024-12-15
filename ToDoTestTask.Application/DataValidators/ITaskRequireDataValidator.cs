namespace ToDoTestTask.Application.DataValidators;

public interface ITaskRequireDataValidator
{
    bool TryValidate(string title, string dueDate, string priority, string status = "New");
}