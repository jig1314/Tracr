using Microsoft.AspNetCore.Components;
using TabBlazor;

namespace Tracr.Client.Components
{
    public partial class CustomAlert : Alert
    {
        private bool dismissed;

        [Parameter]
        public CustomAlertType Type { get; set; }

        [Parameter]
        public string Icon { get; set; } = "";

        protected string CustomClassNames => base.ClassBuilder
            .Add("alert")
            .Add(BackgroundColor.GetColorClass("alert"))
            .Add(TextColor.GetColorClass("text"))
            .AddIf("alert-dismissible", Dismissible)
            .AddIf("alert-important", Important)
            .AddIf("alert-success", Type == CustomAlertType.Success)
            .AddIf("alert-info ", Type == CustomAlertType.Info)
            .AddIf("alert-warning", Type == CustomAlertType.Warning)
            .AddIf("alert-danger", Type == CustomAlertType.Error)
            .ToString();

        protected new void DismissAlert()
        {
            dismissed = true;
        }
    }

    public enum CustomAlertType
    {
        Success,
        Info,
        Warning,
        Error
    }
}