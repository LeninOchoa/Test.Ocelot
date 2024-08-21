using Write.Api.Endpoints.Internal;
using Write.Api.Repositories;

namespace Write.Api.Endpoints;

public class WriterEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Writer";
    private const string BaseRoute = "api/Writer";
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetAllAsync)
            .Produces<IEnumerable<Models.Writer>>(200)
            .WithTags(Tag);
        
        app.MapGet($"{BaseRoute}/{{id}}", GetWriterByIdAsync)
            .Produces<Models.Writer>()
            .Produces(404)
            .WithTags(Tag);
        
        app.MapPost(BaseRoute, CreateWriterAsync)
            .Accepts<Models.Writer>(ContentType)
            .Produces<Models.Writer>(201)
            .WithTags(Tag);
    }

    private static async Task<IResult> CreateWriterAsync(Models.Writer writer, IWriterRepository writerRepository)
    {
        var created = await writerRepository.Insert(writer);
        return Results.Ok(created);
    }

    private static async Task<IResult> GetWriterByIdAsync(int id, IWriterRepository writerRepository)
    {
        var writer = await writerRepository.Get(id);
        if (writer is null)
            return Results.NotFound();
        return Results.Ok(writer);
    }

    private static async Task<IResult> GetAllAsync(IWriterRepository writerRepository)
    {
        var books = await writerRepository.GetAll();
        return Results.Ok(books);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IWriterRepository, WriterRepository>();
    }
}