using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flamesapi.DTOs
{
    public class MessageCreationDto
    {
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
    }
}