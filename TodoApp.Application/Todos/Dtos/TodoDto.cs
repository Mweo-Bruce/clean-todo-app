namespace TodoApp.Application.Todos.Dtos;

public record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt
);
