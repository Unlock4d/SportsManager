using Microsoft.AspNetCore.Mvc;
using ProjetoFinalAtletasTreinadores.Models;

namespace ProjetoFinalAtletasTreinadores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtletaController : Controller
    {
        private static string con = "SERVER=LAPTOP-DL5DG31G\\SQLEXPRESS; Database=SportsManager; uid=userTeste; pwd=passwordComplicada!; TrustServerCertificate=True;";

        [HttpPost]
        public IActionResult Criar(AtletaDto atletaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var atleta = new Atleta(con)
            {
                Nome = atletaDto.Nome,
                Idade = atletaDto.Idade,
                Modalidade = atletaDto.Modalidade,
                emAtividade = atletaDto.emAtividade,
                Descricao = atletaDto.Descricao,
                Peso = atletaDto.Peso,
                Altura = atletaDto.Altura
            };

            if (atleta.Criar() != 0)
                return Ok();
            else return BadRequest("Erro ao criar o atleta!");
        }

        [HttpPut]
        public IActionResult Atualizar(int atletaid, [FromBody] AtletaDto atletaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            
            var atleta = new Atleta(con)
            {
                IdAtleta = atletaid,
                Nome = atletaDto.Nome,
                Idade = atletaDto.Idade,
                Modalidade = atletaDto.Modalidade,
                emAtividade = atletaDto.emAtividade,
                Descricao = atletaDto.Descricao,
                Peso = atletaDto.Peso,
                Altura = atletaDto.Altura
            };
            if (atleta.GetContactIdByAtleteID() == 0)
                return BadRequest("Erro encontrar o idContacto do atleta.");

            if (atleta.Atualizar() != 0)
                return Ok("Atleta atualizado com sucesso.");
            else
                return BadRequest("Erro ao atualizar atleta.");
        }

        [HttpGet]
        public IActionResult Buscar(int atletaid)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var atleta = new Atleta(con);

            if (atleta.Buscar(atletaid) != 0)
                return Ok(atleta);
            else
                return BadRequest("Erro ao encontrar atleta.");
        }
    }
}
