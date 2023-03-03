namespace GoldenRaspberryAwards.Services
{
    using GoldenRaspberryAwards.Models;

    public class IntervalService
    {
        #region Métodos Estáticos
        public static AwardIntervalResponse GetProducersGreaterLesserInterval(List<Movie> winnerMovies)
        {
            // Lista de intervalos
            List<AwardInterval> intervals = new List<AwardInterval>();

            // Obter os produtores dos filmes vencedores
            List<Producer> producers = winnerMovies.SelectMany(mov => mov.Producers).Distinct().ToList();
            foreach (Producer producer in producers)
            {
                // Obter os filmes vencedores do produtor
                List<Movie> producerWinnerMovies = winnerMovies.Where(x => x.Producers.Contains(producer)).ToList();
                if (producerWinnerMovies.Count < 2)
                {
                    // Não obteve dois prêmios
                    continue;
                }

                // Montar a lista para o cálculo
                for (int count = 0; count < producerWinnerMovies.Count - 1; count++)
                {
                    intervals.Add(
                        new AwardInterval()
                        {
                            Producer = producer.Name,
                            PreviousWin = producerWinnerMovies.ElementAt(count).Year,
                            FollowingWin = producerWinnerMovies.ElementAt(count + 1).Year
                        });
                }
            }

            // Nenhum produtor obteve dois prêmios
            if (!intervals.Any())
            {
                return new AwardIntervalResponse();
            }

            // Obter os intervalos mínimo e máximo
            int min = intervals.Min(x => x.Interval);
            int max = intervals.Max(x => x.Interval);

            // Retornar os produtores com menor e maior intervalo entre dois prêmios
            return
                new AwardIntervalResponse()
                {
                    Min = intervals.Where(x => x.Interval == min).ToList(),
                    Max = intervals.Where(x => x.Interval == max).ToList()
                };
        }
        #endregion
    }
}