using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.DTOs;
using flamesapi.Entities;
using flamesapi.Helpers;

namespace flamesapi.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetMessage(int id);
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<PagedList<MessageDto>> GetMessagesForUser(string username);
        Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId);
        Task<bool> SaveAllAsync();
    }
}