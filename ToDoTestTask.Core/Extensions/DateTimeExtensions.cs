namespace ToDoTestTask.Core.Extensions;

public static class DateTimeExtensions
{
    public static string DateToString(this DateTime dateTime) => 
        dateTime.ToString("dd.MM.yyyy HH:mm");
}