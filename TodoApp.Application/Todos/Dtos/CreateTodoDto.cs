namespace TodoApp.Application.Todos.Dtos;

public record CreateTodoDto(
    string Title,
    string? Description
);
