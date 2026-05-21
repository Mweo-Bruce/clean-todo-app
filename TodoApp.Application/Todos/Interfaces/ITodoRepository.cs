using TodoApp.Domain.Todos;

namespace TodoApp.Application.Todos.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task AddAsync(TodoItem todo, CancellationToken cancellation = default);
    Task UpdateAsync(TodoItem todo, CancellationToken cancellation = default);
    Task DeleteAsync(TodoItem todo, CancellationToken cancellation = default);
    Task SaveChangesAsync(CancellationToken cancellation = default);
}
