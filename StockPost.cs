using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;

namespace prjStock.Performance.Models
{
    public class StockPost
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DtNasc { get; set; }
        public SByte Contribuidor { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public StockPost(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `tb_clientes` (null, `Email`, `Senha`, `Nome`, `DtNasc`, `Contribuidor`) VALUES (@email, @senha, @nome, @dtnasc, @contribuidor);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `tb_clientes` SET `Email` = @mail, `Senha` = @senha, `Nome` = @nome, `DtNasc` = @dtnasc, `Contribuidor` = @contribuidor WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `tb_clientes` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@mail",
                DbType = DbType.String,
                Value = Email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@senha",
                DbType = DbType.String,
                Value = Senha,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@nome",
                DbType = DbType.String,
                Value = Nome,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dtnasc",
                DbType = DbType.DateTime,
                Value = DtNasc,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@contribuidor",
                DbType = DbType.SByte,
                Value = Contribuidor,
            });
        }

    }
}