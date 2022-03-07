using OpenQA.Selenium;

namespace Tracr.Tests.Pages
{
    public class UserProfilePage
    {
        public IWebDriver WebDriver { get; }

        #region Properties

        public IWebElement UserProfileLink => WebDriver.FindElement(By.Id("userProfileLink"));
        public IWebElement UserProfilePageBody => WebDriver.FindElement(By.Id("userProfilePageBody"));

        #endregion

        public UserProfilePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public void ClickViewProfile()
        {
            UserProfileLink.Click();
        }

        public bool OnProfilePage()
        {
            return UserProfilePageBody.Displayed;
        }
    }
}
