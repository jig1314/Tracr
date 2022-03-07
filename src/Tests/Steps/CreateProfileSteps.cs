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
    [Scope(Feature = "Create Profile")]
    public sealed class CreateProfileSteps
    {

        private readonly IWebDriver webDriver;
        private readonly CreateProfilePage createProfilePage;

        private string TracrBaseUrl;

        public CreateProfileSteps()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 60);

            createProfilePage = new CreateProfilePage(webDriver);
            TracrBaseUrl = "";
        }

        [BeforeScenario]
        public void SetBaseUrl()
        {
            TracrBaseUrl = Environment.GetEnvironmentVariable("TracrUrl");
        }

        [Given(@"I access the application")]
        public void WhenIAccessTheApplication()
        {
            webDriver.Navigate().GoToUrl(TracrBaseUrl);
        }

        [When(@"I click register link")]
        public void WhenIClickRegisterLink()
        {
            createProfilePage.ClickRegister();
        }

        [When(@"I submit the required information")]
        public void WhenISubmitTheRequiredInformation(Table registerInfotable)
        {
            createProfilePage.SubmitRequiredInformation(registerInfotable);
        }

        [Then(@"I have the ability to create a profile")]
        public void ThenIHaveTheAbilityToCreateAProfile()
        {
            bool profileCreated = createProfilePage.IsProfileCreated();
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