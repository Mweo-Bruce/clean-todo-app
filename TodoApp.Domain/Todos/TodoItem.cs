namespace TodoApp.Domain.Todos;

public class TodoItem
{
    // properties for a todo item
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // for domain integrity use constructor initialization to
    // enforce that a todo item cannot exits without a title
    private TodoItem()
    {
        Title = string.Empty;
    }

    // guard clause to prevent developers or
    // users from passing an empty string or spaces
    public TodoItem(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Todo title is required.");

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Todo title is required.");

        Title = title;
        Description = description;
    }

    public void MarkAsComplete()
    {
        IsCompleted = true;
    }

    public void MarkAsPending()
    {
        IsCompleted = false;
    }
}
