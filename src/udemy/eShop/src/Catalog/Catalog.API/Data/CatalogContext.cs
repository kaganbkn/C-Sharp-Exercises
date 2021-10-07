using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data.Interfaces;
using Catalog.API.Models;
using Catalog.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            var settings = catalogDatabaseSettings?.Value ?? new CatalogDatabaseSettings();

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            SeedData(Products);
        }

        private static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(c=>true).Any(); //any rows in db


        }


        public IMongoCollection<Product> Products { get; }
    }
}
