namespace CleanArchitecture.Application.Features.TodoItems.Commands;

using Contracts.Persistance;
using Domain;
using MediatR;

public record UpdateTodoItemCommand(long Id, string Name, bool IsComplete) : IRequest;

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IAsyncRepository<TodoItem> todoRepository;

    public UpdateTodoItemCommandHandler(IAsyncRepository<TodoItem> todoRepository) =>
        this.todoRepository = todoRepository;

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoIdem = new TodoItem { Id = request.Id, Name = request.Name, IsComplete = request.IsComplete };

        await this.todoRepository.UpdateAsync(todoIdem, cancellationToken);
        return Unit.Value;
    }
}
