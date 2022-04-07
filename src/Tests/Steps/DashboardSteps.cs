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
    [Scope(Feature = "Dashboard")]
    public sealed class DashboardSteps
    {
        private readonly IWebDriver webDriver;
        private readonly CreateProfilePage createProfilePage;
        private readonly LoginPage loginPage;
        private readonly DashboardPage dashboardPage;

        private string TracrBaseUrl;

        public DashboardSteps()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 60);

            createProfilePage = new CreateProfilePage(webDriver);
            loginPage = new LoginPage(webDriver);
            dashboardPage = new DashboardPage(webDriver);
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
            loginPage.ClickLogout();
        }

        [When(@"I click the log in link")]
        public void WhenIClickTheLogInLink()
        {
            loginPage.ClickLogin();
        }

        [When(@"I log in")]
        public void WhenILogIn(Table loginInfotable)
        {
            loginPage.SubmitRequiredInformation(loginInfotable);
        }

        [Then(@"I have the ability to see the dashboard")]
        public void ThenIHaveTheAbilityToSeeTheDashboard()
        {
            bool canSeeDashboard = dashboardPage.IsVisible();
            Assert.IsTrue(canSeeDashboard);
        }

        [AfterScenario]
        public void Dispose()
        {
            var client = new RestClient(TracrBaseUrl);
            var request = new RestRequest("api/user/delete/testName", Method.Delete);
            client.ExecuteAsync(request);

            webDriver.Dispose();
        }
    }
}
