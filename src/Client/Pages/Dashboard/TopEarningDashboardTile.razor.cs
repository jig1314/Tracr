using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class TopEarningDashboardTile : ComponentBase
    {
        public enum ViewMode
        {
            YTD,
            SixMonths,
            OneYear
        }

        protected Dropdown DropdownRef { get; set; }

        protected ApexChartOptions<TopEarningProperty> ChartOptions;

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

        public string ViewModeMessage { get; set; }

        public List<TopEarningProperty> TopEarningProperties { get; set; }

        protected override void OnInitialized()
        {
            ChartOptions = new ApexChartOptions<TopEarningProperty>()
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
            TopEarningProperties = new List<TopEarningProperty>();
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
