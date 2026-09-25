using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace __MYUI_NAMESPACE__
{
    public partial class Alert : ComponentBase
    {
        [Parameter] public string? AlertTitle { get; set; }
        [Parameter] public bool Dissmisible { get; set; } = false;
        [Parameter] public string Description { get; set; } = string.Empty;
        [Parameter] public AlertVariant Variant { get; set; }
        [Parameter] public AlertType Type { get; set; } = AlertType.Default;
        [Parameter] public EventCallback OnDismissed { get; set; }

        [Parameter] public RenderFragment? Icon { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
        private bool IsVisible { get; set; } = true;
        private bool _isFadingOut { get; set; } = false;

        [Parameter] public string? Class { get; set; }

        private string GetAlertClasses()
        {
            var basesClasses = $"flex w-full relative items-start gap-3 px-4 py-3 rounded-xl transition-opacity transition-all duration-300 ease-in-out transform shadow-sm {(IsVisible ? "opacity-100" : "opacity-0")}";

            var colorClasses = Variant switch
            {
                AlertVariant.Info => Type == AlertType.Bordered
                    ? "border bg-status-infoBg border-status-info/50 text-status-info "
                    : "bg-status-infoBg text-status-info",
                AlertVariant.Success => Type == AlertType.Bordered
                    ? "border bg-status-successBg border-status-success/50 text-status-success "
                    : "bg-status-successBg text-status-success",
                AlertVariant.Warning => Type == AlertType.Bordered
                    ? "border bg-status-warningBg border-status-warning/50 text-status-warning "
                    : "bg-status-warningBg text-status-warning",
                AlertVariant.Error => Type == AlertType.Bordered
                    ? "border bg-status-errorBg border-status-error/50 text-status-error "
                    : "bg-status-errorBg text-status-error",
                _ => "bg-gray-100 text-gray-700"
            };

            return new CssBuilder(basesClasses)
                .AddClass(colorClasses)
                .AddClass(Class ?? "")
                .Build();
        }

        private async Task Dismiss()
        {
            _isFadingOut = true;
            StateHasChanged();

            await Task.Delay(500); 
            IsVisible = false;
            _isFadingOut = false;
            StateHasChanged();

            if (OnDismissed.HasDelegate)
            {
                await OnDismissed.InvokeAsync();
            }
        }

        public enum AlertVariant
        {
            Info,
            Error,
            Success,
            Warning
        }

        public enum AlertType
        {
            Default,
            Bordered
        }
    }
}