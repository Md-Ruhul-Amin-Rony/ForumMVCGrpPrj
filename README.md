# Anonymt forum
Our thinking was to build one teamproject where we will build a Forum.In this forum User may able to create their own topics (Ex. School, Sport, File, Enviornment). Under this topics user may able to create own posts under above the subject. Under the posts users may able to start their own conversation with comments and replies. 

## Structure of Project:
|   Tasks     |   Framwork    |  Effect  |
|-----|--------|-------|
|C#  |  ASP.Net   | wwwroot
|MVC |  Model, View, Controller   | Fetching Data
|Model |  Classes   | Fetching Data
|View |  Partial View, View Model,   | Fetching Data
|Controller |  HTTP Post, HTTP Create,  | Fetching Data
|EF |  Entity Framework   | 
|Database |   SQL   | LocalDatabase
|Connection |  JSON   |  Global Datbase

## Key features
|Feature     |Status    |
|-----|:--------:|
|School, Sports, Film, AdditionalTopice |:white_check_mark:     |
|Thread, Comment, Reply | :white_check_mark:    |
|ViewModel, PartialView|:white_check_mark:     |

## Extra Features
  - Home Page: Create new Topics.
  - Topics Page: Allows you to create a new topic.

  - ##### Client-Side Validation: Validates form data on the client side for a better user experience.
  - ##### Server-Side Validation: Ensures data integrity and security.
  - ##### TempData: Displays success and error messages.
  - ##### Partial View: Utilizes a partial view to keep code DRY (Don't Repeat Yourself).
  - ##### Toastr Alerts: Provides user-friendly alert messages.

## Sample of Code Structure - CommentsController - HTTPPost
 [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCommentViewModel createCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    Title = createCommentViewModel.Title,
                    Text = createCommentViewModel.Text,
                    ThreadOneId = createCommentViewModel.ThreadOneId,
                    Created = DateTime.Now,
                };
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Topics", new { id = comment.ThreadOneId });
            }
            ViewData["ThreadOneId"] = new SelectList(_context.ThreadOnes, "Id", "Id", createCommentViewModel.ThreadOneId);
            return View(createCommentViewModel);
        }

## Team
- [Md Ruhul Amin](https://github.com/Md-Ruhul-Amin-Rony)
- [Reza](https://github.com/Rezaeskandar)
- [Tasmia Zahin](https://github.com/tasmiazahin)
- [Md. Kamrul Hasan](https://github.com/chasmkhasan)

