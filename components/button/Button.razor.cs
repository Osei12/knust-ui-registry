using System.ComponentModel;

namespace __MYUI_NAMESPACE__;

public class Button:ComponentBase
{

     [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string Type { get; set; } = "button";

    [Parameter]
    public string? Class { get; set; }

    private string CssClass =>
        $"knust-ui-button {Class}".Trim();

}