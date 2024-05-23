using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using System.Text;
using PatientBackend1.utils;
using Microsoft.Extensions.DependencyInjection;
using PatientBackend1.Services.PatientServices;
using patientBackend1.Services.UserServices;
using patientBackend1.Services.FileServices;
using patientBackend1.Services;
using patientBackend1.Services.MedicalServices;


var builder = WebApplication.CreateBuilder(args);
var DBConfig = builder.Configuration.GetSection("DB");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientService, PatientServices>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IMedRecServices,MedRecServices>();
//builder.Services.AddScoped<ChapaApiHelper>();
   

//Custom Services
//builder.Services.AddScoped<IFileService, FileService>();


// automapper configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);



//Database Configuration

builder.Services.Configure<MongoDBSettings>(DBConfig);

var config = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();
app.MapControllers();
app.Run();


