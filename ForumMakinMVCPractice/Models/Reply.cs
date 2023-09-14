using System.ComponentModel.DataAnnotations;

namespace ForumMakinMVCPractice.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }
        public int CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
