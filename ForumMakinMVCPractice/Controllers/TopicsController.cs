


using ForumMakinMVCPractice.Data;
using ForumMakinMVCPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace ForumMakinMVCPractice.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TopicsController(ApplicationDbContext db)
        {
            _db = db;
        }

       [HttpGet]
        public IActionResult Index()
        {
            var topics = _db.Topics.Select(topic => new TopicViewModel { Topic = topic }).ToList();
            return View(topics);
        }


        [HttpGet] 
        public IActionResult CreateTopic()
        {
            var topicViewModel = new TopicViewModel
            {
                Topic = new Topic() 
            };

            return View(topicViewModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTopic(TopicViewModel topicViewModel)
        {
            if (ModelState.IsValid)
            {
               
                _db.Topics.Add(topicViewModel.Topic);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Topic created successfully";

                return RedirectToAction(nameof(Index));
            }

            
            return View(topicViewModel);
        }


        public IActionResult TopicPosts(int id)
        {
            var topic = _db.Topics
                .Include(t => t.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(c => c.Replies)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
            {
                return NotFound();
            }

            var viewModel = new TopicViewModel
            {
                Topic = topic,
                Post = new Post(),
                Comment = new Comment(),
                Reply = new Reply()
            };

            return View(viewModel);
        }

        public IActionResult CreatePost(int topicId)
        {
            var topic = _db.Topics.FirstOrDefault(t => t.Id == topicId);
            if (topic == null)
            {
                return NotFound();
            }

            var post = new Post { TopicId = topic.Id };
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(Post post)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Add(post);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Post created successfully";
                return RedirectToAction(nameof(TopicPosts), new { id = post.TopicId });
            }
            return View(post);
        }





        public IActionResult CreateComment(int postId)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = new TopicViewModel
            {
                Post = post,
                Comment = new Comment { PostId = post.Id },
               // Topic = post.Topic 
            };

           // var comment = new Comment { PostId = post.Id };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var post = _db.Posts.FirstOrDefault(p => p.Id == comment.PostId);
                if (post == null)
                {
                    return NotFound();
                }

                _db.Comments.Add(comment);

                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Comment created successfully";
                return RedirectToAction(nameof(TopicPosts), new { id = post.TopicId });
            }
            return View(comment);
        }

        public IActionResult CreateReply(int commentId)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var viewModel = new TopicViewModel
            {
                Reply = new Reply { CommentId = comment.Id },
                Comment = comment
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReply(Reply reply)
        {
            if (ModelState.IsValid)
            {
                _db.Replies.Add(new Reply
                {
                    Text = reply.Text,
                    CommentId = reply.CommentId
                });

                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Reply created successfully";

                var post = _db.Posts
                    .Where(p => p.Comments.Any(x => x.Id == reply.CommentId))
                    .FirstOrDefault();

                if (post == null)
                {
                    return NotFound(); 
                }

                return RedirectToAction(nameof(TopicPosts), new { id = post.TopicId });
            }
            return View(reply);
        }
    }
}
