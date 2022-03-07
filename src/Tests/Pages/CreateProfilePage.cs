using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tracr.Tests.Pages
{
    public class CreateProfilePage
    {
        public IWebDriver WebDriver { get; }

        #region Properties

        public IWebElement RegisterLink => WebDriver.FindElement(By.Id("registerLink"));

        public IWebElement LogOutButton => WebDriver.FindElement(By.Id("logOutButton"));

        public IWebElement FirstName => WebDriver.FindElement(By.Id("firstNameText"));

        public IWebElement LastName => WebDriver.FindElement(By.Id("lastNameText"));

        public IWebElement Email => WebDriver.FindElement(By.Id("emailText"));

        public IWebElement UserName => WebDriver.FindElement(By.Id("userNameText"));

        public IWebElement Password => WebDriver.FindElement(By.Id("passwordText"));

        public IWebElement ConfirmPassword => WebDriver.FindElement(By.Id("confirmPasswordText"));

        public IWebElement Submit => WebDriver.FindElement(By.Id("submitButton"));

        #endregion

        public CreateProfilePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public void ClickRegister()
        {
            RegisterLink.Click();
        }

        public void SubmitRequiredInformation(Table registerInfotable)
        {
            dynamic registerInfo = registerInfotable.CreateDynamicInstance();
            FirstName.SendKeys(registerInfo.FirstName);
            LastName.SendKeys(registerInfo.LastName);
            Email.SendKeys(registerInfo.Email);
            UserName.SendKeys(registerInfo.Username);
            Password.SendKeys(registerInfo.Password);
            ConfirmPassword.SendKeys(registerInfo.ConfirmPassword);

            Submit.Click();
        }

        public bool IsProfileCreated()
        {
            return LogOutButton.Displayed;
        }
    }
}
