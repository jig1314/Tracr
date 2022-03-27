using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class ProfitProjectionDashboardTile : ComponentBase
    {
        public enum ViewMode
        {
            SixMonths,
            OneYear,
            FiveYears
        }

        protected Dropdown DropdownRef { get; set; }

        protected ApexChartOptions<ProjectedProfit> ChartOptions;

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

        public List<ProjectedProfit> ProjectedProfits { get; set; }

        protected override void OnInitialized()
        {
            ChartOptions = new ApexChartOptions<ProjectedProfit>()
            {
                Chart = new Chart()
                {
                    Toolbar = new Toolbar()
                    {
                        Show = false
                    }
                }
            };

            CurrentViewMode = ViewMode.SixMonths;
            ViewModeMessage = "Next 6 months";
            ProjectedProfits = new List<ProjectedProfit>();
        }

        private void OnViewModeChanged()
        {
            switch (_currentViewMode)
            {
                case ViewMode.SixMonths:
                    ViewModeMessage = "Next 6 months";
                    break;
                case ViewMode.OneYear:
                    ViewModeMessage = "Next 1 year";
                    break;
                case ViewMode.FiveYears:
                    ViewModeMessage = "Next 5 years";
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
