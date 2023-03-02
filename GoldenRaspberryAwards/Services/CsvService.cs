namespace GoldenRaspberryAwards.Services
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;
    using GoldenRaspberryAwards.Data;
    using GoldenRaspberryAwards.Models;

    public class CsvService
    {
        #region Campos
        /// <summary>
        /// Contexto da aplicação
        /// </summary>
        private readonly ApiDbContext _context;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="CsvService"/>
        /// </summary>
        /// <param name="context">Contexto da aplicação via injeção de dependência</param>
        public CsvService(ApiDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Importar os filmes do arquivo CSV e incluí-los no banco de dados em memória
        /// </summary>
        /// <param name="filePath">Caminho do arquivo CSV</param>
        public void ImportMoviesFromCsv(string filePath)
        {
            // Define as configurações do CSV
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            };

            using (StreamReader streamReader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(streamReader, csvConfig))
            {
                // Registrar a classe MovieMap para realizar o mapeamento do CSV para a classe Movie
                csvReader.Context.RegisterClassMap<MovieMap>();
                List<Movie> movies = csvReader.GetRecords<Movie>().ToList();

                // Salvar dados dos filmes indicados
                _context.Movies.AddRange(movies);
                _context.SaveChanges();
            }
        }
        #endregion
    }
}