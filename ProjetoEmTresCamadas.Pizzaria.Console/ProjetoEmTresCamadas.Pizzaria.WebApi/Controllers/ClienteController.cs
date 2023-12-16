using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

namespace ProjetoEmTresCamadas.Pizzaria.WebApi.Controllers
 {
        [Route("api/[controller]")]
        [ApiController]
        public class ClienteController : ControllerBase
        {
            private readonly ClienteService _clienteService;

            public ClienteController(ClienteService clienteService)
            {
                _clienteService = clienteService;
            }

            [HttpGet]
            public Cliente[] GetClientes()
            {
                List<Cliente> clientes = _clienteService.ObterTodos();

                return clientes.ToArray();
            }

            [HttpPost]
            public Cliente CriarCliente(Cliente cliente)
            {
                cliente = _clienteService.Adicionar(cliente);
                return cliente;
            }

            [HttpPut]
            public async Task<Cliente> AtualizarCliente(Cliente cliente)
            {
                return await _clienteService.AtualizarAsync(cliente);
            }

            [HttpDelete("{id}")]
            public async Task DeleteCliente(int id)
            {
                await _clienteService.Deletar(id);
            }
            
        }
 }
