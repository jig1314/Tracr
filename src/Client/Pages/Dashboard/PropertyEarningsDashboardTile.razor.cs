using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class PropertyEarningsDashboardTile : ComponentBase
    {
        public enum ViewMode
        {
            YTD,
            SixMonths,
            OneYear
        }

        protected Dropdown DropdownRef { get; set; }

        protected ApexChartOptions<PropertyEarnings> ChartOptions;

        private ViewMode _currentViewMode;
        public ViewMode CurrentViewMode
        {
            get
            {
                return _currentViewMode;
            }
            set
            {
                if (_currentViewMode != value)
                {
                    _currentViewMode = value;
                    OnViewModeChanged();
                }
            }
        }

        public string FinalProjection { get; set; } = "";

        public string ViewModeMessage { get; set; }

        public List<PropertyEarnings> PastPropertyEarnings { get; set; }

        protected override void OnInitialized()
        {
            ChartOptions = new ApexChartOptions<PropertyEarnings>()
            {
                Chart = new Chart()
                {
                    Toolbar = new Toolbar()
                    {
                        Show = false
                    }
                }
            };

            CurrentViewMode = ViewMode.YTD;
            ViewModeMessage = "Year-To-Date (YTD)";
            PastPropertyEarnings = new List<PropertyEarnings>();
        }

        private void OnViewModeChanged()
        {
            switch (_currentViewMode)
            {
                case ViewMode.YTD:
                    ViewModeMessage = "Year-To-Date (YTD)";
                    break;
                case ViewMode.SixMonths:
                    ViewModeMessage = "Last 6 months";
                    break;
                case ViewMode.OneYear:
                    ViewModeMessage = "Last year";
                    break;
                default:
                    break;
            }
            StateHasChanged();
        }

        protected string GetProfitLabel(decimal value)
        {
            return value.ToString("C");
        }
    }
}
