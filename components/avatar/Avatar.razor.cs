using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__;

public partial class Avatar : ComponentBase
{
    [Parameter] public string? Src { get; set; }
    [Parameter] public string? Alt { get; set; }
    [Parameter] public string? Fallback { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public AvatarSize Size { get; set; } = AvatarSize.Md;
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool _imageError;

    private void HandleImageError()
    {
        _imageError = true;
    }

    private static readonly Dictionary<AvatarSize, string> SizeClasses = new()
    {
        { AvatarSize.Xs, "h-6 w-6 text-[10px]" },
        { AvatarSize.Sm, "h-8 w-8 text-xs" },
        { AvatarSize.Md, "h-10 w-10 text-sm" },
        { AvatarSize.Lg, "h-12 w-12 text-base" },
        { AvatarSize.Xl, "h-16 w-16 text-xl" },
        { AvatarSize.TwoXl, "h-20 w-20 text-2xl" }
    };

    protected string ClassName => new CssBuilder("relative flex shrink-0 overflow-hidden rounded-full border border-slate-200 dark:border-white/10")
        .AddClass(SizeClasses[Size])
        .AddClass(Class)
        .Build();

    public enum AvatarSize
    {
        Xs,
        Sm,
        Md,
        Lg,
        Xl,
        TwoXl
    }
}
