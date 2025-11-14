using Avalonia.Threading;
using System.Runtime.Versioning;

namespace TqkLibrary.Avalonia.Utils.Interfaces
{
    public interface IMainThread
    {
        Dispatcher Dispatcher { get; }
    }
}
