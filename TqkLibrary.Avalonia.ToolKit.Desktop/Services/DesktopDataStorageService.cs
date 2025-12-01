
using Newtonsoft.Json;
using TqkLibrary.Avalonia.ToolKit.Interfaces.Services;
using TqkLibrary.Utils;

public class DesktopDataStorageService : IDataStorageService
{
    readonly string _dir;
    readonly JsonSerializerSettings? _serializerSettings;
    public bool IsFullTypeName { get; set; } = false;
    public DesktopDataStorageService(string dir, JsonSerializerSettings? serializerSettings = null)
    {
        this._dir = dir;
        Directory.CreateDirectory(_dir);
        _serializerSettings = serializerSettings;
    }

    public virtual Task<TData?> GetAsync<TData>()
        => GetAsync<TData>(typeof(TData).GetNameFixed(IsFullTypeName) + ".json");

    public virtual async Task<TData?> GetAsync<TData>(string key)
    {
        if (!key.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            key += ".json";
        string? json = await ReadFileKeyAsync(key);
        if (string.IsNullOrWhiteSpace(json)) return default!;
        return JsonConvert.DeserializeObject<TData>(json!, _serializerSettings);
    }
    public virtual Task SetAsync<TData>(TData value) => SetAsync<TData>(typeof(TData).GetNameFixed(IsFullTypeName) + ".json", value);



    static readonly Dictionary<string, string> _cache = new();
    public virtual async Task SetAsync<TData>(string key, TData value)
    {
        if (!key.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            key += ".json";
        string value_str = JsonConvert.SerializeObject(value, _serializerSettings);
        lock (_cache) _cache[key] = value_str;
        string filePath = Path.Combine(_dir, key);
        using StreamWriter fileStream = new StreamWriter(filePath, false);
        await fileStream.WriteAsync(value_str);
    }

    async Task<string?> ReadFileKeyAsync(string name)
    {
        lock (_cache)
        {
            if (_cache.ContainsKey(name)) return _cache[name];
        }
        var file = Directory.GetFiles(_dir).Select(x => new FileInfo(x)).FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (file?.Exists == true)
        {
#if NETSTANDARD
            string value = File.ReadAllText(file.FullName);
#else
            string value = await File.ReadAllTextAsync(file.FullName);
#endif
            lock (_cache) _cache[name] = value;
            return _cache[name];
        }
        return null;
    }
}