using OpenQA.Selenium;
using System;

namespace Tracr.Tests.Pages
{
    public class PropertySuggestionsPage
    {
        public IWebDriver WebDriver { get; }

        #region Properties

        public IWebElement SuggestionsButton => WebDriver.FindElement(By.Id("suggestionsButton"));
        public IWebElement SuggestionUnavailableAlert => WebDriver.FindElement(By.Id("suggestionsUnavailable"));

        #endregion

        public PropertySuggestionsPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public void ClickSuggestionsButton()
        {
            SuggestionsButton.Click();
        }

        public bool SuggestionsUnavailable()
        {
            return SuggestionUnavailableAlert.Displayed;
        }
    }
}
