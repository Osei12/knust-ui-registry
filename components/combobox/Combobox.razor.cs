using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__;

public partial class Combobox<TValue> : ComponentBase, IAsyncDisposable
{
    [Inject] private ThemeService ThemeService { get; set; } = default!;

    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public Expression<Func<TValue?>>? ValueExpression { get; set; }
    [Parameter] public IEnumerable<ComboboxItem<TValue>> Items { get; set; } = Enumerable.Empty<ComboboxItem<TValue>>();
    [Parameter] public string? Placeholder { get; set; } = "Select option...";
    [Parameter] public string? SearchPlaceholder { get; set; } = "Search...";
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private EditContext? EditContext { get; set; }
    
    private FieldIdentifier _fieldIdentifier;
    private bool _hasError => EditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false;

    private bool _isOpen;
    private string _searchTerm = "";
    private string _id = "cb-" + Guid.NewGuid().ToString("N");
    private ElementReference _searchInputRef;
    private IJSObjectReference? _clickOutsideHandler;
    private DotNetObjectReference<Combobox<TValue>>? _dotNetRef;

    private IEnumerable<ComboboxItem<TValue>> FilteredItems => string.IsNullOrWhiteSpace(_searchTerm)
        ? Items
        : Items.Where(i => i.Label.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase));

    private ComboboxItem<TValue>? SelectedItem => Items.FirstOrDefault(i => EqualityComparer<TValue>.Default.Equals(i.Value, Value));

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

    private async Task ToggleDropdown()
    {
        if (Disabled) return;
        _isOpen = !_isOpen;
        if (_isOpen)
        {
            _searchTerm = "";
            await Task.Yield();
            try { await _searchInputRef.FocusAsync(); } catch { }
        }
    }

    private async Task SelectItem(ComboboxItem<TValue> item)
    {
        Value = item.Value;
        _isOpen = false;
        await ValueChanged.InvokeAsync(Value);

        if (EditContext != null && ValueExpression != null)
        {
            EditContext.NotifyFieldChanged(_fieldIdentifier);
        }
    }

    private string ComboboxButtonClasses => new CssBuilder("flex h-10 w-full items-center justify-between rounded-md border bg-white px-3 py-2 text-sm transition-colors")
        .AddClass("border-slate-200 dark:border-white/10", !_hasError)
        .AddClass("border-red-500", _hasError)
        .AddClass("dark:bg-slate-950 dark:text-slate-50")
        .Build();

    private void OnSearchInput(ChangeEventArgs e)
    {
        _searchTerm = e.Value?.ToString() ?? "";
    }

    [JSInvokable]
    public void CloseDropdown()
    {
        _isOpen = false;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            _clickOutsideHandler = await ThemeService.RegisterClickOutsideAsync(_dotNetRef, _id, nameof(CloseDropdown));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (EditContext != null)
        {
            EditContext.OnValidationStateChanged -= HandleValidationStateChanged;
        }

        if (_clickOutsideHandler != null)
        {
            await _clickOutsideHandler.InvokeVoidAsync("dispose");
            await _clickOutsideHandler.DisposeAsync();
        }
        _dotNetRef?.Dispose();
    }
}

public class ComboboxItem<TValue>
{
    public string Label { get; set; } = string.Empty;
    public TValue Value { get; set; } = default!;
}
