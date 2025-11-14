using System.Collections.Generic;

namespace TqkLibrary.Avalonia.Utils.Interfaces
{
    public interface IMainThreadList<T> : IMainThread, IList<T>, IMainThreadCollection<T>
    {

    }
}
