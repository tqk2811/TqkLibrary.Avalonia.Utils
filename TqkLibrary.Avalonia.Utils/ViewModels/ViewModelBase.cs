using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;

namespace TqkLibrary.Avalonia.Utils.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo
        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName != null && _errors.TryGetValue(propertyName, out var list)
                ? list
                : Enumerable.Empty<string>();
        }
        #endregion

        private readonly Dictionary<string, List<string>> _errors = new();
        protected void AddError(string prop, string error)
        {
            _errors[prop] = new List<string>() { error };
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(prop));
        }
        protected void AddError(string prop, IEnumerable<string> errors)
        {
            _errors[prop] = [.. errors];
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(prop));
        }
        protected void ClearErrors(string prop)
        {
            if (_errors.Remove(prop))
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(prop));
        }
        protected void ClearErrors()
        {
            var keys = _errors.Keys.ToList();
            _errors.Clear();
            foreach (var key in keys)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(key));
            }
        }
    }
}
