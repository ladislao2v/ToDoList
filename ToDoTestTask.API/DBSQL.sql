CREATE DATABASE ToDoList;

USE ToDoList;

CREATE TABLE Tasks (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Title VARCHAR(100) NOT NULL,
	Description VARCHAR(500),
	DueDate VARCHAR(25) NOT NULL,
	Priority VARCHAR(25) NOT NULL,
	Status VARCHAR(25) NOT NULL
);