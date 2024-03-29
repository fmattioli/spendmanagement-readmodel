﻿using Data.Queries.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Extensions.Mongo
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
