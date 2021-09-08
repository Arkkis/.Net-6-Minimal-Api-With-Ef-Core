var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<NotesDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.RegisterControllers();
app.Run();