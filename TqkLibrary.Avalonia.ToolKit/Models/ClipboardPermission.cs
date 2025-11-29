namespace TqkLibrary.Avalonia.ToolKit.Models
{
    public record struct ClipboardPermission
    {
        public bool? Read { get; init; }
        public bool? Write { get; init; }
    }
}
