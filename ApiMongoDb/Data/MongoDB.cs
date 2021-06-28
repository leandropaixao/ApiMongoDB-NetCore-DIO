using System;
using ApiMongoDb.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ApiMongoDb.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; set; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);

                DB = client.GetDatabase(configuration["DBName"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException($"Não foi possível conectar{ex}");
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }

    }
}
