using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Student.API.DataModels;
using Student.API.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
	builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
builder.Services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer("server=CAPRAZ\\SQLEXPRESS;database=StudentDb;Trusted_Connection=true"));
builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
	RequestPath = "/Resources"
});


app.UseRouting();

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
