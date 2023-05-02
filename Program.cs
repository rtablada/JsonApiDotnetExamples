using JsonApiDotNetCore.Configuration;
using JsonApiExample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("ExampleDb");
    options.UseNpgsql(connectionString);
});

builder.Services.AddJsonApi<AppDbContext>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthorization();
app.UseJsonApi();
app.MapControllers();

await SetupDb(app.Services);

app.Run();

static async Task SetupDb(IServiceProvider serviceProvider)
{
    await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await CreateDatabaseAsync(dbContext);
    await InsertData(dbContext);
}

static async Task InsertData(AppDbContext dbContext)
{
    var usa = new Country { Name = "United States", Population = new Population { Pop = 334702010, EstimatePop = 0 } };
    var mexico = new Country { Name = "Mexico", Population = new Population { Pop = 129830000, EstimatePop = 0 } };
    var uk = new Country { Name = "United Kingdon", Population = new Population { Pop = 67830000, EstimatePop = 0 } };
    var sweeden = new Country { Name = "Sweeden", Population = new Population { Pop = 10830000, EstimatePop = 1 } };

    var regions = new List<Region> {
        new Region() {
            Name = "North America",
            Countries = new List<Country> { usa, mexico }
        },
        new Region() {
            Name = "Europe",
            Countries = new List<Country> { uk, sweeden }
        },
    };

    dbContext.Regions.AddRange(regions);
    await dbContext.SaveChangesAsync();
}

static async Task CreateDatabaseAsync(AppDbContext dbContext)
{
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
}