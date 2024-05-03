namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarRentalDbContext dbContext;

    public UnitOfWork(CarRentalDbContext dbContext) => this.dbContext = dbContext;

    public async Task SaveChangesAsync() => await this.dbContext.SaveChangesAsync();
}