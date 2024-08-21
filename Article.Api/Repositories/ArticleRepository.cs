namespace Article.Api.Repositories;

public class ArticleRepository : List<Models.Article>, IArticleRepository
{
    private static readonly List<Models.Article> _articles = Populate();
    private static List<Models.Article> Populate()
    {
        var result = new List<Models.Article>()
        {
            new Models.Article
            {
                Id = 1,
                Title = "First Article",
                WriterId = 1,
                LastUpdate = DateTime.Now
            },
            new Models.Article
            {
                Id = 2,
                Title = "Second title",
                WriterId = 2,
                LastUpdate = DateTime.Now
            },
            new Models.Article
            {
                Id = 3,
                Title = "Third title",
                WriterId = 3,
                LastUpdate = DateTime.Now
            }
        };
        return result;
    }

    public Task<Models.Article?> Get(int id)
    {
        return Task.FromResult(_articles.FirstOrDefault(x => x.Id == id));
    }

    public Task<List<Models.Article>> GetAllAsync()
    {
        return Task.FromResult( _articles);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var removed = _articles.SingleOrDefault(x => x.Id == id);
        if (removed != null)
            _articles.Remove(removed);
        return Task.FromResult(removed != null);
    }
}