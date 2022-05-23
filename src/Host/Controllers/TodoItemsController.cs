namespace CleanArchitecture.Host.Controllers;

using Application.Features.TodoItems.Commands;
using Application.Features.TodoItems.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ISender sender;

    public TodoItemsController(ISender sender) => this.sender = sender;

    // GET: api/TodoItems
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems(
        [FromQuery] GetTodoItemsQuery query,
        CancellationToken cancellationToken = default
    ) => await this.sender.Send(query, cancellationToken);

    // GET: api/TodoItems/5
    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TodoItem>> GetTodoItem(
        [FromRoute] GetTodoItemQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var todoItem = await this.sender.Send(query, cancellationToken);
        if (todoItem is null)
        {
            return this.NotFound();
        }

        return todoItem;
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // PUT: api/TodoItems/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTodoItem(
        [FromRoute] long id,
        [FromBody] UpdateTodoItemCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            return this.BadRequest();
        }

        try
        {
            await this.sender.Send(command, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            var todoItem = await this.sender.Send(new GetTodoItemQuery(command.Id), cancellationToken);
            if (todoItem is null)
            {
                return this.NotFound();
            }

            throw;
        }

        return this.NoContent();
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // POST: api/TodoItems
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TodoItem>> PostTodoItem(
        CreateTodoItemCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var todoItem = await this.sender.Send(command, cancellationToken);
        return this.CreatedAtAction(nameof(this.GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoItem(
        [FromRoute] DeleteTodoItemCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await this.sender.Send(command, cancellationToken);
        }
        catch (ArgumentNullException)
        {
            var todoItem = await this.sender.Send(new GetTodoItemQuery(command.Id), cancellationToken);
            if (todoItem is null)
            {
                return this.NotFound();
            }

            throw;
        }

        return this.NoContent();
    }
}
