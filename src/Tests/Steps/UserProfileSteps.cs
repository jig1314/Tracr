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
    [Scope(Feature = "User Profile")]
    public class UserProfileSteps
    {
        private readonly IWebDriver webDriver;
        private readonly CreateProfilePage createProfilePage;
        private readonly UserProfilePage userProfilePage;

        private string TracrBaseUrl;

        public UserProfileSteps()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 60);

            createProfilePage = new CreateProfilePage(webDriver);
            userProfilePage = new UserProfilePage(webDriver);
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

        [When(@"I click the view profile link")]
        public void WhenIClickTheViewProfileLink()
        {
            userProfilePage.ClickViewProfile();
        }

        [Then(@"I have the ability to view user profile")]
        public void ThenIHaveTheAbilityToViewUserProfile()
        {
            bool profileviewed = userProfilePage.OnProfilePage();
            Assert.IsTrue(profileviewed);
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
