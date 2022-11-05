using Poketto.Api;
using Poketto.Application;
using Poketto.Infrastructure;
using Poketto.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//add application layer here
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddGraphQLServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //IdentityModelEventSource.ShowPII = true;
    //app.UseMigrationsEndPoint();

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.InitializeAsync();
        await initializer.SeedAsync();
    }
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints => endpoints.MapGraphQL());

app.Run();