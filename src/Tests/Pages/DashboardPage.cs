using OpenQA.Selenium;

namespace Tracr.Tests.Pages
{
    public class DashboardPage
    {
        public IWebDriver WebDriver { get; }

        public IWebElement DashboardComponent => WebDriver.FindElement(By.Id("dashboardTitle"));


        public DashboardPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public bool IsVisible()
        {
            return DashboardComponent.Displayed;
        }
    }
}
