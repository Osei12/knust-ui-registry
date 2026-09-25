using Microsoft.JSInterop;

namespace __MYUI_NAMESPACE__;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;

    public event Action? OnThemeChanged;
    public string CurrentTheme { get; private set; } = "dark";

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
        CurrentTheme = await _module.InvokeAsync<string>("getPreferredTheme");
        await ApplyThemeAsync();
    }

    public async Task ToggleThemeAsync()
    {
        CurrentTheme = CurrentTheme == "light" ? "dark" : "light";
        await ApplyThemeAsync();
        await _module!.InvokeVoidAsync("saveTheme", CurrentTheme);
        OnThemeChanged?.Invoke();
    }

    public async Task InitializeScrollSpyAsync()
    {
        if (_module != null)
        {
            await _module.InvokeVoidAsync("initScrollSpy");
        }
    }

    public async Task<IJSObjectReference?> RegisterClickOutsideAsync<T>(DotNetObjectReference<T> dotnetHelper, string elementId, string methodName) where T : class
    {
        if (_module != null)
        {
            return await _module.InvokeAsync<IJSObjectReference>("registerClickOutside", dotnetHelper, elementId, methodName);
        }
        return null;
    }

    public async Task<bool> CopyToClipboardAsync(string text)
    {
        if (_module != null)
        {
            return await _module.InvokeAsync<bool>("copyToClipboard", text);
        }
        return false;
    }

    public async Task ScrollCarouselAsync(string carouselId, string direction)
    {
        if (_module != null)
        {
            Console.WriteLine($"Scrolling carousel {carouselId} in direction {direction} module {_module}");
            await _module.InvokeVoidAsync("scrollCarousel", carouselId, direction);
        }
    }

    private async Task ApplyThemeAsync()
    {
        if (_module != null)
        {
            await _module.InvokeVoidAsync("setTheme", CurrentTheme);
        }
    }
}
