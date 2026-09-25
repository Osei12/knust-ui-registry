using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace __MYUI_NAMESPACE__;


public partial class Button : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Default;
    [Parameter] public bool IsRounded { get; set; } = false;
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected string ClassName => new CssBuilder("inline-flex gap-3 items-center justify-center text-sm font-normal transition-all active:scale-95 duration-100 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:opacity-50 disabled:pointer-events-none ring-offset-background")
        .AddClass(IsRounded ? "rounded-full" : "rounded-xl")
        .AddClass(VariantClasses[Variant])
        .AddClass(SizeClasses[Size])
        .AddClass(Class)
        .Build();

    private static readonly Dictionary<ButtonVariant, string> VariantClasses = new()
    {
        { ButtonVariant.Primary, "bg-primary text-white hover:bg-primary/90" },
        { ButtonVariant.Destructive, "bg-status-error text-white hover:bg-status-error/90" },
        { ButtonVariant.Outline, "border border-primary hover:bg-accent hover:text-accent-foreground" },
        { ButtonVariant.Secondary, "bg-secondary text-secondary-foreground dark:text-neutral-600 hover:bg-secondary/80" },
        { ButtonVariant.Ghost, "hover:bg-gray-50/40 hover:text-accent-foreground" },
        { ButtonVariant.Link, "underline-offset-4 hover:underline text-primary" }
    };

    private static readonly Dictionary<ButtonSize, string> SizeClasses = new()
    {
        { ButtonSize.Xs, "h-7 px-2 text-xs" },
        { ButtonSize.Sm, "h-9 px-3" },
        { ButtonSize.Default, "h-10 py-2 px-4" },
        { ButtonSize.Lg, "h-11 px-8 text-base" },
        { ButtonSize.Xl, "h-12 px-10 text-lg" },
        { ButtonSize.Icon, "h-10 w-10" }
    };

    public enum ButtonVariant
    {
        Primary,
        Destructive,
        Outline,
        Secondary,
        Ghost,
        Link
    }

    public enum ButtonSize
    {
        Xs,
        Sm,
        Default,
        Lg,
        Xl,
        Icon
    }
    }
    