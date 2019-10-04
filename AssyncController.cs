using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prjStock.Performance;
using prjStock.Performance.Models;

namespace prjStock.Performance.Controllers
{
    [Route("api/[controller]")]
    public class AsyncController : Controller
    {

         
        // GET api/async
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                await db.Connection.OpenAsync();
                var query = new StockQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/async/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                await db.Connection.OpenAsync();
                var query = new StockQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/async
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StockPost body)
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/async/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]StockPost body)
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                await db.Connection.OpenAsync();
                var query = new StockQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.Email = body.Email;
                result.Senha = body.Senha;
                result.Nome = body.Nome;
                result.DtNasc = body.DtNasc;
                result.Contribuidor = body.Contribuidor;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/async/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                await db.Connection.OpenAsync();
                var query = new StockQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/async
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb("server=127.0.0.1;user id=root;password=admin123;port=3306;database=mydb;"))
            {
                await db.Connection.OpenAsync();
                var query = new StockQuery(db);
                //await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}   