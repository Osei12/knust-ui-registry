using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__;

public partial class Accordion : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public bool AllowMultiple { get; set; } = false;
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private readonly List<AccordionItem> _items = new();

    public void RegisterItem(AccordionItem item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }

    public async Task ToggleItem(AccordionItem item)
    {
        if (!AllowMultiple && !item.IsOpen)
        {
            foreach (var otherItem in _items)
            {
                if (otherItem != item && otherItem.IsOpen)
                {
                    await otherItem.Close();
                }
            }
        }
        
        await item.Toggle();
        StateHasChanged();
    }
}
