using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }

        public ClienteVo ToClienteVo()
        {
            return new ClienteVo
            {
                Id = this.Id,
                Nome = this.Nome,
                // Mapear outros atributos, se necessário
            };
        }
    }    
}
