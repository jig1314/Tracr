using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

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
        private Dictionary<int, string> PropertyNameMap = new Dictionary<int, string>();

        protected Dropdown DropdownRef { get; set; }

        protected ApexChart<TopEarningProperty> TopEarningPropertyChart { get; set; }

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

        public List<TopEarningProperty> TopEarningProperties { get; set; } = new List<TopEarningProperty>();

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
            OnUserPropertiesChanged();
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

            OnPropertyIncomeChanged();
            StateHasChanged();
        }

        private async void OnPropertyIncomeChanged()
        {
            TopEarningProperties = new List<TopEarningProperty>();

            if (PropertyIncome?.Count > 0)
            {
                int startingMonth = default;
                int startingYear = default;

                switch (CurrentViewMode)
                {
                    case ViewMode.YTD:
                        startingMonth = 1;
                        startingYear = DateTime.Today.Year;
                        break;
                    case ViewMode.SixMonths:
                        startingMonth = DateTime.Today.AddMonths(-6).Month;
                        startingYear = DateTime.Today.AddMonths(-6).Year;
                        break;
                    case ViewMode.OneYear:
                        startingMonth = DateTime.Today.AddYears(-1).Month;
                        startingYear = DateTime.Today.AddYears(-1).Year;
                        break;
                    default:
                        break;
                }

                var startDate = new DateTime(startingYear, startingMonth, CurrentViewMode == ViewMode.YTD ? 1 : DateTime.Today.Day);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
                var diffInMonths = Math.Round(endDate.Subtract(startDate).Days / (365.25 / 12));
                var allEarnings = new List<TopEarningProperty>();

                PropertyIncome.Where(e => e.Month >= startDate && e.Month <= endDate).DistinctBy(e => e.PropertyId).ToList()
                    .ForEach(property =>
                    {
                        var earnings = new TopEarningProperty()
                        {
                            PropertyName = PropertyNameMap.TryGetValue(property.PropertyId, out string? name) ? name : "",
                            Earnings = PropertyIncome.Where(e => e.PropertyId == property.PropertyId && e.Month >= startDate && e.Month <= endDate).Sum(e => e.Income) 
                                        - (GetMonthlyExpenses(property.PropertyId) * (decimal)diffInMonths)
                        };
                        allEarnings.Add(earnings);
                    });

                TopEarningProperties = allEarnings.OrderByDescending(e => e.Earnings).Take(5).ToList();
                if (TopEarningPropertyChart != null)
                {
                    await TopEarningPropertyChart.AppendDataAsync(TopEarningProperties);
                    await TopEarningPropertyChart.RenderAsync();
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
            PropertyNameMap = UserProperties.ToDictionary(p => p.Id, p => p.Name);
        }

        protected string GetProfitLabel(decimal value)
        {
            return value.ToString("C");
        }
    }
}
