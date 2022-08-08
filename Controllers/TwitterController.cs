using CoreWebAPIDapperPractice1.Models;
using CoreWebAPIDapperPractice1.Reposiroty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterDemoAPI.Models;

namespace TwitterDemoAPI.DBContext
{
    [Route("Twitter")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ITweetRepository tweetRepository;
        private readonly ITweetResponseRepository tweetResponseRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public TwitterController(ITweetRepository tweetRepository, ITweetResponseRepository tweetResponseRepository, UserManager<ApplicationUser> userManager)
        {
            this.tweetRepository = tweetRepository;
            this.tweetResponseRepository = tweetResponseRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("{username}/Add")]
        [Authorize()]
        public async Task<IActionResult> Add([FromBody] Tweet model, string username)
        {
            if (ModelState.IsValid)
            {
                var userid = this.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.UserData).Value;

                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound(new Response { Status = "Fail", Message = "User not existing with provided user name" });
                }
                else if (user.Id != userid)
                {
                    return NotFound(new Response { Status = "Fail", Message = "Invalid UserID" });
                }

                model.UserId = user.Id;

                tweetRepository.AddAsync(model);
                return Ok(new Response { Status = "Success", Message = "added successfully!" });
            }
            return BadRequest(new Response { Status = "Error", Message = "failure to add!" });
        }

        [HttpGet]
        [Route("all")]
        [Authorize()]
        public async Task<IActionResult> All()
        {
            var tweets = await tweetRepository.GetAllAsync();
            return Ok(new Response { Status = "Success", Message = "Found", Data = tweets });

        }

        [HttpGet]
        [Route("{username}")]
        [Authorize()]
        public async Task<IActionResult> GetUserTweets(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new Response { Status = "Fail", Message = "User not existing with provided user name" });
            }
            var tweets = await tweetRepository.GetAllByUserIdAsync(user.Id);
            return Ok(new Response { Status = "Success", Message = "Found", Data = tweets });
        }

        [HttpPost]
        [Route("{username}/Update/{id}")]
        [Authorize()]
        public async Task<IActionResult> UpdateTweet([FromBody] Tweet model, string username, int id)
        {
            if (ModelState.IsValid)
            {
                var userid = this.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.UserData).Value;

                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound(new Response { Status = "Fail", Message = "User not existing with provided user name" });
                }
                else if (user.Id != userid)
                {
                    return NotFound(new Response { Status = "Fail", Message = "Invalid UserID" });
                }

                model.UserId = user.Id;

                var tweet = await tweetRepository.GetByIdAsync(id);

                if (tweet == null)
                {
                    return NotFound(new Response { Status = "Fail", Message = "Tweet not found" });
                }
                tweet.Tag = model.Tag;
                tweet.Message = model.Message;
                tweetRepository.UpdateAsync(tweet);
                return Ok(new Response { Status = "Success", Message = "added successfully!" });
            }
            return BadRequest(new Response { Status = "Error", Message = "failure to add!" });
        }


        [HttpDelete]
        [Route("{username}/delete/{id}")]
        [Authorize()]
        public async Task<IActionResult> DeleteTweet(string username, int id)
        {
            var userid = this.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.UserData).Value;

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new Response { Status = "Fail", Message = "User not existing with provided user name" });
            }
            else if (user.Id != userid)
            {
                return NotFound(new Response { Status = "Fail", Message = "Invalid UserID" });
            }

            int deleted = await tweetRepository.DeleteAsync(id);

            if (deleted > 0)
            {
                return Ok(new Response { Status = "Success", Message = "Deleted successfully!" });

            }
            return NotFound(new Response { Status = "Fail", Message = "Tweet not found" });
        }

        [HttpPost]
        [Route("{username}/Like/{id}")]
        [Route("{username}/Reply/{id}")]
        [Authorize()]
        public async Task<IActionResult> Like([FromBody] TweetResponse model, string username, int id)
        {
            if (ModelState.IsValid)
            {
                var userid = this.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.UserData).Value;

                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound(new Response { Status = "Fail", Message = "User not existing with provided user name" });
                }
                else if (user.Id != userid)
                {
                    return NotFound(new Response { Status = "Fail", Message = "Invalid UserID" });
                }

                var tweet = await tweetRepository.GetByIdAsync(id);

                if (tweet == null)
                {
                    return NotFound(new Response { Status = "Fail", Message = "Tweet not found" });
                }

                model.TweetId = tweet.Id;
                model.UserId = user.Id;

                await tweetResponseRepository.AddAsync(model);
                return Ok(new Response { Status = "Success", Message = "added successfully!" });
            }
            return BadRequest(new Response { Status = "Error", Message = "failure to add!" });
        }

    }
}
