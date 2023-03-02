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
        /// Contexto da aplicação
        /// </summary>
        private readonly ApiDbContext _context;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="ApiDbContext"/>
        /// </summary>
        /// <param name="context">Contexto da aplicação via injeção de dependência</param>
        public MoviesController(ApiDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos REST
        /// <summary>
        /// Método responsável por retornar todos os filmes indicados e vencedores do Golden Raspberry Awards
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
        /// Método responsável por retornar o filme de acordo com o id informado
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <returns>200OK - O objeto com as informações do filme solicitado</returns>
        /// <returns>204NoContent - Código informando que o filme solicitado não foi encontrado</returns>
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
        /// Método responsável por inserir um novo filme no banco de dados
        /// </summary>
        /// <param name="movie">O objeto com as informações do filme a ser inserido</param>
        /// <returns>201Created - Indicando que o filme foi inserido com sucesso</returns>
        [HttpPost]
        public ActionResult<Movie> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Método responsável por atualizar os dados de um filme no banco de dados
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <param name="movie">Objeto com as informações atualizadas para a atualização</param>
        /// <returns>204NoContent - Código que indica que o filme foi atualizado com sucesso</returns>
        /// <returns>400BadRequest - Código que indica que a requisição foi feita com informações erradas</returns>
        /// <returns>404NotFound - Código que indica que o filme não foi encontrado no banco de dados</returns>
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
        /// Método responsável por excluir um filme do banco de dados
        /// </summary>
        /// <param name="id">Identificador do filme a ser excluído</param>
        /// <returns>204NoContent - Código que indica que o filme foi excluído com sucesso</returns>
        /// <returns>404NotFound - Código que indica que o filme não foi encontrado no banco de dados</returns>
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
        /// Método responsável por obter os produtores com maior e menor intervalo entre prêmios
        /// </summary>
        /// <returns>200Ok - Os produtores com o maior e menor intervalo entre prêmios</returns>
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