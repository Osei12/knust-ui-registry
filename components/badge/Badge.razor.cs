using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__
{
    public partial class Badge : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public BadgeVariant Status { get; set; } = BadgeVariant.Pending;
        [Parameter] public BadgeSize Size { get; set; } = BadgeSize.Default;
        [Parameter] public bool IsRounded { get; set; } = false;
        [Parameter] public string Class { get; set; } = string.Empty;
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

        private static readonly Dictionary<BadgeVariant, string> StatusClasses = new()
        {
            { BadgeVariant.Valid, "bg-green-100 text-green-700 border border-green-300 dark:bg-green-900/30 dark:text-green-400 dark:border-green-800" },
            { BadgeVariant.Pending, "bg-yellow-50 text-yellow-700 border border-yellow-200 dark:bg-yellow-900/30 dark:text-yellow-400 dark:border-yellow-800" },
            { BadgeVariant.Invalid, "bg-red-100 text-red-700 border border-red-300 dark:bg-red-900/30 dark:text-red-400 dark:border-red-800" },
            { BadgeVariant.Rejected, "bg-orange-100 text-orange-700 border border-orange-300 dark:bg-orange-900/30 dark:text-orange-400 dark:border-orange-800" },
            { BadgeVariant.Completed, "bg-status-successBg text-status-success border border-successBg dark:bg-green-950 dark:text-green-400 dark:border-green-900" },
            { BadgeVariant.Secondary, "bg-secondary text-secondary-foreground border-transparent" },
            { BadgeVariant.Outline, "text-foreground border border-slate-200 dark:border-white/10" },
            { BadgeVariant.Ghost, "text-foreground border-transparent hover:bg-slate-100 dark:hover:bg-white/5" },
        };

        private static readonly Dictionary<BadgeSize, string> SizeClasses = new()
        {
            { BadgeSize.Small, "px-2 h-4 py-0.5 text-[10px] leading-none" },
            { BadgeSize.Default, "px-2.5 h-5 py-0.5 text-xs font-semibold leading-none" },
            { BadgeSize.Large, "px-3 h-6 py-1 text-sm font-semibold leading-none" }
        };

        protected string GetBadgeClasses()
        {
            var baseClasses = "inline-flex gap-2 w-max items-center justify-center font-normal whitespace-nowrap transition-colors";
            var statusClasses = StatusClasses.TryGetValue(Status, out var s) ? s : "bg-gray-100 text-gray-700 border border-gray-300";
            var sizeClasses = SizeClasses.TryGetValue(Size, out var sz) ? sz : string.Empty;

            return new CssBuilder(baseClasses)
                .AddClass(IsRounded ? "rounded-full" : "rounded-md")
                .AddClass(statusClasses)
                .AddClass(sizeClasses)
                .AddClass(Class)
                .Build();
        }

        public enum BadgeVariant
        {
            Valid,
            Invalid,
            Pending,
            Rejected,
            Completed,
            Secondary,
            Outline,
            Ghost
        }

        public enum BadgeSize
        {
            Small,
            Default,
            Large
        }
    }
}
