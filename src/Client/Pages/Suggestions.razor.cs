using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Tracr.Client.Models;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Client.Pages
{
    public partial class Suggestions : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        [Inject]
        public IMapper? Mapper { get; set; }

        public SuggestionsFilterViewModel SuggestionsFilterViewModel { get; set; } = new SuggestionsFilterViewModel();

        public bool LoadingData { get; set; } = false;
        public bool LoadingPropertyData { get; set; } = false;

        public List<StateCode> StateCodes { get; set; }
        public List<SortByOption> SortByOptions { get; set; }
        public List<PropertyDto> UserProperties { get;  set; }
        protected List<PropertyIncome> PropertyIncome { get; set; }

        public List<PropertyForSale> Properties { get; set; }

        private Dictionary<int, decimal> PropertyMonthlyExpenseMap = new Dictionary<int, decimal>();

        public string ErrorMessage { get; set; } = "";
        public decimal FinalProjectedProfit { get; private set; }
        public decimal MinListPrice { get; private set; }
        public decimal MaxListPrice { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null || RealEstateService == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
            else
            {
                await InitializePage();
                await SearchForProperties();
            }
        }

        private async Task InitializePage()
        {
            if (RealEstateService == null || PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                var taskGetStateCodes = RealEstateService.GetStates(); 
                var taskGetSortByOptions = RealEstateService.GetSortByOptions();
                var taskGetPropertyIncomes = PropertyService.GetUserPropertyIncome();
                var taskGetUserProperties = PropertyService.GetUserProperties();

                UserProperties = await taskGetUserProperties;
                PropertyMonthlyExpenseMap = UserProperties.ToDictionary(p => p.Id, p => p.Mortage.MonthlyPayment);

                PropertyIncome = await taskGetPropertyIncomes;
                StateCodes = await taskGetStateCodes;
                SortByOptions = await taskGetSortByOptions;

                SetProjectedProfits();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }
        }

        protected async Task SearchForProperties()
        {
            if (RealEstateService == null || Mapper == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingPropertyData = true;

                var resourceParameters = Mapper.Map<ForSaleResourceParameters>(SuggestionsFilterViewModel);
                Properties = await RealEstateService.GetPropertiesForSale(resourceParameters);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingPropertyData = false;
            }
        }

        private async void SetProjectedProfits()
        {
            var ProjectedProfits = new List<ProjectedProfit>();

            if (PropertyIncome?.Count > 0)
            {
                int endingMonth = DateTime.Today.AddMonths(6).Month;
                int endingYear = DateTime.Today.AddMonths(6).Year;

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
                FinalProjectedProfit = ProjectedProfits.Last().Profit;

                MinListPrice = FinalProjectedProfit * 100 / 20;
                MaxListPrice = FinalProjectedProfit * 100 / 5;

                SuggestionsFilterViewModel.MaxListPrice = MaxListPrice;
            }
        }

        private decimal GetMonthlyExpenses(int propertyId)
        {
            if (PropertyMonthlyExpenseMap.TryGetValue(propertyId, out decimal value))
                return value;
            else
                return 0;
        }

    }
}
