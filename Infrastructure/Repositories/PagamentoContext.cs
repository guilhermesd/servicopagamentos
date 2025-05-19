using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IPagamentoContext
    {
        IMongoCollection<Pagamento> Pagamentos { get; }
    }

    public class PagamentoContext : IPagamentoContext
    {
        private readonly IMongoDatabase _database;

        public PagamentoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<Pagamento> Pagamentos => _database.GetCollection<Pagamento>("Pagamentos");
    }
}