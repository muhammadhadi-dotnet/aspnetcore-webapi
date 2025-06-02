using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Data;
using WebApi.Data.Interfaces;
using WebApi.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // <-- React's dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
IMapper Imapper=MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(Imapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);

builder.Services.AddScoped<IProduct, ProductService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
//app.UseSwaggerUI(s =>s.SwaggerEndpoint("/openapi/v1.json","Swagger"));
app.UseAuthorization();

app.MapControllers();

app.Run();
