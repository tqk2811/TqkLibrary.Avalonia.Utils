using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TqkLibrary.Avalonia.Utils.Interfaces
{
    public interface IRemoteDataReadonly<T>
    {
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<T> GetAsync(T item, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        IAsyncEnumerable<T> GetAllAsync(CancellationToken cancellationToken = default);
    }
}