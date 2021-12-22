var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(x =>
{
    x.DocumentName = "Involved Empire";
    x.Title = "Involved Empire";
});

// Database
builder.Services.AddDbContext<EmpireContext>(options => options.UseSqlServer(configuration.GetConnectionString("db")));

// Data Services
builder.Services.AddScoped<EmpireDatabaseService>();
builder.Services.AddSingleton<EmpireCache>();
builder.Services.AddScoped<IEmpireService, EmpireService>();

// Background service
builder.Services.AddHostedService<InvolvedEmpireBackgroundService>();

// Authentication
var key = Encoding.ASCII.GetBytes(configuration.GetSection("Authentication").GetValue<string>("TokenSecret"));
var tokenProvider = new TokenProvider(key);
builder.Services.AddSingleton(tokenProvider);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Cors
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<EmpireHub>("/hub/InvolvedEmpire");
});

app.Run();
