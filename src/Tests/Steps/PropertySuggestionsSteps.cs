using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using Tracr.Tests.Pages;

namespace Tracr.Tests.Steps
{
    [Binding]
    [Scope(Feature = "Property Suggestions")]
    public sealed class PropertySuggestionsSteps
    {
        private readonly IWebDriver webDriver;
        private readonly CreateProfilePage createProfilePage;
        private readonly PropertySuggestionsPage propertySuggestionsPage;

        private string TracrBaseUrl;

        public PropertySuggestionsSteps()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 60);

            createProfilePage = new CreateProfilePage(webDriver);
            propertySuggestionsPage = new PropertySuggestionsPage(webDriver);
            TracrBaseUrl = "";
        }

        [BeforeScenario]
        public void SetBaseUrl()
        {
            TracrBaseUrl = Environment.GetEnvironmentVariable("TracrUrl");
        }

        [Given(@"I have registered for an account")]
        public void GivenIHaveRegisteredForAnAccount(Table registerInfotable)
        {
            webDriver.Navigate().GoToUrl(TracrBaseUrl);
            createProfilePage.ClickRegister();
            createProfilePage.SubmitRequiredInformation(registerInfotable);
        }

        [When(@"I click the suggestions button")]
        public void WhenIClickTheSuggestionsButton()
        {
            propertySuggestionsPage.ClickSuggestionsButton();
        }

        [Then(@"I will not receive property suggestions")]
        public void ThenIWillNotReceivePropertySuggestions()
        {
            bool suggestionsUnavailable = propertySuggestionsPage.SuggestionsUnavailable();
            Assert.IsTrue(suggestionsUnavailable);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var client = new RestClient(TracrBaseUrl);
            var request = new RestRequest("api/user/delete/testName", Method.Delete);
            client.ExecuteAsync(request);

            webDriver.Dispose();
        }
    }
}