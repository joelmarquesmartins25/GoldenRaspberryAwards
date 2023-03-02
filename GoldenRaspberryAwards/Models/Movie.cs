namespace GoldenRaspberryAwards.Models
{
    public class Movie
    {
        #region Propriedades
        public long Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Studio> Studios { get; set; } = new List<Studio>();
        public List<Producer> Producers { get; set; } = new List<Producer>();
        public bool Winner { get; set; }
        #endregion
    }
}