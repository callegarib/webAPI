using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects
{
    public class PedidoVo : EntidadeBaseVo
    {
        public string NomeCliente { get; set; }
        public string SaborPizza { get; set; }
        public DateTime HorarioSolicitacao { get; set; }
        public DateTime HorarioFinalizacaoPreparacao { get; set; }
        public DateTime HorarioSaidaEntrega { get; set; }
        public DateTime HorarioFinalizacaoEntrega { get; set; }   
      
    }
}
