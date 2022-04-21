﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TabBlazor
{
    public partial class CustomDropdownMenu : TablerBaseComponent
    {
        [Parameter] public bool Card { get; set; } = false;
        [Parameter] public bool Arrow { get; set; } = false;

        private List<CustomDropdownItem> subMenuItems = new();

        protected override string ClassNames => ClassBuilder
           .Add("dropdown-menu")
           .AddIf($"dropdown-menu-card", Card)
           .Add(BackgroundColor.GetColorClass("bg"))
           .Add(TextColor.GetColorClass("text"))
           .AddIf("show", true)
           .AddIf($"dropdown-menu-arrow", Arrow)
           .ToString();

        public void CloseAllSubMenus()
        {
            foreach (var item in subMenuItems)
            {
                item.CloseSubMenu();
            }
            StateHasChanged();
        }

        public void AddSubMenuItem(CustomDropdownItem item)
        {
            if (item != null && !subMenuItems.Contains(item))
            {
                subMenuItems.Add(item);
            }
        }

        public void RemoveSubMenuItem(CustomDropdownItem item)
        {
            if (item != null && subMenuItems.Contains(item))
            {
                subMenuItems.Remove(item);
            }
        }
     
    }
}