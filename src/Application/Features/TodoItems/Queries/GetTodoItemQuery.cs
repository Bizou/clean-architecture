namespace CleanArchitecture.Application.Features.TodoItems.Queries;

using Contracts.Persistance;
using Domain;
using MediatR;

public record GetTodoItemQuery(long Id) : IRequest<TodoItem?>;

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItem?>
{
    private readonly IAsyncRepository<TodoItem> todoRepository;

    public GetTodoItemQueryHandler(IAsyncRepository<TodoItem> todoRepository) => this.todoRepository = todoRepository;

    public async Task<TodoItem?> Handle(GetTodoItemQuery request, CancellationToken cancellationToken) =>
        await this.todoRepository.GetByIdAsync(request.Id, cancellationToken);
}
