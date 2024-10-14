using JernaClassLib.Data.DatabaseObjects;
using JernaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services
{
    public class UserService
    {
        private readonly IDbContextFactory<JernaContext> _factory;

        public UserService(IDbContextFactory<JernaContext> contextFactory)
        {
            _factory = contextFactory;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var context = await _factory.CreateDbContextAsync();
            return await context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var context = await _factory.CreateDbContextAsync();
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            var context = await _factory.CreateDbContextAsync();
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            var context = await _factory.CreateDbContextAsync();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

    }
}
