using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.DTOs;
using flamesapi.Extensions;
using flamesapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace flamesapi.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly ILogger<LikesController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;


        public LikesController(ILogger<LikesController> logger, IUserRepository userRepository, ILikeRepository likeRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _likeRepository = likeRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _likeRepository.GetUserWithLikes(sourceUserId);
            if (likedUser == null) return NotFound();
            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself.");
            var userLike = await _likeRepository.GetUserLike(sourceUserId, likedUser.Id);
            if (userLike != null) return BadRequest("You already like this user.");

            userLike = new Entities.UserLike
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            if(await _likeRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var userId = User.GetUserId();
            var users = await _likeRepository.GetUserLikes(predicate, userId);
            return Ok(users);
        }

    }
}