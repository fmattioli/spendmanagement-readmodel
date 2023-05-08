﻿using Crosscutting.Config;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CrossCutting.Extensions
{
    public static class MongoExtension
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, MongoSettings mongoSettings)
        {

            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(mongoSettings.ConnectionString);
            });

            services.AddSingleton(sp =>
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
                var mongoClient = sp.GetService<IMongoClient>() ?? throw new Exception("MongoDB was not injectable.");
                var db = mongoClient.GetDatabase(mongoSettings.Database);
                return db;
            });

            return services;
        }
    }
}
