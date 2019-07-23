namespace NoticiasLand.Models
{
    public class CommentsModel
    {

        public int Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public string IdUser { get; set; }

        public int NewsId { get; set; }

    }
}