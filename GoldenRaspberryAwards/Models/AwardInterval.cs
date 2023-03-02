namespace GoldenRaspberryAwards.Models
{
    public class AwardInterval
    {
        #region Propriedades
        public string Producer { get; set; } = string.Empty;
        public int Interval => this.FollowingWin - this.PreviousWin;
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
        #endregion
    }
}