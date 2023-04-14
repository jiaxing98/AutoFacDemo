using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoFacDemo.Data;
using AutoFacDemo.Helpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// guid to string serializer
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

// get mongodb settings from appsettings.json
var mongodbSettings = builder.Configuration.GetSection(nameof(MongoDBSettings));
builder.Services.Configure<MongoDBSettings>(mongodbSettings);

// autofac container
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
    container.RegisterModule(new AutoFacModule(builder.Configuration, builder.Services)));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
