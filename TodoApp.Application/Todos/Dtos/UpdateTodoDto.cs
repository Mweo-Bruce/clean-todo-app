namespace TodoApp.Application.Todos.Dtos;

public record UpdateTodoDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted
);
