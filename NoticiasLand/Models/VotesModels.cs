namespace NoticiasLand.Models
{
    public class VotesModels
    {
        public int Id { get; set; }

        public bool Vote { get; set; }

        public int IdNews { get; set; }

        public string IdUser { get; set; }

        public string UserName { get; set; }

        public NewsModels News { get; set; }

    }
}