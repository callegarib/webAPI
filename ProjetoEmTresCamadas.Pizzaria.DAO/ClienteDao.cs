using Microsoft.Data.Sqlite;
using ProjetoEmTresCamadas.Pizzaria.DAO.Regras;
using ProjetoEmTresCamadas.Pizzaria.DAO.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProjetoEmTresCamadas.Pizzaria.DAO
{
    public interface IClienteDao : IDao<ClienteVo>
    {
        // Métodos específicos do ClienteDao, se houver necessidade
    }

    public class ClienteDao : BaseDao<ClienteVo>, IClienteDao
    {
        private const string TABELA_CLIENTE_NOME = "TB_CLIENTE";

        private const string TABELA_CLIENTE = @$"CREATE TABLE IF NOT EXISTS {TABELA_CLIENTE_NOME}
                (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome VARCHAR(50) not null                    
                )";

        private const string INSERIR_CLIENTE = @$"
                INSERT INTO {TABELA_CLIENTE_NOME} (Nome)
                VALUES (@Nome)";

        private const string UPDATE_CLIENTE = @$"
            UPDATE {TABELA_CLIENTE_NOME}
            SET
                Nome = @Nome                
            WHERE
                ID = @Id";

        private const string DELETE_CLIENTE = $@"
            DELETE FROM {TABELA_CLIENTE_NOME} 
            WHERE ID = @ID";

        private const string SELECT_CLIENTE = @$"SELECT * FROM {TABELA_CLIENTE_NOME}";

        public ClienteDao() : base(
            TABELA_CLIENTE,
            SELECT_CLIENTE,
            INSERIR_CLIENTE,
            TABELA_CLIENTE_NOME,
            UPDATE_CLIENTE,
            DELETE_CLIENTE)
        { }

        protected override ClienteVo CriarInstancia(SqliteDataReader sqliteDataReader)
        {
            return new ClienteVo
            {
                Id = Convert.ToInt32(sqliteDataReader["Id"]),
                Nome = sqliteDataReader["Nome"].ToString(),
                // Outros campos do cliente...
            };
        }
    }
}