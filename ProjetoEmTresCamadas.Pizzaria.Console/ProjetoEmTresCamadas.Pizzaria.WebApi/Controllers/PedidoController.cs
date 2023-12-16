using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.DAO;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoDao _pedidoDao;

        public PedidoController(IPedidoDao pedidoDao)
        {
            _pedidoDao = pedidoDao;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PedidoVo>> ObterPedidos()
        {
            try
            {
                var pedidos = _pedidoDao.ObterRegistros();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar pedidos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoVo>> ObterPedidoPorId(int id)
        {
            try
            {
                var pedido = await _pedidoDao.ObterRegistro(id);
                if (pedido == null)
                {
                    return NotFound($"Pedido com ID {id} não encontrado");
                }

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar pedido: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PedidoVo>> CriarPedido(PedidoVo pedido)
        {
            try
            {
                var idPedido = await Task.Run(() => _pedidoDao.CriarRegistro(pedido));
                pedido.Id = idPedido;
                return CreatedAtAction(nameof(ObterPedidoPorId), new { id = idPedido }, pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar pedido: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarPedido(int id, PedidoVo pedido)
        {
            try
            {
                if (id != pedido.Id)
                {
                    return BadRequest("IDs do pedido não correspondem");
                }

                await _pedidoDao.AtualizarRegistro(pedido);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar pedido: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarPedido(int id)
        {
            try
            {
                var pedidoExistente = await _pedidoDao.ObterRegistro(id);
                if (pedidoExistente == null)
                {
                    return NotFound($"Pedido com ID {id} não encontrado");
                }

                await _pedidoDao.DeletarRegistro(id); // Certifique-se de que DeletarRegistro seja um método assíncrono
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar pedido: {ex.Message}");
            }
        }
    }
}
