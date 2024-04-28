using GraphqlDemo.Schema.Mutations;
using GraphqlDemo.Schema.Queries;
using GraphqlDemo.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGraphQLServer().AddMutationType<Mutation>().AddQueryType<Query>().AddSubscriptionType<Subscription>().AddInMemorySubscriptions();
 

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

app.UseRouting();

app.UseWebSockets();

app.MapControllers();
app.UseEndpoints(endpoints => endpoints.MapGraphQL());


app.Run();
