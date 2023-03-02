namespace GoldenRaspberryAwards.Models
{
    using System.Text.Json.Serialization;

    public class Producer
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="Producer"/>
        /// </summary>
        /// <param name="name">Nome do produtor</param>
        public Producer(string name)
        {
            this.Name = name;
        }
        #endregion

        #region Propriedades
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Métodos
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Producer producer = (Producer)obj;
            return this.Name.Equals(producer.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        #endregion
    }
}