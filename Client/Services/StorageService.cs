namespace Client.Services;
using Microsoft.JSInterop;

public class StorageService
{
    private readonly IJSRuntime _js;

    public StorageService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task Save(string key, string value)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", key, value);
    }
}
