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
    [Scope(Feature = "Login")]
    public sealed class LoginSteps
    {
        private readonly IWebDriver webDriver;
        private readonly CreateProfilePage createProfilePage;
        private readonly LoginPage loginPage;

        private string TracrBaseUrl;

        public LoginSteps()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 60);

            createProfilePage = new CreateProfilePage(webDriver);
            loginPage = new LoginPage(webDriver);
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

        [When(@"I submit the required information")]
        public void WhenISubmitTheRequiredInformation(Table loginInfotable)
        {
            loginPage.SubmitRequiredInformation(loginInfotable);
        }

        [Then(@"I have the ability to log in")]
        public void ThenIHaveTheAbilityToLogIn()
        {
            bool profileCreated = loginPage.IsLoggedIn();
            Assert.IsTrue(profileCreated);
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
