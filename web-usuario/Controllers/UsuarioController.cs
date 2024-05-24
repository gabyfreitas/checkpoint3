using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;
using web_usuario.Entidade;

namespace web_usuario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string? _connectionString;

        public UsuarioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        private IDbConnection OpenConnection()
        {
            IDbConnection dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            using IDbConnection dbConnection = OpenConnection();
            string sql = "select id, nome, senha from Usuario;";
            var result = await dbConnection.QueryAsync<Usuario>(sql);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            using IDbConnection dbConnection = OpenConnection();
            string sql = "select id, nome, senha from Usuario where id = @id";
            var usuario = await dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { id });
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] Usuario usuario)
        {
            using IDbConnection dbConnection = OpenConnection();
            string query = @"INSERT into Usuario (nome, senha) VALUES (@Nome, @Senha);";
            await dbConnection.ExecuteAsync(query, usuario);
            dbConnection.Close();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUsuario([FromBody] Usuario usuario)
        {

            using IDbConnection dbConnection = OpenConnection();
            var query = @"UPDATE Usuario SET 
                          Nome = @Nome,
                          Senha = @Senha";
            dbConnection.Execute(query, usuario);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            using IDbConnection dbConnection = OpenConnection();
            var produto = await dbConnection.QueryAsync<Usuario>("delete from usuario where id = @id;", new { id });
            return Ok();
        }
    }
}
