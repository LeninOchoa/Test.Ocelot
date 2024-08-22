using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Ocelot
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = "OcelotConfiguration";
});
builder.Services.AddOcelot(builder.Configuration).AddAppConfiguration();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
#endregion


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Ocelot
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";

});
app.UseWebSockets();
app.UseOcelot().Wait(); 
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Run();
