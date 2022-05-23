namespace CleanArchitecture.Infrastructure.Configurations;

using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TodoItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder
            .Property(todoItem => todoItem.Id)
            .IsRequired();

        builder
            .Property(todoItem => todoItem.Name)
            .IsUnicode(false);
    }
}
