﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public int ClienteId { get; set; }
        public int PizzaId { get; set; }
        public string NomeCliente { get; set; }
        public string SaborPizza { get; set; }
        public DateTime HorarioSolicitacao { get; set; }
        public DateTime HorarioFinalizacaoPreparacao { get; set; }
        public DateTime HorarioSaidaEntrega { get; set; }
        public DateTime HorarioFinalizacaoEntrega { get; set; }
       
    }
}
