using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
using TqkLibrary.Avalonia.ToolKit.Interfaces.Services;
using TqkLibrary.Avalonia.ToolKit.Models;

namespace TqkLibrary.Avalonia.ToolKit.Browser.Services
{
    static unsafe partial class BrowserClipboardServiceHelper
    {
        [JSImport("writeText", "clipboardHelper")]
        internal static partial Task WriteText(string text);

        [JSImport("readText", "clipboardHelper")]
        internal static partial Task<string?> ReadText();

        [JSImport("checkPermissions", "clipboardHelper")]
        internal static partial Task<string> HasPermissionAsync();

        [JSImport("requestReadPermissions", "clipboardHelper")]
        internal static partial Task<bool> RequestReadPermissionAsync();

        [JSImport("requestWritePermissions", "clipboardHelper")]
        internal static partial Task<bool> RequestWritePermissionAsync();
    }
    public sealed class BrowserClipboardService : IClipboardService
    {
        public async Task<ClipboardPermission> RequestPermissionAsync(ClipboardPermission? request = null)
        {
            if (request is null)
                request = new() { Read = true, Write = true };
            if (request.Value.Write is null && request.Value.Read is null)
                return request.Value;

            bool isCanRead = false;
            bool isCanWrite = false;
            if (request.Value.Read == true)
            {
                isCanWrite = await BrowserClipboardServiceHelper.RequestReadPermissionAsync();
            }
            if (request.Value.Write == true)
            {
                isCanWrite = await BrowserClipboardServiceHelper.RequestWritePermissionAsync();
            }
            return new ClipboardPermission()
            {
                Read = isCanRead,
                Write = isCanWrite
            };
        }

        public async Task<ClipboardPermission> HasPermissionAsync()
        {
            string data = await BrowserClipboardServiceHelper.HasPermissionAsync();
            return JsonConvert.DeserializeObject<ClipboardPermission>(data);
        }

        public Task SetTextAsync(string text) => BrowserClipboardServiceHelper.WriteText(text);
        public Task<string?> GetTextAsync() => BrowserClipboardServiceHelper.ReadText();

    }
}
