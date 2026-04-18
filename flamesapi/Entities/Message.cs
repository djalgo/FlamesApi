using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flamesapi.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now; 
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }

        public AppUser Sender { get; set; }
        public AppUser Recipient { get; set; }
    }
}