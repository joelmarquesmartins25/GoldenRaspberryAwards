namespace GoldenRaspberryAwards.Models
{
    public class AwardIntervalResponse
    {
        #region Propriedades
        public List<AwardInterval> Min { get; set; } = new List<AwardInterval>();

        public List<AwardInterval> Max { get; set; } = new List<AwardInterval>();
        #endregion
    }
}