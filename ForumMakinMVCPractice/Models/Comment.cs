using System.ComponentModel.DataAnnotations;

namespace ForumMakinMVCPractice.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
        public List<Reply>? Replies { get; set; }
    }
}
