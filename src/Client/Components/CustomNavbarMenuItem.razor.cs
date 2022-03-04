using Microsoft.AspNetCore.Components;
using TabBlazor;

namespace Tracr.Client.Components
{
    public partial class CustomNavbarMenuItem : NavbarMenuItem
    {
        [Parameter] public bool IsActive { get; set; } = false;

        protected string CustomClassNames => base.ClassBuilder
            .Add(base.ClassNames)
            .AddIf("active", !IsDropdown && IsActive)
            .ToString();
    }
}

