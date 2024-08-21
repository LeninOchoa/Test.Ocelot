namespace Article.Api.Repositories;

public interface IArticleRepository
{
    Task<Models.Article?> Get(int id);
    Task<List<Models.Article>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}