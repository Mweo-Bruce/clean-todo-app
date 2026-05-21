using TodoApp.Domain.Todos;
using TodoApp.Application.Todos.Dtos;
using TodoApp.Application.Todos.Interfaces;

namespace TodoApp.Application.Todos;

public class TodoService(ITodoRepository todoRepository)
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    
    public async Task<List<TodoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var todos = await _todoRepository.GetAllAsync(cancellationToken);

        return [.. todos.Select(todo => new TodoDto(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.IsCompleted,
                todo.CreatedAt
        ))];
    }

    public async Task<TodoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoRepository.GetByIdAsync(id, cancellationToken);

        if (todo is null)
            return null;

        return new TodoDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt
        );
    }

    public async Task<TodoDto> CreateAsync(CreateTodoDto dto, CancellationToken cancellationToken = default)
    {
        var todo = new TodoItem(dto.Title, dto.Description);

        await _todoRepository.AddAsync(todo, cancellationToken);
        await _todoRepository.SaveChangesAsync(cancellationToken);

        return new TodoDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt
        );
    }

    public async Task UpdateAsync(UpdateTodoDto dto, CancellationToken cancellationToken = default)
    {
        var todo = await _todoRepository.GetByIdAsync(dto.Id, cancellationToken) ?? throw new Exception("Todo was not found.");

        todo.Update(dto.Title, dto.Description);

        if (dto.IsCompleted)
            todo.MarkAsComplete();
        else
            todo.MarkAsPending();

        await _todoRepository.UpdateAsync(todo, cancellationToken);
        await _todoRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoRepository.GetByIdAsync(id, cancellationToken) ?? throw new Exception("Todo was not found.");

        await _todoRepository.DeleteAsync(todo, cancellationToken);
        await _todoRepository.SaveChangesAsync(cancellationToken);
    }
}
