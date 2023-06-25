using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAnyOrigin", builder =>
//    {
//        builder.AllowAnyOrigin();
//        builder.AllowAnyMethod();
//        builder.AllowAnyHeader();
//    }
//    );
//});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
        });
});

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//app.UseStaticFiles();
//var staticFileOpts = new StaticFileOptions();
app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    //spa.Options.SourcePath = "ClientApp";

    //if (app.Environment.IsDevelopment())
    //{
    //    spa.UseReactDevelopmentServer(npmScript: "start");
    //}
});

app.MapControllers();

app.UseCors();

app.Run();
