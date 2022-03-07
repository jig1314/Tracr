using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tracr.Tests.Pages
{
    public class LoginPage
    {
        public IWebDriver WebDriver { get; }

        #region Properties

        public IWebElement LoginLink => WebDriver.FindElement(By.Id("loginLink"));

        public IWebElement LogOutButton => WebDriver.FindElement(By.Id("logOutButton"));

        public IWebElement UserName => WebDriver.FindElement(By.Id("userNameText"));

        public IWebElement Password => WebDriver.FindElement(By.Id("passwordText"));

        public IWebElement Submit => WebDriver.FindElement(By.Id("submitButton"));

        #endregion

        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public void ClickLogin()
        {
            LoginLink.Click();
        }

        public void SubmitRequiredInformation(Table registerInfotable)
        {
            dynamic registerInfo = registerInfotable.CreateDynamicInstance();
            UserName.SendKeys(registerInfo.Username);
            Password.SendKeys(registerInfo.Password);

            Submit.Click();
        }

        public bool IsLoggedIn()
        {
            return LogOutButton.Displayed;
        }

        public void ClickLogout()
        {
            LogOutButton.Click();
        }
    }
}
