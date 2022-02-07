using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure((Action<ClusterOptions>)(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "dev";
    }));
    siloBuilder.UseAdoNetClustering(options =>
    {
        //Qekjo merrret esht string statik n'dbconfig tAdoNet
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
    });
    siloBuilder.AddAdoNetGrainStorage("betStorage", options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
        options.UseJsonFormat = true;
    });
    siloBuilder.ConfigureApplicationParts
    (
        parts => parts.AddApplicationPart(typeof(BetGrain).Assembly).WithReferences()
    );
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
