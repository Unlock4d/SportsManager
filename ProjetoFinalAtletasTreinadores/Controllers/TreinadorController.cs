using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoFinalAtletasTreinadores.Models;

namespace ProjetoFinalAtletasTreinadores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreinadorController : Controller
    {
        private IConfiguration configuration;
        private string con = string.Empty;
        public TreinadorController(IConfiguration config)
        {
            configuration = config;
            con = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpPost]
        public IActionResult Criar(TreinadorDto treinadorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");

            if (!Enum.IsDefined((NivelExperiencia)treinadorDto.Nivel_de_certificao))
                return BadRequest("Nivel de experiencia não existe!");

            if(!Enum.IsDefined((StatusAtividade)treinadorDto.emAtividade))
                return BadRequest("Status de atividade não existe!");

            var treinador = new Treinador(con)
            {
                Nome = treinadorDto.Nome,
                Idade = treinadorDto.Idade,
                Modalidade = treinadorDto.Modalidade,
                emAtividade = treinadorDto.emAtividade,
                Descricao = treinadorDto.Descricao,
                Anos_de_experiencia = treinadorDto.Anos_de_experiencia,
                Nivel_de_certificao = treinadorDto.Nivel_de_certificao
            };

            if (treinador.Criar() != 0)
                return Ok();
            else return BadRequest("Erro ao criar o treinador!");
        }

        [HttpPut]
        public IActionResult Atualizar(int atletaid, [FromBody] TreinadorDto treinadorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");

            if (Enum.IsDefined((NivelExperiencia)treinadorDto.Nivel_de_certificao))
                return BadRequest("Nivel de experiencia não existe!");

            if (!Enum.IsDefined((StatusAtividade)treinadorDto.emAtividade))
                return BadRequest("Status de atividade não existe!");

            var treinador = new Treinador(con)
            {
                IdTreinador = atletaid,
                Nome = treinadorDto.Nome,
                Idade = treinadorDto.Idade,
                Modalidade = treinadorDto.Modalidade,
                emAtividade = treinadorDto.emAtividade,
                Descricao = treinadorDto.Descricao,
                Anos_de_experiencia = treinadorDto.Anos_de_experiencia,
                Nivel_de_certificao = treinadorDto.Nivel_de_certificao
            };
            if (treinador.GetContactIdByAtleteID() == 0)
                return BadRequest("Erro a encontrar o idContacto do treinador!");

            if (treinador.Atualizar() != 0)
                return Ok("Treinador atualizado com sucesso.");
            else
                return BadRequest("Erro ao atualizar treinador!");
        }

        [HttpGet]
        public IActionResult Buscar(int treinadorid)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");

            var treinador = new Treinador(con);

            if (treinador.Buscar(treinadorid) != 0)
                return Ok(treinador);
            else
                return BadRequest("Erro ao encontrar treinador!");
        }

        [HttpGet("niveis")]
        public IActionResult ObterNiveis()
        {
            var niveis = Enum.GetValues(typeof(NivelExperiencia))
                             .Cast<NivelExperiencia>()
                             .Select(n => new
                             {
                                 Nome = n.ToString(),
                                 Valor = (int)n
                             });

            return Ok(niveis);
        }
    }
}
