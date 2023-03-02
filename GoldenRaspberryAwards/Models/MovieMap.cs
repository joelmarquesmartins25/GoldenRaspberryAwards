namespace GoldenRaspberryAwards.Models
{
    using CsvHelper.Configuration;

    public class MovieMap : ClassMap<Movie>
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="MovieMap"/>
        /// </summary>
        public MovieMap()
        {
            // Ignorar a coluna "Id"
            Map(m => m.Id).Ignore();

            // Mapeamento do ano e do título do filme
            Map(m => m.Year).Name("year");
            Map(m => m.Title).Name("title");

            // Mapeamento dos estúdios
            Map(m => m.Studios).Convert(record =>
            {
                return (record.Row.GetField<string>("studios") ?? string.Empty)
                    .Split(new[] { ",", " and " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(name => new Studio(name.Trim()))
                    .ToList();
            });

            // Mapeamento dos produtores
            Map(m => m.Producers).Convert(record =>
            {
                return (record.Row.GetField<string>("producers") ?? string.Empty)
                    .Split(new[] { ",", " and " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(name => new Producer(name.Trim()))
                    .ToList();
            });

            // Mapeamento da propriedade que indica se foi vencedor
            Map(m => m.Winner).Name("winner")
                .TypeConverterOption.BooleanValues(true, true, "Yes", "Y")
                .TypeConverterOption.BooleanValues(false, true, "No", "N", string.Empty);
        }
        #endregion
    }
}