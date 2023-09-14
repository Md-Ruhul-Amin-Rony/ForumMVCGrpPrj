using System.ComponentModel.DataAnnotations;

namespace ForumMakinMVCPractice.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
