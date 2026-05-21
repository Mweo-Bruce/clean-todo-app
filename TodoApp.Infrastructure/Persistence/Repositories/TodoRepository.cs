using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Todos.Interfaces;
using TodoApp.Domain.Todos;

namespace TodoApp.Infrastructure.Persistence.Repositories;

public class TodoRepository(AppDbContext context) : ITodoRepository
{
    private readonly AppDbContext _context = context;

    public Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.TodoItems
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        await _context.TodoItems.AddAsync(todo, cancellationToken);
    }

    public Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Update(todo);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Remove(todo);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
