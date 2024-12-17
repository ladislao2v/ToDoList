# ToDoList
To-do-List web application

Стек используемых технологий: ASP.NET (backend), Angular (frontend), SQL Server (Database)

## Гайд по запуску
1. Выполнить T-SQL (DBSQL.sql) запрос на вашем SQL Server (создает нужную базу данных и таблицу для хранения задач);
2. Запустить Program.cs доменной области API для запуска сервера (http://localhost:5171/);
3. Выполнить команду "ng serve" в папке проекта для запуска клиента (http://localhost:4200);
4. ДОПОЛНИТЕЛЬНО: Запустить Program.cs доменной области Additional для запуска фонового сервиса;
5. ДОПОЛНИТЕЛЬНО: Для запуска юнит-тестов трубуется вручную подготовить базы данных для проверок различных ситуаций и вставить ConnectionString в методы тестов. 

## Описание проекта:
Функционал приложения:
1. Создание, редактирование и удаление информации о задачах (с учетом проверок валидности данных);
2. Cортировка задач (по приоритету, статусу и дате ззавершения);
3. Уведомление пользователей об истекающих задачах с истекающим сроком (менее 8 часов, либо менее 24 часов при высоком приоритете).

## Структура проекта
API: содержит логику запуска сервера и обрабаботки внешние http-запросов;

Application: Сервисный слой приложения;

Core: содержит описание модели приложения;

Data: содержит логику для работы с базой данных; 

Additional: Консольный проект (Фоновый сервис) для перевода задач в архив;

Tests: Юнит-тесты приложения.

## Демонстрация работы:
Главная страница приложения:

![Main page](images/main.png "Main page")

Страница добавления задачи:

![Adding page](images/adding.png "Adding page")

Уведомление о задачах с истекающих сроком (менее 8 часов):

![Notification](images/notification.png "Notification")

Предупреждение о задачах с высоким приоритетом и с истекающих сроком (менее 24 часов):

![Warning](images/warning.png "Warning")

Редактирование:

![Edit](images/editing.png "Edit")

Удаление:

![Delete](images/deleting.png "Delete")

Сортиврока по статусу:

![Status sort](images/sortingbystatus.png "Status")

Сортиврока по приоритету:

![Priority sort](images/sortingbypriority.png "Priority")

Сортиврока по дате завершения:

![DueDate sort](images/sortingbyduedate.png "DueDate")

Задачи, которые закончатся в течении часа:

![Less One Hour](images/lessonehour.png "LessOneHour")

Задачи, которые закончились:

![Expired](images/expired.png "Expired")

Перевод задач в архив фоновым сервисом:

![Expired](images/backgroundapp.png "Background service")
