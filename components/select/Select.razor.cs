using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Collections.Generic;
using __UTILS__;
namespace __MYUI_NAMESPACE__;

public partial class Select : ComponentBase, IDisposable
{
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    [Parameter] public Expression<Func<string?>>? ValueExpression { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private EditContext? EditContext { get; set; }
    
    private FieldIdentifier _fieldIdentifier;
    private bool _hasError => EditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false;

    protected override void OnInitialized()
    {
        if (ValueExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
        }

        if (EditContext != null)
        {
            EditContext.OnValidationStateChanged += HandleValidationStateChanged;
        }
    }

    private void HandleValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        StateHasChanged();
    }

    public void Dispose()
    {
        if (EditContext != null)
        {
            EditContext.OnValidationStateChanged -= HandleValidationStateChanged;
        }
    }

    private string SelectClasses => new CssBuilder("flex h-10 w-full items-center justify-between rounded-md border bg-white px-3 py-2 text-sm ring-offset-background focus:outline-none focus:ring-2 focus:ring-ring focus:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 appearance-none dark:bg-slate-950 dark:text-slate-50 transition-colors")
        .AddClass("border-slate-200 dark:border-white/10", !_hasError)
        .AddClass("border-red-500 focus:ring-red-500", _hasError)
        .AddClass(Class ?? "")
        .Build();

    private async Task OnChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        await ValueChanged.InvokeAsync(Value);

        if (EditContext != null && ValueExpression != null)
        {
            EditContext.NotifyFieldChanged(_fieldIdentifier);
        }
    }
}
