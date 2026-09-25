using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__;

public partial class Switch : ComponentBase, IDisposable
{
    [Parameter] public bool Checked { get; set; }
    [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
    [Parameter] public Expression<Func<bool>>? CheckedExpression { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private EditContext? EditContext { get; set; }
    
    private FieldIdentifier _fieldIdentifier;
    private bool _hasError => EditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false;

    protected override void OnInitialized()
    {
        if (CheckedExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(CheckedExpression);
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

    private string SwitchClasses => new CssBuilder("peer inline-flex h-5 w-9 shrink-0 cursor-pointer items-center rounded-full border-2 transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-actionPrimary focus-visible:ring-offset-2 focus-visible:ring-offset-white disabled:cursor-not-allowed disabled:opacity-50")
        .AddClass("border-transparent", !_hasError)
        .AddClass("border-red-500", _hasError)
        .AddClass(Checked ? "bg-actionPrimary" : "bg-gray-200 dark:bg-white/10")
        .AddClass(Class ?? "")
        .Build();

    private async Task ToggleAsync()
    {
        if (Disabled) return;
        Checked = !Checked;
        await CheckedChanged.InvokeAsync(Checked);

        if (EditContext != null && CheckedExpression != null)
        {
            EditContext.NotifyFieldChanged(_fieldIdentifier);
        }
    }
}
