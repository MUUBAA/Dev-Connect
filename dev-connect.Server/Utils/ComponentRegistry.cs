namespace dev_connect.Server.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Converters;
using dev_connect.Server.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using dev_connect.Server.Data.Filters;
using Microsoft.AspNetCore.Authentication;
using dev_connect.Server.Services.AuthService;
using dev_connect.Server.Services.UserService;

public class ComponentRegistry
    {
    public static async Task Registry(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        services.AddDbContext<Repository>(options =>
        {
            string? ConnectionString = configuration.GetConnectionString("Repository");
            options.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 25)));
        });

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomException));
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        }
    }

