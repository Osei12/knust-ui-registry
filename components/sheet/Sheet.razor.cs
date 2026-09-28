using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__;

public enum SheetSide
{
    Left,
    Right,
    Bottom
}

public partial class Sheet : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public SheetSide Side { get; set; } = SheetSide.Right;
    [Parameter] public bool CloseOnBackdropClick { get; set; } = true;
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _show;
    private bool _isVisible;

    protected override async Task OnParametersSetAsync()
    {
        if (IsOpen && !_show)
        {
            _show = true;
            await Task.Yield();
            _isVisible = true;
        }
        else if (!IsOpen && _show)
        {
            _isVisible = false;
            await Task.Delay(300); // Match transition-duration
            _show = false;
        }
    }

    private async Task CloseAsync()
    {
        await IsOpenChanged.InvokeAsync(false);
    }

    private async Task HandleBackdropClick()
    {
        if (CloseOnBackdropClick)
        {
            await CloseAsync();
        }
    }

    private string SideClasses => Side switch
    {
        SheetSide.Left => $"fixed inset-y-0 left-0 h-full w-3/4 border-r sm:max-w-sm transition-transform duration-300 ease-in-out {(_isVisible ? "translate-x-0" : "-translate-x-full")}",
        SheetSide.Right => $"fixed inset-y-0 right-0 h-full w-3/4 border-l sm:max-w-sm transition-transform duration-300 ease-in-out {(_isVisible ? "translate-x-0" : "translate-x-full")}",
        SheetSide.Bottom => $"fixed inset-x-0 bottom-0 w-full border-t sm:h-[40vh] transition-transform duration-300 ease-in-out {(_isVisible ? "translate-y-0" : "translate-y-full")}",
        _ => ""
    };
}

public partial class SheetHeader : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
}

public partial class SheetTitle : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
}

public partial class SheetDescription : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
}

public partial class SheetFooter : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
}
