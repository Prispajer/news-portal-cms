using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Mappings;
using NewsPortalCMS.Application.Services;
using NewsPortalCMS.Application.Validators.Business;
using NewsPortalCMS.Application.Validators.Fluent.Article;
using NewsPortalCMS.Application.Validators.Fluent.Category;
using NewsPortalCMS.Domain.Services;
using NewsPortalCMS.Infrastructure.Data;
using NewsPortalCMS.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NewsPortalCmsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("NewsPortalCMS.Infrastructure")
    )
);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateArticleDtoValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateArticleDtoValidator>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISlugService, SlugService>();
builder.Services.AddSingleton<ArticleBusinessValidator>();
builder.Services.AddSingleton<CategoryBusinessValidator>();
builder.Services.AddSingleton<ArticleStatsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { }, typeof(ArticleProfile), typeof(CategoryProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NewsPortalCmsDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
