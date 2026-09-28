using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__;

public partial class Dropdown : ComponentBase, IAsyncDisposable
{
    [Inject] private ThemeService ThemeService { get; set; } = default!;

    [Parameter] public RenderFragment? Trigger { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _isOpen;
    private string _id = "dd-" + Guid.NewGuid().ToString("N");
    private IJSObjectReference? _clickOutsideHandler;
    private DotNetObjectReference<Dropdown>? _dotNetRef;

    private void Toggle()
    {
        _isOpen = !_isOpen;
    }

    [JSInvokable]
    public void Close()
    {
        _isOpen = false;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            _clickOutsideHandler = await ThemeService.RegisterClickOutsideAsync(_dotNetRef, _id, nameof(Close));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_clickOutsideHandler != null)
        {
            await _clickOutsideHandler.InvokeVoidAsync("dispose");
            await _clickOutsideHandler.DisposeAsync();
        }
        _dotNetRef?.Dispose();
    }
}

public partial class DropdownItem : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private async Task HandleClick()
    {
        if (Disabled) return;
        await OnClick.InvokeAsync();
    }
}

public partial class DropdownSeparator : ComponentBase
{
    [Parameter] public string? Class { get; set; }
}
