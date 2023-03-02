namespace GoldenRaspberryAwards.Models
{
    using System.Text.Json.Serialization;

    public class Studio
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova inst�ncia da classe <see cref="Studio"/>
        /// </summary>
        /// <param name="name">Nome do est�dio</param>
        public Studio(string name)
        {
            this.Name = name;
        }
        #endregion

        #region Propriedades
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        #endregion

        #region M�todos
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Studio producer = (Studio)obj;
            return this.Name.Equals(producer.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        #endregion
    }
}