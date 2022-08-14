using BookLibServ.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordBook.reposit.Interface;

namespace BookLibServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _db;
        private readonly ILogger _logger;

        public BookController(IBookRepository db, ILogger<BookController> log)
        {
            _db = db;
            _logger= log;
        }

        //get
        [AllowAnonymous]
        [HttpGet]
        [Route("get/all-books")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            List<BookView> bookViews = await _db.GetAllAsync();

            if(bookViews == null)
            {
                return BadRequest("0 books in library");
            }

            return Ok(bookViews);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get/tags")]
        public async Task<IActionResult> GetTagsAsync()
        {
            List<TagView> tags = await _db.GetTagsAsync();

            if (tags == null)
            {
                return BadRequest("cant find tags");
            }

            return Ok(tags);
        }

        //post
        [Authorize]
        [HttpPost]
        [Route("create/book")]
        public async Task<IActionResult> createBookAsync([FromBody] BookView bookToCreate)
        {
            BookView? book = await _db.CreateAsync(bookToCreate);
            
            if (book == null)
            {
                return BadRequest("cant create book");
            }

            _logger.LogInformation("Book created by" + User.Identity.Name);

            return Ok(book);
        }

        [Authorize]
        [HttpPost]
        [Route("create/tag")]
        public async Task<IActionResult> createTagAsync([FromBody] TagView tag)
        {
            TagView? response = await _db.CreateTagAsync(tag);

            if (response == null)
            {
                return BadRequest("cant create tag");
            }

            _logger.LogInformation("Tag created by" + User.Identity.Name);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/book/{id}")]
        public async Task<IActionResult> deleteAsync(int id)
        {
            bool response = await _db.DeleteAsync(id);

            if (response == false)
            {
                _logger.LogInformation("error in delete" 
                    + id + " by user: " + User.Identity.Name);

                return BadRequest("Delete error");
            }

            _logger.LogInformation("Book deleted by" + User.Identity.Name);

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [Route("update/book")]
        public async Task<IActionResult> updateAsync([FromBody] BookView bookToUpdate)
        {
            var book = await _db.UpdateAsync(bookToUpdate);

            if (book == null)
            {
                _logger.LogInformation("error in update bookId: " 
                    + bookToUpdate.Id + " by user: " + User.Identity.Name);

                return BadRequest("Error in update");
            }

            _logger.LogInformation("Book deleted by" + User.Identity.Name);

            return Ok(book);
        }
    }
}
