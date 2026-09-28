using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazicons;
using Microsoft.AspNetCore.Components;

namespace __MYUI_NAMESPACE__
{
    public partial class MetricCard:ComponentBase
    {
        [Parameter]
        [EditorRequired]
        public required string Title {get;set;} = string.Empty;
        [Parameter]
        [EditorRequired] 
        public required string Value {get;set;} = string.Empty;
        [Parameter] public string Description {get;set;} = string.Empty;
        [Parameter] public SvgIcon? Icon {get;set;}
        [Parameter] public MetricVariant Variant {get;set;}  = MetricVariant.Default;
    }



    public enum MetricVariant
    {
        Default,
        V2,
        V3
    }
}