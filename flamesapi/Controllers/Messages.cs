using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using flamesapi.DTOs;
using flamesapi.Entities;
using flamesapi.Extensions;
using flamesapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace flamesapi.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MessagesController(ILogger<MessagesController> logger,
         IMessageRepository messageRepository,
         IUserRepository userRepository,
         IMapper mapper)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(MessageCreationDto messageCreationDto)
        {
            var username = User.GetUsername();

            if(username == messageCreationDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself");
            
            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(messageCreationDto.RecipientUsername);
            if (recipient == null) return NotFound("Recipient not found");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = username,
                RecipientUsername = messageCreationDto.RecipientUsername,
                Content = messageCreationDto.Content
            };

            _messageRepository.AddMessage(message);
            if (await _messageRepository.SaveAllAsync()) 
                return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");
        }
    }
}