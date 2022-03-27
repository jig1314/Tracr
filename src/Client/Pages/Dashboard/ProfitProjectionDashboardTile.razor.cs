using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

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

        private List<PropertyIncome> _propertyIncome;

        [Parameter]
        public List<PropertyIncome> PropertyIncome
        {
            get
            {
                return _propertyIncome;
            }
            set
            {
                if (_propertyIncome != value)
                {
                    _propertyIncome = value;
                    OnPropertyIncomeChanged();
                }
            }
        }

        private List<PropertyDto> _userProperties;

        [Parameter]
        public List<PropertyDto> UserProperties
        {
            get
            {
                return _userProperties;
            }
            set
            {
                if (_userProperties != value)
                {
                    _userProperties = value;
                    OnUserPropertiesChanged();
                }
            }
        }

        private Dictionary<int, decimal> PropertyMonthlyExpenseMap = new Dictionary<int, decimal>();

        protected Dropdown DropdownRef { get; set; }

        protected ApexChart<ProjectedProfit> ProjectedProfitChart { get; set; }

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

        public List<ProjectedProfit> ProjectedProfits { get; set; } = new List<ProjectedProfit>();

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
            OnUserPropertiesChanged();
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

            OnPropertyIncomeChanged();
            StateHasChanged();
        }

        private async void OnPropertyIncomeChanged()
        {
            ProjectedProfits = new List<ProjectedProfit>();
            FinalProjection = "";

            if (PropertyIncome?.Count > 0)
            {
                int endingMonth = default;
                int endingYear = default;

                switch (CurrentViewMode)
                {
                    case ViewMode.SixMonths:
                        endingMonth = DateTime.Today.AddMonths(6).Month;
                        endingYear = DateTime.Today.AddMonths(6).Year;
                        break;
                    case ViewMode.OneYear:
                        endingMonth = DateTime.Today.AddYears(1).Month;
                        endingYear = DateTime.Today.AddYears(1).Year;
                        break;
                    case ViewMode.FiveYears:
                        endingMonth = DateTime.Today.AddYears(5).Month;
                        endingYear = DateTime.Today.AddYears(5).Year;
                        break;
                    default:
                        break;
                }

                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var endDate = new DateTime(endingYear, endingMonth, DateTime.DaysInMonth(endingYear, endingMonth));

                var projectedProfits = new List<ProjectedProfit>()
                {
                    new ProjectedProfit()
                    {
                        Month = startDate,
                        Profit = PropertyIncome.Where(e => e.Month.Month == startDate.Month && e.Month.Year == startDate.Year).Sum(e => e.Income) - 
                                    UserProperties.DistinctBy(e => e.Id).Sum(e => GetMonthlyExpenses(e.Id))
                    }
                };

                var currentDate = startDate.AddMonths(1);

                while (currentDate <= endDate)
                {
                    var profit = new ProjectedProfit()
                    {
                        Month = currentDate,
                        Profit = PropertyIncome.Where(e => e.Month.Month == currentDate.Month && e.Month.Year == currentDate.Year).Sum(e => e.Income) 
                    };

                    profit.Profit -= UserProperties.DistinctBy(e => e.Id).Sum(e => GetMonthlyExpenses(e.Id));

                    profit.Profit += projectedProfits.Last().Profit;
                    projectedProfits.Add(profit);

                    currentDate = currentDate.AddMonths(1);
                }

                ProjectedProfits = projectedProfits;
                FinalProjection = ProjectedProfits.Last().Profit.ToString("C");
                if (ProjectedProfitChart != null)
                {
                    await ProjectedProfitChart.AppendDataAsync(projectedProfits);
                    await ProjectedProfitChart.RenderAsync();
                }
            }
        }

        private decimal GetMonthlyExpenses(int propertyId)
        {
            if (PropertyMonthlyExpenseMap.TryGetValue(propertyId, out decimal value))
                return value;
            else
                return 0;
        }

        private void OnUserPropertiesChanged()
        {
            PropertyMonthlyExpenseMap = UserProperties.ToDictionary(p => p.Id, p => p.Mortage.MonthlyPayment);
        }

        protected string GetProfitLabel(decimal value)
        {
            return value.ToString("C");
        }
    }
}
