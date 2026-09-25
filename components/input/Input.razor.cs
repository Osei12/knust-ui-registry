using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__;

public partial class Input : ComponentBase, IDisposable
{
    [Parameter] public string Type { get; set; } = "text";
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public Expression<Func<string>>? ValueExpression { get; set; }

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? ContainerClass { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public InputSize Size { get; set; } = InputSize.Default;
    [Parameter] public InputStatus Status { get; set; } = InputStatus.Default;

    [Parameter(CaptureUnmatchedValues = true)] 
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private EditContext? EditContext { get; set; }

    private FieldIdentifier _fieldIdentifier;
    private bool _hasError => (EditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false) || Status == InputStatus.Error;

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

    private string InputClasses => new CssBuilder("flex w-full rounded-md border bg-white px-3 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-slate-500 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 dark:bg-slate-950 dark:text-slate-50 dark:placeholder:text-slate-400 transition-colors")
        .AddClass("border-slate-200 dark:border-white/10", !_hasError && Status == InputStatus.Default)
        .AddClass(SizeClasses[Size])
        .AddClass(StatusClasses[Status])
        .AddClass("border-red-500 focus-visible:ring-red-500", _hasError)
        .AddClass("focus-visible:ring-actionPrimary", !_hasError && Status == InputStatus.Default)
        .AddClass(Class ?? "")
        .Build();


    private static readonly Dictionary<InputSize, string> SizeClasses = new()
    {
        { InputSize.Sm, "h-8 text-xs" },
        { InputSize.Default, "h-10 text-sm" },
        { InputSize.Lg, "h-12 text-base" }
    };

    private static readonly Dictionary<InputStatus, string> StatusClasses = new()
    {
        { InputStatus.Default, "" },
        { InputStatus.Success, "border-green-500 focus-visible:ring-green-500" },
        { InputStatus.Warning, "border-yellow-500 focus-visible:ring-yellow-500" },
        { InputStatus.Error, "border-red-500 focus-visible:ring-red-500" }
    };

    private async Task OnInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        await ValueChanged.InvokeAsync(Value);
        
        if (EditContext != null && ValueExpression != null)
        {
            EditContext.NotifyFieldChanged(_fieldIdentifier);
        }
    }

    public enum InputSize { Sm, Default, Lg }
    public enum InputStatus { Default, Success, Warning, Error }
}
