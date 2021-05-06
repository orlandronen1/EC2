using OpenQA.Selenium;
using System.Linq;

namespace EC2
{
    /// <summary>
    /// Page Object Class for the EC2 Instances website.
    /// </summary>
    public class EC2PageObject : BasePageObject
    {
        private readonly string PageUrl = "https://ec2instances.info/";

        #region Page Objects
        public IWebElement CompareButton => Class("btn-compare");

        public IWebElement RDSTab => LinkText("RDS");

        public IWebElement HeaderText => XPath("//h1/small");

        public IWebElement TestRow => Id("db.m3.medium");
        #endregion

        public EC2PageObject(IWebDriver browser) : base(browser) { }

        public void Navigate()
        {
            base.Navigate(PageUrl);
        }

        public int RowCount()
        {
            return Driver.FindElements(By.ClassName("instance")).Where(a => a.Displayed).Count();
        }
    }
}
