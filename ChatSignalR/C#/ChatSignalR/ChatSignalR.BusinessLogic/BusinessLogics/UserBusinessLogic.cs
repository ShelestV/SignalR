using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using ChatSignalR.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.BusinessLogic.BusinessLogics;

public class UserBusinessLogic :
    BusinessLogicBase<User>, 
    IUserBusinessLogic
{
    private readonly IUserRepository userRepository;

    public UserBusinessLogic(IUserRepository repository, ILogger<BusinessLogicBase<User>>? logger = null)
        : base(repository, logger)
    {
        this.userRepository = repository;
    }

    public async Task<OperationResult<User>> GetByNameAsync(string name)
    {
        return await this.userRepository.GetByNameAsync(name);
    }
}
