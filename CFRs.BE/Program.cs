using CFRs.BE;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHangfire(config => {
    config.UseSqlServerStorage(CFRs.DAL.Helper.GetSettingsHelper.CFRsConnectionString);
});

builder.Services.AddHangfireServer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Centralized Financial Records", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

app.UseHangfireDashboard();

startup.Configure(app);

//app.UseFileServer(new FileServerOptions
//{
//    FileProvider = new PhysicalFileProvider(@"\\server\path"),
//    RequestPath = new PathString("/Export"),
//    EnableDirectoryBrowsing = true
//});

string fileProvider = Directory.GetCurrentDirectory() + "/Export";
const string cacheMaxAge = "604800";
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".msg"] = "application/vnd.ms-outlook";
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(fileProvider),
    RequestPath = new PathString("/" + "Export"),
    OnPrepareResponse = ctx =>
    {
        // using Microsoft.AspNetCore.Http;
        ctx.Context.Response.Headers.Append(
            "Cache-Control", $"public, max-age={cacheMaxAge}");
    }
        ,
    ContentTypeProvider = provider
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//    //app.UseSwaggerUI(c =>
//    //{
//    //    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
//    //    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "CFRs.BE v1.0.0");
//    //});
//}

//app.UseHttpsRedirection();

////app.UseAuthentication();
//app.UseAuthorization();

////app.MapControllers();

//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();

    //app.UseSwaggerUI();

    //Set Swagger to Default Page
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Centralized Financial Records");
        c.InjectStylesheet("/swagger/custom.css");
        c.RoutePrefix = String.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();