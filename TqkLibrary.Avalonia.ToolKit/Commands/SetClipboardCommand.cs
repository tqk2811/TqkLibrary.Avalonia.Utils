using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TqkLibrary.Avalonia.ToolKit.Interfaces.Services;

namespace TqkLibrary.Avalonia.ToolKit.Commands
{
    public class SetClipboardCommand : BaseCommand
    {
        readonly IClipboardService _clipboardService;
        public SetClipboardCommand(
            IClipboardService clipboardService
            )
        {
            this._clipboardService = clipboardService;
        }

        public override async void Execute(object? parameter)
        {
            string? text = parameter as string;
            if (string.IsNullOrWhiteSpace(text))
                text = parameter?.ToString();
            if (string.IsNullOrWhiteSpace(text))
                return;

            using var l = LockButton();
            try
            {
                var Permission = await _clipboardService.HasPermissionAsync();
                if (Permission.Read != true)
                    Permission = await _clipboardService.RequestPermissionAsync(new() { Write = true });
                if (Permission.Read == true)
                {
                    await _clipboardService.SetTextAsync(text!);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(SetClipboardCommand)} {ex.GetType().FullName}: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }
    }
}
