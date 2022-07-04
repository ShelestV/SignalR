using ChatSignalR.Core;
using ChatSignalR.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.BusinessLogic;

public abstract class BusinessLogicBase<TModel> 
    where TModel : Data.Models.ModelBase
{
    protected readonly ICrudRepository<TModel> repository;
    protected readonly ILogger<BusinessLogicBase<TModel>>? logger;

    public BusinessLogicBase(ICrudRepository<TModel> repository, ILogger<BusinessLogicBase<TModel>>? logger = null)
    {
        this.repository = repository;
        this.logger = logger;
    }

    protected virtual async Task<BeforeResult> BeforeAddAsync(TModel model)
    {
        return await Task.FromResult(new BeforeResult());
    }

    public async Task<OperationResult<Guid>> AddAsync(TModel model)
    {
        var before = await this.BeforeAddAsync(model);
        if (before.Result)
            return await this.repository.AddAsync(model);

        var result = OperationResult<Guid>.Create();
        result.Error(before.Error!);
        return result;
    }

    protected virtual async Task<BeforeResult> BeforeUpdateAsync(Guid id, TModel model)
    {
        return await Task.FromResult(new BeforeResult());
    }

    public async Task<OperationResult<TModel>> UpdateAsync(Guid id, TModel model)
    {
        var before = await this.BeforeUpdateAsync(id, model);
        if (before.Result)
            return await this.repository.UpdateAsync(id, model);

        var result = OperationResult<TModel>.Create();
        result.Error(before.Error!);
        return result;
    }

    public async Task<OperationResult<bool>> DeleteAsync(Guid id)
    {
        return await this.repository.DeleteAsync(id);
    }

    public async Task<OperationResult<TModel>> GetAsync(Guid id)
    {
        return await this.repository.GetAsync(id);
    }

    public async Task<OperationResult<IList<TModel>>> GetAllAsync()
    {
        return await this.repository.GetAllAsync();
    }
}

