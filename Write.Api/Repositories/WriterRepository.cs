namespace Write.Api.Repositories;

public class WriterRepository : List<Models.Writer>, IWriterRepository
{
    private static readonly List<Models.Writer> _writers = Populate();

    private static List<Models.Writer> Populate()
    {
        return new List<Models.Writer>
        {
            new Models.Writer
            {
                Id = 1,
                Name = "Leanne Graham"
            },
            new Models.Writer
            {
                Id = 2,
                Name = "Ervin Howell"
            },
            new Models.Writer
            {
                Id = 3,
                Name = "Glenna Reichert"
            }
        };
    }

    public Task<List<Models.Writer>> GetAll()
    {
        return Task.FromResult(_writers);
    }

    public Task<bool> Insert(Models.Writer writer)
    {
        var maxId = _writers.Max(x => x.Id);

        writer.Id = ++maxId;
        _writers.Add(writer);

        return Task.FromResult(true);
    }

    public Task<Models.Writer?> Get(int id)
    {
        return  Task.FromResult(_writers.FirstOrDefault(x => x.Id == id));
    }
}