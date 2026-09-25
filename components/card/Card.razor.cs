using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__;

public partial class Card : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public bool Hoverable { get; set; } = false;
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("rounded-3xl border border-slate-200 bg-white text-slate-950 shadow-sm dark:border-white/10 dark:bg-slate-950 dark:text-slate-50 transition-all duration-300")
        .AddClass(Hoverable ? "hover:shadow-md hover:scale-[1.01] cursor-pointer" : "")
        .AddClass(Class ?? "")
        .Build();
}

public partial class CardHeader : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("flex flex-col space-y-1.5 p-6")
        .AddClass(Class ?? "")
        .Build();
}

public partial class CardTitle : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("text-2xl font-semibold leading-none tracking-tight")
        .AddClass(Class ?? "")
        .Build();
}

public partial class CardDescription : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("text-sm text-slate-500 dark:text-slate-400")
        .AddClass(Class ?? "")
        .Build();
}

public partial class CardContent : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("p-6 pt-0")
        .AddClass(Class ?? "")
        .Build();
}

public partial class CardFooter : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("flex items-center p-6 pt-0")
        .AddClass(Class ?? "")
        .Build();
}
