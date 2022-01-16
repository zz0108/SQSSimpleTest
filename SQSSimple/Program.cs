using Amazon.SQS;
using Microsoft.OpenApi.Models;
using SQSSimple.Business.Helper;
using SQSSimple.Business.Service;
using SQSSimple.Domain.Class;
using SQSSimple.Domain.Helper;
using SQSSimple.Domain.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var appSettingsSection = builder.Configuration.GetSection("ServiceConfiguration");
builder.Services.AddAWSService<IAmazonSQS>();  
builder.Services.Configure<ServiceConfiguration>(appSettingsSection);  
builder.Services.AddTransient<IAWSSQSService, AWSSQSService>();  
builder.Services.AddTransient<IAWSSQSHelper, AWSSQSHelper>();
builder.Services.AddSwaggerGen(a =>
{
    a.SwaggerDoc("v1",new OpenApiInfo(){Title = "My SimpleSQS API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowSpecificOrigins,builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My SimpleSQS API V1");
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();