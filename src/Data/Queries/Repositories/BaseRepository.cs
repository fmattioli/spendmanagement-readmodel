﻿using Domain.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Data.Queries.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> collection;

        public BaseRepository(IMongoDatabase mongoDb, string collectionName)
        {
            MapClasses();
            collection = mongoDb.GetCollection<TEntity>(collectionName);
        }

        public async Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            return await collection.Find(filterExpression).SingleOrDefaultAsync();
        }

        private static void MapClasses()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
            {
                BsonClassMap.RegisterClassMap<TEntity>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
