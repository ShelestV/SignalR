using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Data.Repositories;

public sealed class UserRepository : 
    RepositoryBase<User>, 
    IUserRepository
{
    protected override string ModelName => "user";

    public UserRepository(IConfiguration configuration, ILogger<UserRepository>? logger = null)
        : base(configuration, logger)
    {
    }

    protected override DbSet<User> GetModelSet(ChatDbContext context)
    {
        return context.Users;
    }

    public async Task<OperationResult<User>> UpdateAsync(Guid id, User model)
    {
        return await base.UpdateAsync(id, model, (x, y) => x.Name = y.Name);
    }

    public async Task<OperationResult<User>> GetByNameAsync(string name)
    {
        var result = OperationResult<User>.Create();
        try
        {
            await using var context = this.Context;
            result.Done(context.Users.FirstOrDefault(x => x.Name == name));
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot get user by name: {Name}. Error: {Exception}", name, ex.Message);
            result.Error(ex);
        }
        return result;
    }
}
