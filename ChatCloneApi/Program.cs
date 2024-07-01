using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using ChatCloneApi.AuthorizationRequirements;
using ChatCloneApi.Hubs;
using ChatCloneApi.Models;
using ChatCloneApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new 
        HasScopeRequirement("read:messages", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication().AddJwtBearer();


var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            var origins = new List<string>
            {
                "http://localhost:5173/",
                "https://localhost:5173/",
                "http://localhost:5173"
            };
            
            if (!string.IsNullOrEmpty(frontendUrl))
            {
                origins.Add(frontendUrl);
            }

            policy.WithOrigins(origins.ToArray()).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

builder.Services.Configure<ChatCloneDatabaseSettings>(builder.Configuration.GetSection("ChatCloneDatabase"));
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<GroupService>();


builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());


builder.Services.AddSignalR();



var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapGet("/", () => "Hello World!");


app.MapControllers();
app.MapHub<ChatHub>("/Chat");
app.Run();