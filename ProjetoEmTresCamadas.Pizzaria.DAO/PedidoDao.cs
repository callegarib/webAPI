using Microsoft.Data.Sqlite;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProjetoEmTresCamadas.Pizzaria.DAO
{
    public interface IPedidoDao : IDao<PedidoVo>
    {

    }

    public class PedidoDao : BaseDao<PedidoVo>, IPedidoDao
    {
        private const string TABELA_PEDIDO_NOME = "TB_PEDIDO";

        private const string TABELA_PEDIDO = @$"CREATE TABLE IF NOT EXISTS {TABELA_PEDIDO_NOME}
                (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    NomeCliente VARCHAR(250),
                    SaborPizza VARCHAR(50),
                    HorarioSolicitacao DATETIME,
                    HorarioPreparacao DATETIME,
                    HorarioSaidaEntrega DATETIME,
                    HorarioEntregaFinalizada DATETIME,
                    CONSTRAINT FK_PedidoCliente FOREIGN KEY (NomeCliente)
                    REFERENCES TB_CLIENTE(Nome),
                    CONSTRAINT FK_PedidoPizza FOREIGN KEY (SaborPizza)
                    REFERENCES TB_PIZZA(Sabor)
                )";

        private const string INSERIR_PEDIDO = @$"
                INSERT INTO {TABELA_PEDIDO_NOME} (NomeCliente, SaborPizza, HorarioSolicitacao, HorarioPreparacao, HorarioSaidaEntrega, HorarioEntregaFinalizada)
                VALUES (@NomeCliente, @SaborPizza, @HorarioSolicitacao, @HorarioPreparacao, @HorarioSaidaEntrega, @HorarioEntregaFinalizada)";

        private const string UPDATE_PEDIDO = @$"
            UPDATE {TABELA_PEDIDO_NOME}
            SET
                NomeCliente = @NomeCliente,
                SaborPizza = @SaborPizza,  
                HorarioSolicitacao = @HorarioSolicitacao, 
                HorarioPreparacao = @HorarioPreparacao, 
                HorarioSaidaEntrega = @HorarioSaidaEntrega, 
                HorarioEntregaFinalizada = @HorarioEntregaFinalizada, 
             WHERE
                ID = @Id";

        private const string DELETE_PEDIDO = $@"
            DELETE FROM {TABELA_PEDIDO_NOME} 
            WHERE ID = @ID";

        private const string SELECT_PEDIDO = @$"
            SELECT * FROM {TABELA_PEDIDO_NOME}";

        public PedidoDao() : base(
            TABELA_PEDIDO,
            SELECT_PEDIDO,
            INSERIR_PEDIDO,
            TABELA_PEDIDO_NOME,
            UPDATE_PEDIDO,
            DELETE_PEDIDO)
        { }

        protected override PedidoVo CriarInstancia(SqliteDataReader sqliteDataReader)
        {
            return new PedidoVo
            {
                Id = Convert.ToInt32(sqliteDataReader["Id"]),
                NomeCliente = sqliteDataReader["Nome do Cliente"].ToString(),
                SaborPizza = sqliteDataReader["Sabor da Pizza"].ToString(),
                HorarioSolicitacao = Convert.ToDateTime(sqliteDataReader["Horario de Solicitacao"]),
                HorarioFinalizacaoPreparacao = Convert.ToDateTime(sqliteDataReader["Horario de Finalizacao/Preparacao"]),
                HorarioSaidaEntrega = Convert.ToDateTime(sqliteDataReader["Horario de Saida/Entrega"]),
                HorarioFinalizacaoEntrega = Convert.ToDateTime(sqliteDataReader["Horario Finalizacao/Entrega"])
            };
        }
    }

}
