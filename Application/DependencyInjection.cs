using Amazon.S3;
using Application.Common.Behaviours;
using Application.Services.Abstracts;
using Application.Services.Concrets;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        ConfigureValidators(services);
        ConfigureServices(services);
        return services;
    }

    private static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();

            var endpoint = config["Minio:BaseUrl"];
            var accessKey = config["Minio:AccessKey"];
            var secretKey = config["Minio:SecretKey"];

            var s3Config = new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true
            };

            return new AmazonS3Client(accessKey, secretKey, s3Config);
        });

        services.AddScoped<IStorageService, MinIoStorageService>();
    }
}
