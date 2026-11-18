using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using __UTILS__;
namespace __MYUI_NAMESPACE__;

public partial class Modal : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Description { get; set; }
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
    [Parameter] public bool Sticky { get; set; } = true;
    
    protected string ClassName => new CssBuilder("flex flex-col space-y-1.5 text-center sm:text-left")
        .AddClass("sticky top-0 z-10 -mx-6 px-6 pt-6 pb-4 bg-white dark:bg-slate-950 border-b border-slate-100 dark:border-white/10 pr-12", Sticky)
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

public partial class ModalBody : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    
    protected string ClassName => new CssBuilder("py-2")
        .AddClass(Class ?? "")
        .Build();
}

public partial class ModalFooter : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public bool Sticky { get; set; } = false;
    
    protected string ClassName => new CssBuilder("flex flex-col-reverse sm:flex-row sm:justify-end sm:space-x-2")
        .AddClass("sticky bottom-0 z-10 -mb-6 -mx-6 px-6 pb-6 pt-4 bg-white dark:bg-slate-950 border-t border-slate-100 dark:border-white/10", Sticky)
        .AddClass(Class ?? "")
        .Build();
}
