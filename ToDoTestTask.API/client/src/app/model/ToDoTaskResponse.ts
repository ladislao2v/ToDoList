export interface ToDoTaskResponse
{
  id: number;
  title: string;
  description: string;
  dueDate: string;
  priority: string;
  status: string;
  days: number;
  hours: number;
  isRunningOut: boolean;
}
