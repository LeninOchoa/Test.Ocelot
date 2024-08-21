namespace Write.Api.Repositories;

public interface IWriterRepository
{
    Task<List<Models.Writer>> GetAll();
    Task<Models.Writer?> Get(int id);
    Task<bool> Insert(Models.Writer writer);
}