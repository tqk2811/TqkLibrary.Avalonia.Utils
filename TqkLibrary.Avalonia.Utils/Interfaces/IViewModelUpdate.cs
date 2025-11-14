namespace TqkLibrary.Avalonia.Utils.Interfaces
{
    public interface IViewModelUpdate<T> : IViewModel<T>
    {
        void Update(T data);
    }
}