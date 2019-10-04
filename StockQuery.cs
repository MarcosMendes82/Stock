using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace prjStock.Performance.Models
{
    public class StockQuery
    {

        public readonly AppDb Db;
        public StockQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<StockPost> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT id_cliente, nm_email, pwd_senha, nm_cliente, dt_nascimento, flg_contribuidor FROM tb_clientes WHERE id_cliente = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<StockPost>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            //cmd.CommandText = @"SELECT `Id`, `Email`, `Senha`, `Nome`, `DtNasc`, `Contribuidor` FROM `tb_clientes` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandText = @"SELECT `id_cliente`, `nm_email`, `pwd_senha`, `nm_cliente`, `dt_nascimento`, `flg_contribuidor` FROM `tb_clientes` ORDER BY `id_cliente` DESC LIMIT 10;";
            cmd.CommandText = @"SELECT id_cliente, nm_email, pwd_senha, nm_cliente, dt_nascimento, flg_contribuidor FROM tb_clientes ORDER BY id_cliente DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        /*
        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `StockPost`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }
        */

        private async Task<List<StockPost>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<StockPost>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new StockPost(Db)
                    {
                        
                        Id = await reader.GetFieldValueAsync<int>(0),
                        Email = await reader.GetFieldValueAsync<string>(1),
                        Senha = await reader.GetFieldValueAsync<string>(2),
                        Nome = await reader.GetFieldValueAsync<string>(3),
                        //DtNasc = await reader.GetFieldValueAsync<DataSetDateTime>(4);
                        //DtNasc = await reader.GetFieldValueAsync<DataSetDateTime.Utc.ToString()>(4),
                        DtNasc = await reader.GetFieldValueAsync<DateTime>(4),
                        Contribuidor = await reader.GetFieldValueAsync<SByte>(5)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}