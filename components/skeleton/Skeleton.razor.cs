using Microsoft.AspNetCore.Components;

namespace __MYUI_NAMESPACE__;

public partial class Skeleton : ComponentBase
{
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => $"animate-pulse rounded-md bg-slate-100 dark:bg-white/10 {Class}";
}
