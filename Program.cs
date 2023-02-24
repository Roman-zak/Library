using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Library.Data;
using Library.Services;
using Library.Validators;
using FluentValidation;
using Library.Dtos;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IValidator<BookSaveDto>, BookSaveDtoValidator>();
builder.Services.AddScoped<IValidator<ReviewSaveDto>, ReviewSaveDtoValidator>();
builder.Services.AddScoped<IValidator<RatingSaveDto>, RatingSaveDtoValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.Use(async (context, next) =>
{
    // Log request in a simple format
    var request = context.Request;
    var requestBody = string.Empty;

    // Read request body
    if (request.ContentLength != null && request.ContentLength > 0)
    {
        var bodyStream = new MemoryStream();
        await request.Body.CopyToAsync(bodyStream);
        bodyStream.Seek(0, SeekOrigin.Begin);
        requestBody = new StreamReader(bodyStream).ReadToEnd();

        // Replace the request body stream with a new MemoryStream
        bodyStream.Seek(0, SeekOrigin.Begin);
        request.Body = bodyStream;
    }

    // Log request
    string logData = "Method: " + request.Method
    + "\n" + "Path: " + request.Path;
    foreach (var header in request.Headers)
    {
        logData+="\n"+header.Key+":"+ header.Value;
    }
    if (request.QueryString.HasValue)
    {
        logData += "\n" + "QueryString: " + request.QueryString;
    }
    if (requestBody.Length > 0)
    {
        logData +="\nBody: " + requestBody;
    }
    
    Console.WriteLine(logData);

    // Call the next middleware in the pipeline
    await next();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
