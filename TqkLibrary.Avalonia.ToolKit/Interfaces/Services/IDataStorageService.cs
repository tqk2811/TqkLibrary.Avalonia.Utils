namespace TqkLibrary.Avalonia.ToolKit.Interfaces.Services
{
    public interface IDataStorageService
    {
        Task<TData?> GetAsync<TData>();
        Task<TData?> GetAsync<TData>(string key);
        Task SetAsync<TData>(TData value);
        Task SetAsync<TData>(string key, TData value);
    }
}
