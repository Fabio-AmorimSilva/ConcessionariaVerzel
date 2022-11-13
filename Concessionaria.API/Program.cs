using Concessionaria.API.Configuration;
using Concessionaria.API.Filters;
using Concessionaria.Aplicacao.Mappers;
using Concessionaria.Infraestrutura.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddAuthConfig(builder.Configuration);
builder.Services.AddAutoMapper(typeof(DominioParaViewModel), typeof(ViewModelParaDominio));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfig();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(new ApplicationExceptionFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider
        (Path.Combine(Directory.GetCurrentDirectory(), "img")),
    RequestPath = new PathString("/img")
});


app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.MapControllers();

app.Run();
