using Application.UseCases;
using Crosscutting.DTOs;
using Crosscutting.DTOs.MercadoPago;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/pagamentos")]
    public class PagamentoController : ControllerBase
    {
        private readonly IGerarPagamentoUseCase _gerarPagamentoUseCase;
        private readonly IAtualizarPagamentoUseCase _atualizarPagamentoUseCase;

        public PagamentoController(IGerarPagamentoUseCase gerarPagamentoUseCase, IAtualizarPagamentoUseCase atualizarPagamentoUseCase)
        {
            _gerarPagamentoUseCase = gerarPagamentoUseCase;
            _atualizarPagamentoUseCase = atualizarPagamentoUseCase;
        }

        /// <summary>
        /// Gera o pagamento e retorna o QrCode
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GerarPagamento([FromBody] PagamentoRequest gerarPedidoDTO)
        {
            var pagamentoResponse = await _gerarPagamentoUseCase.ExecutarAsync(gerarPedidoDTO);
            return Created(string.Empty, pagamentoResponse);
        }

        /// <summary>
        ///  Mercado pago recebimento de notificação webhook.
        ///  https://www.mercadopago.com.br/developers/pt/docs/your-integrations/notifications/webhooks#editor_13
        /// </summary>
        [HttpPost("webhook/notificacoes")]
        public async Task<IActionResult> WebhookNotificacoes([FromBody] MercadoPagoWebhookModel mercadoPagoWebhook)
        {
            await _atualizarPagamentoUseCase.ExecutarAsync(mercadoPagoWebhook);
            return Ok();
        }
    }
}