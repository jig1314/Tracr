using ApexCharts;
using Microsoft.AspNetCore.Components;
using TabBlazor;
using Tracr.Client.Models;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

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

        [Parameter]
        public HashSet<int> SelectedPropertyIds { get; set; }

        protected Dropdown DropdownRef { get; set; }

        protected ApexChart<PropertyEarnings> PropertyEarningsChart { get; set; }

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

        private Dictionary<int, decimal> PropertyMonthlyExpenseMap = new Dictionary<int, decimal>();
        private Dictionary<int, string> PropertyNameMap = new Dictionary<int, string>();

        public string FinalProjection { get; set; } = "";

        public string ViewModeMessage { get; set; }

        public List<PropertyEarnings> PastPropertyEarnings { get; set; } = new List<PropertyEarnings>();

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
            PastPropertyEarnings = new List<PropertyEarnings>();
            FinalProjection = "";

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

                var startDate = new DateTime(startingYear, startingMonth, 1);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
                var allEarnings = new List<PropertyEarnings>()
                {
                    new PropertyEarnings()
                    {
                        Month = startDate,
                        Earnings = PropertyIncome.Where(e => e.Month.Month == startDate.Month && e.Month.Year == startDate.Year).Sum(e => e.Income) -
                                    UserProperties.DistinctBy(e => e.Id).Sum(e => GetMonthlyExpenses(e.Id))
                    }
                };

                var currentDate = startDate.AddMonths(1);

                while (currentDate <= endDate)
                {
                    var earnings = new PropertyEarnings()
                    {
                        Month = currentDate,
                        Earnings = PropertyIncome.Where(e => e.Month.Month == currentDate.Month && e.Month.Year == currentDate.Year).Sum(e => e.Income)
                    };

                    earnings.Earnings -= UserProperties.DistinctBy(e => e.Id).Sum(e => GetMonthlyExpenses(e.Id));

                    earnings.Earnings += allEarnings.Last().Earnings;
                    allEarnings.Add(earnings);

                    currentDate = currentDate.AddMonths(1);
                }

                PastPropertyEarnings = allEarnings;
                FinalProjection = PastPropertyEarnings.Last().Earnings.ToString("C");
                if (PropertyEarningsChart != null)
                {
                    await PropertyEarningsChart.AppendDataAsync(allEarnings);
                    await PropertyEarningsChart.RenderAsync();
                }
            }
        }

        private decimal GetMonthlyExpenses(int propertyId)
        {
            if (PropertyMonthlyExpenseMap.TryGetValue(propertyId, out decimal value) && (SelectedPropertyIds == null || SelectedPropertyIds.Count == 0 || SelectedPropertyIds.Contains(propertyId)))
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
