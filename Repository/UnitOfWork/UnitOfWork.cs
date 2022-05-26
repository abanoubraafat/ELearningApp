using ELearning_App.Domain.Context;

namespace ELearning_App.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ELearningContext Context { get; }
        // Dependency Injection
        public UnitOfWork (ELearningContext _Context)
        {
            Context = _Context;
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
