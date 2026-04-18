using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.DTOs;
using flamesapi.Entities;

namespace flamesapi.Interfaces
{
    public interface ILikeRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
        Task<bool> SaveAllAsync();
    }
}