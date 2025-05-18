using Domain.Entities;
using FluentAssertions;

namespace Tests.Unidade
{
    public class PagamentoTests
    {
        [Fact]
        public void Construtor_Deve_Criar_Pagamento_Valido()
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idPedido = Guid.NewGuid();
            var txId = "123456789";
            var status = "Pago";
            var dataStatus = DateTime.UtcNow;

            // Act
            var pagamento = new Pagamento(idPagamento, idPedido, txId, status, dataStatus);

            // Assert
            pagamento.IdPagamento.Should().Be(idPagamento);
            pagamento.IdPedido.Should().Be(idPedido);
            pagamento.TxId.Should().Be(txId);
            pagamento.StatusPagamento.Should().Be(status);
            pagamento.DataStatusPagamento.Should().BeCloseTo(dataStatus, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData("Invalido")]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_Deve_Lancar_Excecao_Para_Status_Invalido(string statusInvalido)
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idPedido = Guid.NewGuid();
            var txId = "123456789";
            var dataStatus = DateTime.UtcNow;

            // Act
            Action acao = () => new Pagamento(idPagamento, idPedido, txId, statusInvalido, dataStatus);

            // Assert
            acao.Should().Throw<ArgumentException>()
                .WithMessage("O status está incorreto");
        }
    }
}
