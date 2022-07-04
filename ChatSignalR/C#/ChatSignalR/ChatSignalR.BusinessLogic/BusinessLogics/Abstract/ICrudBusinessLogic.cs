namespace ChatSignalR.BusinessLogic;

public interface ICrudBusinessLogic<TModel> : 
    Core.ICrud<TModel>
    where TModel : Data.Models.ModelBase
{
}
