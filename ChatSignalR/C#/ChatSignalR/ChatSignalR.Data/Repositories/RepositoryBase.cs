using ChatSignalR.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Data.Repositories;

public abstract class RepositoryBase<TModel> 
    where TModel : Models.ModelBase
{
    protected readonly ILogger<RepositoryBase<TModel>>? logger;

    protected readonly string connectionString;
    protected ChatDbContext Context => new(this.connectionString);

    protected abstract string ModelName { get; }
    protected abstract Func<ChatDbContext, DbSet<TModel>> GetModelSet { get; }

    public RepositoryBase(IConfiguration configuration, ILogger<RepositoryBase<TModel>>? logger = null)
    {
        this.connectionString = GetConnectionString(configuration);
        this.logger = logger;
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
    }

    public async Task<OperationResult<Guid>> AddAsync(TModel model)
    {
        var result = OperationResult<Guid>.Create();
        try
        {
            await using var context = this.Context;
            model.Id = Guid.NewGuid();
            await this.GetModelSet(context).AddAsync(model);
            await context.SaveChangesAsync();
            result.Done(model.Id);

        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot add {Name}. Error: {Exception}", this.ModelName, ex.Message);
            result.Error(ex);
        }
        return result;
    }

    public async Task<OperationResult<bool>> DeleteAsync(Guid id)
    {
        var result = OperationResult<bool>.Create();
        try
        {
            await using var context = this.Context;
            var model = await this.GetModelSet(context).FindAsync(id);
            if (model is null)
            {
                this.logger?.LogWarning("Cannot find {Name} to delete by id: {Id}", ModelName, id);
                result.NotFound();
                return result;
            }

            this.GetModelSet(context).Remove(model);
            await context.SaveChangesAsync();
            result.Done(true);
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot delete {Name} by id: {Id}. Error: {Exception}", this.ModelName, id, ex.Message);
            result.Error(ex);
        }
        return result;
    }

    public async Task<OperationResult<IList<TModel>>> GetAllAsync()
    {
        var result = OperationResult<IList<TModel>>.Create();
        try
        {
            await using var context = this.Context;
            result.Done(await this.GetModelSet(context).ToListAsync());
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot get {Name}s. Error: {Exception}", this.ModelName, ex.Message);
            result.Error(ex);
        }
        return result;
    }

    public async Task<OperationResult<TModel>> GetAsync(Guid id)
    {
        var result = OperationResult<TModel>.Create();
        try
        {
            await using var context = this.Context;
            result.Done(await this.GetModelSet(context).FindAsync(id));
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot get {Name} by id: {Id}. Error: {Exception}", this.ModelName, id, ex.Message);
            result.Error(ex);
        }
        return result;
    }

    protected async Task<OperationResult<TModel>> UpdateAsync(Guid id, TModel model, Action<TModel, TModel> changeModel)
    {
        var result = OperationResult<TModel>.Create();
        try
        {
            await using var context = this.Context;
            var changingModel = await this.GetModelSet(context).FindAsync(id);
            if (changingModel is null)
            {
                this.logger?.LogWarning("Cannot find {Name} to update by id: {Id}", this.ModelName, id);
                result.NotFound();
                return result;
            }

            changeModel(changingModel, model);
            await context.SaveChangesAsync();
            result.Done(changingModel);
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot update {Name} by id: {Id}. Error: {Exception}", this.ModelName, id, ex.Message);
            result.Error(ex);
        }
        return result;
    }
}
