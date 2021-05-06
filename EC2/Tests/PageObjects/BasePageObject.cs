using OpenQA.Selenium;

namespace EC2
{
    /// <summary>
    /// Generic Base Page Object Class.
    /// </summary>
    public abstract class BasePageObject
    {
        protected readonly IWebDriver Driver;

        protected BasePageObject(IWebDriver browser)
        {
            this.Driver = browser;
        }

        #region Helpers to find POs
        protected IWebElement Id(string selector)
        {
            return Driver.FindElement(By.Id(selector));
        }

        protected IWebElement LinkText(string selector)
        {
            return Driver.FindElement(By.LinkText(selector));
        }

        protected IWebElement Class(string selector)
        {
            return Driver.FindElement(By.ClassName(selector));
        }

        protected IWebElement XPath(string selector)
        {
            return Driver.FindElement(By.XPath(selector));
        }
        #endregion

        protected void Navigate(string PageUrl)
        {
            this.Driver.Navigate().GoToUrl(PageUrl);
        }
    }
}
