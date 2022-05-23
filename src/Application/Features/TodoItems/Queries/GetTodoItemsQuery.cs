namespace CleanArchitecture.Application.Features.TodoItems.Queries;

using Contracts.Persistance;
using Domain;
using MediatR;

public record GetTodoItemsQuery(int PageIndex, int PageSize) : IRequest<List<TodoItem>>;

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItem>>
{
    private readonly IAsyncRepository<TodoItem> todoRepository;

    public GetTodoItemsQueryHandler(IAsyncRepository<TodoItem> todoRepository) => this.todoRepository = todoRepository;

    public async Task<List<TodoItem>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await this.todoRepository.ListAllAsync(cancellationToken);

        return items
            .OrderByDescending(todoItem => todoItem.Id)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
    }
}
