﻿using Huddle.Context;
using Huddle.Interfaces;
using Huddle.Models;
using Microsoft.EntityFrameworkCore;

namespace Huddle.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HuddleDBContext _context;

        public UserRepository(HuddleDBContext context)
        {
            _context = context;
        }

        // CREATE NEW USER
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // GET USER BY EMAIL
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // GET USER BY ID
        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        // GET USER FRIEND LIST
        public async Task<List<Friendship>?> GetUserFriendList(Guid id)
        {
            return await _context.Friendships
                    .Where(f => f.UserOneId == id || f.UserTwoId == id)
                    .ToListAsync();
        }
    }
}
