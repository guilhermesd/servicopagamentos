using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Pagamento
    {
        public static readonly string[] STATUS = new string[] { "Aguardando", "Pago", "Cancelado" };

        public Pagamento(Guid idPagamento, Guid idPedido, string txId, string statusPagamento, DateTime dataStatusPagamento)
        {
            if (!STATUS.Contains(statusPagamento))
                throw new ArgumentException("O status está incorreto");

            IdPagamento = idPagamento;
            IdPedido = idPedido;
            TxId = txId;
            StatusPagamento = statusPagamento;
            DataStatusPagamento = dataStatusPagamento;
        }

        // Construtor sem parâmetros necessário para deserialização
        private Pagamento() { }


        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid IdPagamento { get; private set; }

        [BsonRepresentation(BsonType.String)]
        public Guid IdPedido { get; private set; }
        
        public string TxId { get; private set; }

        public string StatusPagamento { get; private set; }

        public DateTime DataStatusPagamento { get; private set; }
    }
}