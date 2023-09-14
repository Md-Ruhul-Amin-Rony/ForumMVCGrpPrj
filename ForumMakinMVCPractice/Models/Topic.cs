using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ForumMakinMVCPractice.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public List<Post>? Posts { get; set; }

    }
}
