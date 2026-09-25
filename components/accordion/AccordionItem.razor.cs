using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__;

public partial class AccordionItem : ComponentBase
{
    [CascadingParameter] public Accordion? Parent { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void OnInitialized()
    {
        Parent?.RegisterItem(this);
    }

    public async Task Toggle()
    {
        IsOpen = !IsOpen;
        await Task.CompletedTask;
    }

    public async Task Close()
    {
        IsOpen = false;
        await Task.CompletedTask;
    }

    private async Task HandleToggle()
    {
        if (Parent != null)
        {
            await Parent.ToggleItem(this);
        }
        else
        {
            await Toggle();
        }
    }
}
