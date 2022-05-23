namespace CleanArchitecture.Infrastructure.Persistance;

using Application.Contracts.Persistance;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

public class TodoRepository : IAsyncRepository<TodoItem>
{
    private readonly TodoContext context;

    public TodoRepository(TodoContext context) => this.context = context;

    public async Task<TodoItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default) =>
        await this.context.TodoItems.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IReadOnlyList<TodoItem>> ListAllAsync(CancellationToken cancellationToken = default) =>
        await this.context.TodoItems.ToListAsync(cancellationToken);

    public async Task<TodoItem> AddAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        await this.context.TodoItems.AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        this.context.Entry(entity).State = EntityState.Modified;
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken = default)
    {
        this.context.TodoItems.Remove(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
