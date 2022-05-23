namespace CleanArchitecture.Application.Features.TodoItems.Commands;

using Contracts.Persistance;
using Domain;
using MediatR;

public record DeleteTodoItemCommand(long Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IAsyncRepository<TodoItem> todoRepository;

    public DeleteTodoItemCommandHandler(IAsyncRepository<TodoItem> todoRepository) =>
        this.todoRepository = todoRepository;

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todo = await this.todoRepository.GetByIdAsync(request.Id, cancellationToken);
        await this.todoRepository.DeleteAsync(todo!, cancellationToken);

        return Unit.Value;
    }
}
