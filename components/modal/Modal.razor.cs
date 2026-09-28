using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using __MYUI_NAMESPACE__.Utils;
namespace __MYUI_NAMESPACE__;

public partial class Modal : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
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
}

public partial class ModalHeader : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    
    protected string ClassName => new CssBuilder("flex flex-col space-y-1.5 text-center sm:text-left")
        .AddClass(Class ?? "")
        .Build();
}

public partial class ModalTitle : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    
    protected string ClassName => new CssBuilder("text-lg font-semibold leading-none tracking-tight")
        .AddClass(Class ?? "")
        .Build();
}

public partial class ModalDescription : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    
    protected string ClassName => new CssBuilder("text-sm text-slate-500 dark:text-slate-400")
        .AddClass(Class ?? "")
        .Build();
}

public partial class ModalFooter : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    
    protected string ClassName => new CssBuilder("flex flex-col-reverse sm:flex-row sm:justify-end sm:space-x-2")
        .AddClass(Class ?? "")
        .Build();
}
