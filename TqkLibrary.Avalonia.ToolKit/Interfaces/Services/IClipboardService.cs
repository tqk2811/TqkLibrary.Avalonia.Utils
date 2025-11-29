using TqkLibrary.Avalonia.ToolKit.Models;

namespace TqkLibrary.Avalonia.ToolKit.Interfaces.Services
{
    public interface IClipboardService
    {
        Task<ClipboardPermission> RequestPermissionAsync(ClipboardPermission? request = null);
        Task<ClipboardPermission> HasPermissionAsync();

        Task SetTextAsync(string text);
        Task<string?> GetTextAsync();
    }
}
