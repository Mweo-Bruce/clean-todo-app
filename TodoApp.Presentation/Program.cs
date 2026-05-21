using MudBlazor.Services;
using TodoApp.Application.Todos;
using Microsoft.EntityFrameworkCore;
using TodoApp.Presentation.Components;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Application.Todos.Interfaces;
using TodoApp.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MudBlazor
builder.Services.AddMudServices();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Blazor
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
