using Microsoft.AspNetCore.Components;

namespace __MYUI_NAMESPACE__;

public partial class Tabs : ComponentBase
{
    [Parameter] public string? DefaultValue { get; set; }
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string? _activeTab;

    protected override void OnInitialized()
    {
        _activeTab = Value ?? DefaultValue;
    }

    public string? ActiveTab => _activeTab;

    public async Task SetActiveTab(string value)
    {
        _activeTab = value;
        await ValueChanged.InvokeAsync(value);
        StateHasChanged();
    }
}

public partial class TabsList : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}

public partial class TabsTrigger : ComponentBase
{
    [CascadingParameter] public Tabs? Parent { get; set; }
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private async Task HandleClick()
    {
        if (Parent != null)
        {
            await Parent.SetActiveTab(Value);
        }
    }

    protected string ClassName => $"inline-flex items-center justify-center whitespace-nowrap rounded-sm px-3 py-1.5 text-sm font-medium ring-offset-background transition-all focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 {(Parent?.ActiveTab == Value ? "bg-white text-slate-950 dark:text-white shadow-sm dark:bg-slate-950 dark:text-slate-50" : "text-slate-500 hover:text-slate-900 dark:text-slate-400 dark:hover:text-slate-100")} {Class}";
}

public partial class TabsContent : ComponentBase
{
    [CascadingParameter] public Tabs? Parent { get; set; }
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
