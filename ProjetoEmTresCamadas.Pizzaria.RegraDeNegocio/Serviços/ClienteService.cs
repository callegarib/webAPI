using ProjetoEmTresCamadas.Pizzaria.DAO;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;
using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Regras;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Serviços;

public class ClienteService :
    IAdicionar<Cliente>,
    IObter<Cliente>,
    IAtualizar<Cliente>,
    IDeletar<Cliente>
{
    private readonly IClienteDao ClienteDao; // Alterado para o tipo de interface

    public ClienteService(IClienteDao clienteDao)
    {
        ClienteDao = clienteDao;
    }

    public Cliente Adicionar(Cliente objeto)
    {
        ClienteVo clienteVo = objeto.ToClienteVo();
        objeto.Id = ClienteDao.CriarRegistro(clienteVo);
        //RegistrarAcao("Cliente adicionado: " + objeto.Nome); // Exemplo de registro de ação.
        return objeto;
    }

    public List<Cliente> ObterTodos()
    {
        List<Cliente> clientes = new List<Cliente>();
        List<ClienteVo> clientesBanco = ClienteDao.ObterRegistros();

        foreach (ClienteVo clienteVo in clientesBanco)
        {
            Cliente cliente = new Cliente()
            {
                Nome = clienteVo.Nome,
                // Atributos restantes do cliente...
                Id = clienteVo.Id,
            };
            clientes.Add(cliente);
        }
        return clientes;
    }

    public async Task<Cliente> AtualizarAsync(Cliente objeto)
    {
        ClienteVo clienteVo = objeto.ToClienteVo();
        await ClienteDao.AtualizarRegistro(clienteVo);
        //RegistrarAcao("Cliente atualizado: " + objeto.Nome); // Exemplo de registro de ação.
        return objeto;
    }

    public async Task Deletar(int ID)
    {
        var cliente = ObterPorId(ID);
        if (cliente != null)
        {
            await ClienteDao.DeletarRegistro(ID);
            //RegistrarAcao("Cliente deletado: " + cliente.Nome); // Exemplo de registro de ação.
        }
    }

    public Cliente ObterPorId(int id)
    {
        // Lógica para obter cliente por ID, se necessário.
        // Retorna o cliente encontrado ou null se não encontrado.
        return ObterTodos().FirstOrDefault(c => c.Id == id);
    }

    //public List<Acao> ObterHistoricoAcoesCliente(int id)
    //{
    //    // Lógica para obter histórico de ações do cliente por ID, se necessário.
    //    // Retorna o histórico de ações específico do cliente.
    //    return null; // Implemente a lógica de histórico de ações conforme a sua estrutura.
    //}

    //private void RegistrarAcao(string acao)
    //{
    //    // Lógica para registrar ações do cliente, como histórico.
    //    // Você pode implementar a lógica para gravar a ação em um local específico, por exemplo, um registro de histórico no banco de dados.
    //}
}
