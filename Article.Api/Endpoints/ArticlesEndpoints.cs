using Article.Api.Endpoints.Internal;
using Article.Api.Repositories;


namespace Article.Api.Endpoints;

public class ArticlesEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Article";
    private const string BaseRoute = "api/article";
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetAllAsync)
            .Produces<IEnumerable<Models.Article>>(200)
            .WithOpenApi()
            .WithTags(Tag);
        
        app.MapGet($"{BaseRoute}/{{id}}", GetArticleByIdAsync)
            .Produces<Models.Article>()
            .Produces(404)
            .WithOpenApi()
            .WithTags(Tag);
        
        app.MapDelete($"{BaseRoute}/{{id}}", DeletetArticleAsync)
            .Produces(204)
            .Produces(404)
            .WithOpenApi()
            .WithTags(Tag);
    }

    private static async Task<IResult> DeletetArticleAsync(int id, IArticleRepository articleRepository)
    {
        var deleted = await articleRepository.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult>  GetAllAsync(IArticleRepository articleRepository)
    {
        var books = await articleRepository.GetAllAsync();
        return Results.Ok(books);
    }

    private static async Task<IResult> GetArticleByIdAsync(int id, IArticleRepository articleRepository)
    {
        var article = await articleRepository.Get(id);
        if (article is null)
            return Results.NotFound();
        return Results.Ok(article);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IArticleRepository, ArticleRepository>();
    }
}