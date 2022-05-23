namespace CleanArchitecture.Application.Features.TodoItems.Commands;

using Contracts.Persistance;
using Domain;
using MediatR;

public record CreateTodoItemCommand(string Name, bool IsComplete) : IRequest<TodoItem>;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
{
    private readonly IAsyncRepository<TodoItem> todoRepository;

    public CreateTodoItemCommandHandler(IAsyncRepository<TodoItem> todoRepository) =>
        this.todoRepository = todoRepository;

    public async Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken) =>
        await this.todoRepository.AddAsync(
            new TodoItem { Name = request.Name, IsComplete = request.IsComplete },
            cancellationToken
        );
}
