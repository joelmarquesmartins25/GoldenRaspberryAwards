namespace GoldenRaspberryAwards.Controllers
{
    using GoldenRaspberryAwards.Data;
    using GoldenRaspberryAwards.Models;
    using GoldenRaspberryAwards.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        #region Campos
        /// <summary>
        /// Contexto da aplica��o
        /// </summary>
        private readonly ApiDbContext _context;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova inst�ncia da classe <see cref="ApiDbContext"/>
        /// </summary>
        /// <param name="context">Contexto da aplica��o via inje��o de depend�ncia</param>
        public MoviesController(ApiDbContext context)
        {
            _context = context;
        }
        #endregion

        #region M�todos REST
        /// <summary>
        /// M�todo respons�vel por retornar todos os filmes indicados e vencedores do Golden Raspberry Awards
        /// </summary>
        /// <returns>Uma lista com os filmes indicados e vencedores do Golden Raspberry Awards</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            List<Movie> movies = _context.Movies
                .Include(m => m.Studios)
                .Include(m => m.Producers)
                .OrderBy(m => m.Id)
                .ToList();
            return Ok(movies);
        }

        /// <summary>
        /// M�todo respons�vel por retornar o filme de acordo com o id informado
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <returns>200OK - O objeto com as informa��es do filme solicitado</returns>
        /// <returns>204NoContent - C�digo informando que o filme solicitado n�o foi encontrado</returns>
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById([FromRoute] long id)
        {
            Movie? movie = _context.Movies
                .Include(m => m.Studios)
                .Include(m => m.Producers)
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NoContent();
            }

            return Ok(movie);
        }

        /// <summary>
        /// M�todo respons�vel por inserir um novo filme no banco de dados
        /// </summary>
        /// <param name="movie">O objeto com as informa��es do filme a ser inserido</param>
        /// <returns>201Created - Indicando que o filme foi inserido com sucesso</returns>
        [HttpPost]
        public ActionResult<Movie> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// M�todo respons�vel por atualizar os dados de um filme no banco de dados
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <param name="movie">Objeto com as informa��es atualizadas para a atualiza��o</param>
        /// <returns>204NoContent - C�digo que indica que o filme foi atualizado com sucesso</returns>
        /// <returns>400BadRequest - C�digo que indica que a requisi��o foi feita com informa��es erradas</returns>
        /// <returns>404NotFound - C�digo que indica que o filme n�o foi encontrado no banco de dados</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromRoute] long id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(movie).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// M�todo respons�vel por excluir um filme do banco de dados
        /// </summary>
        /// <param name="id">Identificador do filme a ser exclu�do</param>
        /// <returns>204NoContent - C�digo que indica que o filme foi exclu�do com sucesso</returns>
        /// <returns>404NotFound - C�digo que indica que o filme n�o foi encontrado no banco de dados</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(long id)
        {
            Movie? movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// M�todo respons�vel por obter os produtores com maior e menor intervalo entre pr�mios
        /// </summary>
        /// <returns>200Ok - Os produtores com o maior e menor intervalo entre pr�mios</returns>
        /// <returns>404NotFound - Nenhum filme vencedor encontrado</returns>
        [HttpGet("intervals")]
        public IActionResult GetProducersGreaterLesserInterval()
        {
            // Selecionar os filmes vencedores ordenados por ano
            List<Movie> winnerMovies = _context.Movies
                .Where(m => m.Winner)
                .Include(m => m.Producers)
                .OrderBy(m => m.Year)
                .ToList();
            if (!winnerMovies.Any())
            {
                return NotFound("Nenhum filme vencedor encontrado!");
            }

            return Ok(IntervalService.GetProducersGreaterLesserInterval(winnerMovies));
        }
        #endregion
    }
}