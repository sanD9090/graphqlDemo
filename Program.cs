using GraphqlDemo.Schema.Mutations;
using GraphqlDemo.Schema.Queries;
using GraphqlDemo.Schema.Subscriptions;
using GraphqlDemo.Services;
using GraphqlDemo.Services.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddGraphQLServer().AddMutationType<Mutation>().AddQueryType<Query>().AddSubscriptionType<Subscription>().AddInMemorySubscriptions();

var connectionString = configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>( o => o.UseSqlite(connectionString));

builder.Services.AddScoped<CoursesRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// to apply the migration whenever the project is run
IHost host = app;
using (IServiceScope scope = host.Services.CreateScope())
{
    IDbContextFactory<SchoolDbContext> contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using(SchoolDbContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}
    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseWebSockets();

app.MapControllers();
app.UseEndpoints(endpoints => endpoints.MapGraphQL());


app.Run();
