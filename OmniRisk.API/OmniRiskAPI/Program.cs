<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Api;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Services;
using OmniRiskAPI.Setup;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                          //policy.WithOrigins("http://example.com",
                          //                    "http://www.contoso.com");
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
                       builder.Configuration.GetConnectionString("Test");

builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<OmniRiskDbContext>()
        .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<TwitterService, TwitterService>();
builder.Services.AddScoped<GptService, GptService>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

=======
using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Api;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Setup;
using Swashbuckle.AspNetCore.SwaggerGen;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            //policy.WithOrigins("http://example.com",
            //                    "http://www.contoso.com");
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

builder.Services.AddAuthentication(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddTokenService();


var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
                       builder.Configuration.GetConnectionString("Test");

builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);
builder.Services.AddCurrentUser();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<OmniRiskDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

>>>>>>> 25d184dea064136e45371423d94c33da38ba1aaa
app.Run();