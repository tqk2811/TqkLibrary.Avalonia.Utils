using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Threading;
using TqkLibrary.Avalonia.ToolKit.Interfaces.Services;
using TqkLibrary.Avalonia.ToolKit.Models;

namespace TqkLibrary.Avalonia.ToolKit.Desktop.Services
{
    public sealed class DesktopClipboardService : IClipboardService
    {
        private IClipboard? Clipboard =>
            (Application.Current?.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime)?
            .MainWindow?.Clipboard;

        public Task<ClipboardPermission> HasPermissionAsync()
        {
            return Task.FromResult(new ClipboardPermission() { Read = Clipboard is not null, Write = Clipboard is not null });
        }

        public Task<ClipboardPermission> RequestPermissionAsync(ClipboardPermission? request = null)
            => HasPermissionAsync();

        public Task SetTextAsync(string text)
            => Dispatcher.UIThread
                .TrueThreadInvokeAsync(async () => await (Clipboard?.SetTextAsync(text) ?? Task.CompletedTask))
                .Unwrap();

        public Task<string?> GetTextAsync()
            => Dispatcher.UIThread
                .TrueThreadInvokeAsync(async () => await (Clipboard?.TryGetTextAsync() ?? Task.FromResult<string?>(null)))
                .Unwrap();
    }
}
