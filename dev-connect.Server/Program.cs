using dev_connect.Server.Utils;
using Microsoft.Extensions.FileProviders;



try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    AuthProvider.Configure(builder.Services, builder.Configuration);
    SwaggerProvider.Configure(builder.Services);
    ComponentRegistry.Registry(builder.Services, builder.Configuration).GetAwaiter().GetResult();

    var app = builder.Build();

    DataMigration.Configure(app.Services);

    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseStaticFiles();


    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    // Log the exception or handle it as needed
    Console.WriteLine($"An error occurred: {ex.Message}");
    // Optionally, rethrow the exception if you want to terminate the application
    throw;
}
