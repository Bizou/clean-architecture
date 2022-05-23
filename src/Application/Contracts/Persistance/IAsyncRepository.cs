namespace CleanArchitecture.Application.Contracts.Persistance;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
