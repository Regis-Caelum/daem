using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<YabieBackendContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("YabieBackendContext") ??
                          throw new InvalidOperationException("Connection string 'YabieBackendContext' not found.")));

    // Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"./Credentials/awesomeproject-c835b-firebase-adminsdk-fgppq-7236f7edf7.json");
    // builder.Services.AddSingleton(FirebaseApp.Create());
    // builder.Services.AddAuthentication("DefaultScheme")
        // .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("FirebaseAuthScheme", _ => {});
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    // app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
}

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<YabieBackendContext>();
    context.Database.Migrate();
}


app.Run();