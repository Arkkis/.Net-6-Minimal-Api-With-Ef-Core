namespace MinApi.Controllers;
public partial class Hello : IEndpointDefinition
{
    private IHttpClientFactory _httpClientFactory;
    private NotesDbContext _dbContext;

    public Hello(IServiceProvider services)
    {
        _httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        _dbContext = services.GetScopedService<NotesDbContext>();
    }

    public async Task<ReturnObject> Action()
    {
        await _dbContext.AddAsync(new Employee { Name = $"Ihminen{new Random().Next(0, 9999)}" });
        await _dbContext.SaveChangesAsync();
        var client = _httpClientFactory.CreateClient();
        var result = await client.GetAsync("https://google.fi");
        return new ReturnObject(string.Join(',', _dbContext.Employees.Select(x => x.Name)));
    }
}